using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioRecorderApps
{
    internal class AudioBinaryMessage
    {
        public string sessionId
        {
            get;set;
        }

        public string dataFile
        {
            get;set;
        }

        public string audio_length
        {
            get;set;
        }

        override public String ToString()
        {
            return String.Format("{{\"sessionId\":\"{0}\", \"dataFile\":\"{1}\", \"audio_length\":\"{2}\"}}", sessionId, dataFile, audio_length);
        }
    }
}
