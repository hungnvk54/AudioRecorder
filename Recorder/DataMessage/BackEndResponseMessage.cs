using Newtonsoft.Json;

namespace AudioRecorderApps
{
    class BackEndResponseMessage
    {
        public string status
        {
            get; set;
        }
        public string errMes
        {
            get; set;
        }
        public string data
        {
            get; set;
        }
    }

    public class AudioTestServerResponseData
    {
        [JsonProperty(Required = Required.AllowNull)]
        public string parts { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public string full_text { get; set; }
    }

    public class AudioTestServerReponseMessage
    {
        public int status { get; set; }
        public string message { get; set; }
        public AudioTestServerResponseData data { get; set; }
    }
}
