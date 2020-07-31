using System;
using System.Collections.Generic;
using WebSocketSharp;
using System.Security.Authentication;
using System.Collections;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;


namespace AudioRecorderApps
{
    class AudioStreamer : IDisposable
    {
        
        private enum SslProtocolsWorkarround
        {
            Tls = 192,
            Tls11 = 768,
            Tls12 = 3072
        }

        private WebSocketSharp.WebSocket mWebsocket = null;
        private Queue<AudioBinaryMessage> mMessageQueue = null;
        private object mLocker;

        public event EventHandler<AudioStreamTextArgs> MessageAvailable;
        public event EventHandler Opened;
        public event EventHandler<CloseEventArgs> Closed;

        private string mUrl;
        private int mRetryConnectionCounter;
        private const int MAX_RETRY_CONNECTION = 5;
        public AudioStreamer()
        {
            mMessageQueue = new Queue<AudioBinaryMessage>();
            mLocker = ((ICollection)mMessageQueue).SyncRoot;
        }

        public void StartStreaming(String url)
        {
            //1. Reset all previous parameter
            //this.ResetStream();
            //2. Create new connection
            this.CreateConnection(url);
        }

        public void OnClose (object s, CloseEventArgs e)
        {
            
            if (!e.WasClean)
            {
                if (!mWebsocket.IsAlive)
                {
                    Thread.Sleep(5000);
                    Logger.GetInstance().Logging.Info("Try Reconnect to server ");
                    if (mRetryConnectionCounter < MAX_RETRY_CONNECTION)
                    { 
                        mWebsocket.ConnectAsync();
                        mRetryConnectionCounter += 1;
                        Logger.GetInstance().Logging.Info(String.Format("Number of connection retry {0}", mRetryConnectionCounter));
                    }
                    else
                    {
                        Logger.GetInstance().Logging.Info("Recreate the connection instance");
                        RetryConnection();
                    }
                }
            }
            else
            {
                Closed?.Invoke(this, e);
            }
        }

        public void OnError(object s, ErrorEventArgs e)
        {
        }
        public void OnMessage(object s, MessageEventArgs e)
        {
            if (e.IsText)
            {
                TextResultMessage textObject = ParseingMessage(e.Data);
                if (textObject != null)
                {
                    NotifyMessage(textObject);
                }
            } else if (e.IsBinary)
            {
                //@Todo
            }
        }

        public static void SendMessageThread(AudioStreamer _this)
        {
            
        }

        public void PushMessage(AudioBinaryMessage message)
        {
            lock(mLocker)
            {
                mMessageQueue.Enqueue(message);
            }
        }

        public AudioBinaryMessage PopMessage()
        {
            lock(mLocker)
            if (mMessageQueue.Count <= 0)
            {
                return null;
            }

            return mMessageQueue.Peek();
        }

        public void OnOpen(Object s, EventArgs e)
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        public bool SendMessage(AudioBinaryMessage message)
        {
            if (mWebsocket.ReadyState == WebSocketState.Open)
            {
                try
                {
                    if (mWebsocket != null)
                    {
                        mWebsocket.Send(message.ToString());
                        return true;
                    }
                }
                catch (InvalidOperationException e)
                {
                    // Invalid operation
                    Logger.GetInstance().Logging.Error("Sending Message In Closed State");
                    return false;
                }
            }
            return false;
        }

        public bool T_SendMessage(string message)
        {
            try
            {
                if (mWebsocket != null)
                {
                    mWebsocket.Send(message);
                    return true;
                }
            }
            catch (InvalidOperationException e)
            {
                // Invalid operation
                Logger.GetInstance().Logging.Error("Sending Message In Closed State");
                return false;
            }
            return false;
        }

        private void NotifyMessage(TextResultMessage message)
        {
            MessageAvailable?.Invoke(this, new AudioStreamTextArgs(message));
        }
        private void CreateConnection(String url)
        {
            mUrl = url;
            mWebsocket = new WebSocket(url);
            Utils.ConnectionType type = Utils.ParsingConnectionType(url);
            if (type == Utils.ConnectionType.NormProtocol)
            {
                //Throw exception
            }
            else if (type == Utils.ConnectionType.SSLProtocol)
            {
                mWebsocket.SslConfiguration.EnabledSslProtocols = (SslProtocols)(AudioStreamer.SslProtocolsWorkarround.Tls12) |
                                                                (SslProtocols)(AudioStreamer.SslProtocolsWorkarround.Tls11) | (SslProtocols)(AudioStreamer.SslProtocolsWorkarround.Tls) |
                                                                SslProtocols.Default | SslProtocols.Ssl2 | SslProtocols.Ssl3 | SslProtocols.Tls;

            }
            else
            {
                throw new InvalidOperationException("Url must be ws:// or wss://");
            }

            mWebsocket.OnClose += new EventHandler<CloseEventArgs>(OnClose);
            mWebsocket.OnError += new EventHandler<ErrorEventArgs>(OnError);
            mWebsocket.OnMessage += new EventHandler<MessageEventArgs>(OnMessage);
            mWebsocket.OnOpen += new EventHandler(OnOpen);

            mWebsocket.ConnectAsync();
        }

        private TextResultMessage ParseingMessage(string message)
        {
            Debug.WriteLine(message);
            try {
                TextResultMessage messageObject = JsonConvert.DeserializeObject<TextResultMessage>(message);

                return messageObject;
            } catch(JsonReaderException e)
            {
                Debug.WriteLine(e);
                return null;
            }
            
        }

        private void ResetStream()
        {
            //1. Reset queue
            mMessageQueue.Clear();

            //2. Close websocket connection
            if (mWebsocket != null)
            {
                mWebsocket.Close(CloseStatusCode.Normal, "Close Normal");
                mWebsocket = null;
            }
        }

        private void RetryConnection()
        {
            ResetStream();
            mRetryConnectionCounter = 0;
            CreateConnection(mUrl);
        }

        public void Dispose()
        {
            this.ResetStream();
        }
    }
}
