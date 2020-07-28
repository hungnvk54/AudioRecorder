using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Recorder.DataMessage;

namespace Recorder
{
    /// <summary>
    /// Create Http Request, using json, and read Http Response.
    /// </summary>

    public class MyPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint,
          X509Certificate certificate, WebRequest request,
          int certificateProblem)
        {
            //Return True to force the certificate to be accepted.
            return true;
        }
    }

    public class Request
    {
        public static WebRequest GetWebRequester(string url, string token = "")
        {
            Utils.ConnectionType type = Utils.ParsingConnectionType(url);
            if (type == Utils.ConnectionType.Unknown)
            {
                return null;
            }

            WebRequest request = WebRequest.Create(url);
            if (!token.Equals(""))
            {
                request.Headers.Add("Authorization", token);
            }

            if (type == Utils.ConnectionType.SSLProtocol)
            {
                ServicePointManager.CertificatePolicy = new MyPolicy();
            }

            return request;
        }

        public static bool RequestChangeSessionStatus(string sessionId, int isLive)
        {
            try
            {
                bool requestResult = false;
                // Create a request using a URL that can receive a post.
                string uri = string.Format("{0}/sessions/updateStatusByIdSession", Settings.GetInstance().ApiUrl);
                Logger.GetInstance().Logging.Info(String.Format("Send change system status {0} to {1}", isLive, uri));
                WebRequest request = GetWebRequester(uri, Settings.GetInstance().AuthorizeToken);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";

                // Create POST data and convert it to a byte array.
                string postData = String.Format("{{\"idSession\": {0}, \"status\": 1, \"isLive\": {1} }}", sessionId, isLive);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Logger.GetInstance().Logging.Info(responseFromServer);
                    try
                    {
                        BackEndResponseMessage RspMsg = JsonConvert.DeserializeObject<BackEndResponseMessage>(responseFromServer);
                        if (RspMsg.status.Equals("1"))
                        {
                            requestResult = true;
                        }
                        else
                        {
                            requestResult = false;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance().Logging.Error(e);
                        requestResult = false;
                    }
                }

                // Close the response.
                response.Close();
                return requestResult;
            }
            catch (Exception e)
            {
                Logger.GetInstance().Logging.Error(String.Format("{0}", e));
                return false;
            }

        }

        private static string CreateFormDataBoundary()
        {
            return "---------------------------" + DateTime.Now.Ticks.ToString("x");
        }
        public static bool RequestCreateParentAudio(string sessionId, string fileName)
        {
            try
            {
                bool requestResult = true;
                string FormDataTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";
                // Create a request using a URL that can receive a post.
                string uri = string.Format("{0}/transcript/saveAudioTranslateOnline", Settings.GetInstance().ApiUrl);
                WebRequest request = GetWebRequester(uri, Settings.GetInstance().AuthorizeToken);
                // Set the Method property of the request to POST.
                string boundary = CreateFormDataBoundary();
                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                // Create POST data and convert it to a byte array.
                string postData2 = String.Format("{{\"transcript\": {{}},\"idSession\": {0}, \"fileNameAudio\": \"{1}\",\"type\":\"0\",\"startTimeSplit\": 0}}", sessionId, fileName);

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.

                string item = String.Format(FormDataTemplate, boundary, "translateOnlineInfo", postData2);
                byte[] itemBytes = Encoding.UTF8.GetBytes(item);
                dataStream.Write(itemBytes, 0, itemBytes.Length);


                string HeaderTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n";
                string header = String.Format(HeaderTemplate, boundary, "file", fileName, "audio/wav");
                byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                dataStream.Write(headerbytes, 0, headerbytes.Length);

                byte[] newlineBytes = Encoding.UTF8.GetBytes("\r\n");
                dataStream.Write(newlineBytes, 0, newlineBytes.Length);

                byte[] endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
                dataStream.Write(endBytes, 0, endBytes.Length);
                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Logger.GetInstance().Logging.Info(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Logger.GetInstance().Logging.Info(responseFromServer);
                    try { 
                        BackEndResponseMessage RspMsg = JsonConvert.DeserializeObject<BackEndResponseMessage>(responseFromServer);
                        if (RspMsg.status.Equals("1"))
                        {
                            requestResult = true;
                        }
                        else
                        {
                            requestResult = false;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance().Logging.Error(e);
                        requestResult = false;
                    }
                }

                // Close the response.
                response.Close();

                return requestResult;
            } catch(Exception e)
            {
                Logger.GetInstance().Logging.Error(String.Format("{0}", e));
                return false;
            }
            
        }


        public static string RequestLogin(string username, string password)
        {
            try
            {
                string requestResult = "";
                // Create a request using a URL that can receive a post.
                string uri = string.Format("{0}/login", Settings.GetInstance().ApiUrl);
                WebRequest request = GetWebRequester(uri, Settings.GetInstance().AuthorizeToken);
                // Set the Method property of the request to POST.
                string boundary = CreateFormDataBoundary();
                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                string FormDataTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";

                string item = String.Format(FormDataTemplate, boundary, "username", username);
                byte[] itemBytes = Encoding.UTF8.GetBytes(item);
                dataStream.Write(itemBytes, 0, itemBytes.Length);

                item = String.Format(FormDataTemplate, boundary, "password", password);
                itemBytes = Encoding.UTF8.GetBytes(item);
                dataStream.Write(itemBytes, 0, itemBytes.Length);

                byte[] endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
                dataStream.Write(endBytes, 0, endBytes.Length);
                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Logger.GetInstance().Logging.Info(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Logger.GetInstance().Logging.Info(responseFromServer);
                    try
                    {
                        LoginSuccessfullyMessage RspMsg = JsonConvert.DeserializeObject<LoginSuccessfullyMessage>(responseFromServer);
                        return RspMsg.Authorization;
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance().Logging.Error(e);
                        requestResult = "";
                    }
                }

                // Close the response.
                response.Close();

                return requestResult;
            }
            catch (Exception e)
            {
                Logger.GetInstance().Logging.Error(String.Format("{0}", e));
                return "";
            }

        }


    }
}