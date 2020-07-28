using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace Recorder
{
    class Settings
    {
        private Settings()
        {
            // Disable public create
            //ConfigurationManager
            ReadingAllConfigure();
            this.AuthorizeToken = "";
        }

        private static Settings mInstance = null;
        public static Settings GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new Settings();
            }
            return mInstance;
        }

        public void SaveConfigure()
        {
            AddUpdateAppSettings("ws", this.WebsocketUrl);
            AddUpdateAppSettings("mic", this.DefaultMic);
            AddUpdateAppSettings("audiolength", this.DefaultAudioLength);
            AddUpdateAppSettings("api", this.ApiUrl);
        }

        private void ReadingAllConfigure()
        {
            this.WebsocketUrl = ReadSetting("ws","wss://127.0.0.1");
            this.DefaultMic = ReadSetting("mic", "wss://127.0.0.1"); 
            this.DefaultAudioLength = ReadSetting("audiolength", "wss://127.0.0.1");
            this.ApiUrl = ReadSetting("api", "https://127.0.0.1/bocbang");

            Logger.GetInstance().Logging.Info(this.WebsocketUrl);
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
    }
}
