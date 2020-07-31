using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using WebSocketSharp;
using System.IO;


namespace AudioRecorderApps
{
    class AppsSettings
    {
        private AppsSettings()
        {
            // Disable public create
            //ConfigurationManager
            ReadingAllConfigure();
            this.AuthorizeToken = "";
        }

        private static AppsSettings mInstance = null;
        public static AppsSettings GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new AppsSettings();
            }
            return mInstance;
        }

        public void SaveConfigure()
        {
            /*AddUpdateAppSettings("ws", this.WebsocketUrl);
            AddUpdateAppSettings("mic", this.DefaultMic);
            AddUpdateAppSettings("audiolength", this.DefaultAudioLength);
            AddUpdateAppSettings("api", this.ApiUrl);
            AddUpdateAppSettings("datadir", this.DataDir); */

            Settings.Default.ws = this.WebsocketUrl;
            Settings.Default.mic = this.DefaultMic;
            Settings.Default.audiolength = this.DefaultAudioLength;
            Settings.Default.api = this.ApiUrl;
            Settings.Default.datadir = this.DataDir;
            Settings.Default.Save();
        }

        private void ReadingAllConfigure()
        {
            this.WebsocketUrl = Settings.Default.ws; // ReadSetting("ws","wss://127.0.0.1");
            this.DefaultMic = Settings.Default.mic; // ReadSetting("mic", "wss://127.0.0.1"); 
            this.DefaultAudioLength = Settings.Default.audiolength; // ReadSetting("audiolength", "wss://127.0.0.1");
            this.ApiUrl = Settings.Default.api; // ReadSetting("api", "https://127.0.0.1/bocbang");
            this.DataDir = Settings.Default.datadir; // ReadSetting("datadir", "");
            if (this.DataDir.IsNullOrEmpty())
            {
                this.DataDir = Directory.GetCurrentDirectory();
            }

            Logger.GetInstance().Logging.Info("save directory: " + this.DataDir);
        }
        
        private string ReadSetting(string key, string defaultValue)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? defaultValue;
                Console.WriteLine(result);
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return defaultValue;
            }
        }

        void AddUpdateAppSettings(string key, string value)
        {
            Logger.GetInstance().Logging.Info(String.Format("Saving configure {0}:{1}", key, value));
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Logger.GetInstance().Logging.Error("Error writing app settings");
            }
        }

        public string WebsocketUrl
        {
            get;set;
        }

        public string DefaultMic
        {
            get;set;
        }

        public string  DefaultAudioLength
        {
            get;set;
        }

        public string ApiUrl
        {
            get;set;
        }

        public string AuthorizeToken
        {
            get;set;
        }

        public string DataDir
        {
            get; set;
        }
    }
}
