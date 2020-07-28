using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Recorder
{
    internal class TextResultMessage
    {
        public string flag
        {
            get; set;
        }

        public string body
        {
            get;set;
        }

        public string speaker_type
        {
            get;set;
        }

        public string speaker_id
        {
            get;set;
        }

        public string unknown_speaker
        {
            get;set;
        }
        
        public string para
        {
            get;set;
        }
        public string script_index
        {
            get;set;
        }

        public string audio_base64
        {
            get;set;
        }

    }
}
