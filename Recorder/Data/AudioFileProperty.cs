using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorderApps.Data
{
    public class AudioFileProperty
    {
        public enum TranslateStatus{
            Not_Sent,
            Sent
        }
        private TimeSpan mAudioTime;
        public AudioFileProperty(string path)
        {
            this.FileName = Path.GetFileName(path);
            this.mAudioTime = Utils.GetAudioLength(path);
            this.status = TranslateStatus.Not_Sent;
            this.FullFilePath = path;
        }

        public double DurationInSecond { get { return mAudioTime.TotalSeconds; } }
        public string FileName { get; set; }
        public string FileDuration { get {
                return string.Format("{0:D2}:{1:D2}:{2:D2}",
                                        this.mAudioTime.Hours,
                                        this.mAudioTime.Minutes,
                                        this.mAudioTime.Seconds);
            } }
        public TranslateStatus status { get; set; }
        public string FullFilePath { get; set; }
    }
}
