using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorderApps.Data
{
    public class AudioTestResult
    {
        public enum TranslateStatus
        {
            Not_Sent,
            Sent,
            Empty
        }
        public AudioTestResult(string path, string text)
        {
            this.FileName = Path.GetFileName(path);
            this.status = TranslateStatus.Not_Sent;
            this.FullFilePath = path;
            this.Text = text;
        }

        
        public string FileName { get; set; }
        public TranslateStatus status { get; set; }
        public string FullFilePath { get; set; }
        public string Text { get; set; }
        public string volume { get; set; }
    }
}
