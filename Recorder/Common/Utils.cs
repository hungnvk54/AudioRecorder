using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WebSocketSharp;

namespace AudioRecorderApps
{
    class Utils
    {
        public enum ConnectionType
        {
            NormProtocol = 0,
            SSLProtocol = 1,
            Unknown = 2
        }
        public static string ParsingApiUrl()
        {
            return AppsSettings.GetInstance().ApiUrl;
        }

        public static string ParsingWeboscketUrl(string sessionId)
        {
            string url = AppsSettings.GetInstance().WebsocketUrl + String.Format("/client/ws/speech?sessionId={0}&isMaster=APP", sessionId);
            Logger.GetInstance().Logging.Info(String.Format("Outut Websocket url: {0}", url));
            return url;
        }

        public static string ParsingWeboscketUrl_Test(string sessionId)
        {
            string url = AppsSettings.GetInstance().WebsocketUrl + String.Format("/echo", sessionId);
            Logger.GetInstance().Logging.Info(String.Format("Outut Websocket url: {0}", url));
            return url;
        }


        public static string ParsingSessionId(string url)
        {
            /* The url format: http://hostname/phien-hop/1323 */
            String[] spearator = { "/"};
            String[] listPattern = url.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            if (listPattern.Length <=3)
            {
                return String.Empty;
            }
            else
            {
                return listPattern[listPattern.Length - 1];
            }
        }

        public static ConnectionType ParsingConnectionType(String url)
        {
            ConnectionType type = ConnectionType.NormProtocol;
            if (url.StartsWith("wss://") || url.StartsWith("https://"))
            {
                type = ConnectionType.SSLProtocol;
            }
            else if (url.StartsWith("ws://") || url.StartsWith("http://"))
            {
                type = ConnectionType.NormProtocol;
            }
            else
            {
                type = ConnectionType.Unknown;
            }

            return type;
        }

        public static string Base64Encode(byte [] data, int length)
        {
            return System.Convert.ToBase64String(data, 0, length);
        }

        public static string GetFilePath(string fileName, string ext, string addPath = "")
        {
            if (addPath.IsNullOrEmpty())
            {
                return Path.Combine(AppsSettings.GetInstance().DataDir, fileName + ext);
            }
            else
            {
                return Path.Combine(AppsSettings.GetInstance().DataDir, addPath, fileName + ext);
            }
        }

        public static string GetAudioFilePath(string session, string fileName, string ext)
        {
            return GetFilePath(fileName, ext, session);
        }

        public static TimeSpan GetAudioLength(string mp3Path)
        {
            NAudio.Wave.Mp3FileReader reader = new NAudio.Wave.Mp3FileReader(mp3Path);

            return reader.TotalTime;
        }
    }
}
