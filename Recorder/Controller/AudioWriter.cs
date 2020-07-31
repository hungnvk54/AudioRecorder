using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Lame;

namespace AudioRecorderApps.Controller
{
    class AudioWriter : IDisposable
    {
        private NAudio.Wave.WaveFileWriter mWavFileWriter = null;
        //NAudio.Lame.LameMP3FileWriter mMp3FileWriter = null;
        private bool disposedValue;
        private Type type;
        public enum Type
        {
            Mp3,
            Wav
        }

        public AudioWriter(string path, AudioWriter.Type type = Type.Wav)
        {
            this.type = type;
            if (type == Type.Wav)
            {
                NAudio.Wave.WaveFormat format = new NAudio.Wave.WaveFormat(16000, 1);
                mWavFileWriter = new NAudio.Wave.WaveFileWriter(path, format);
            } else if (type == Type.Mp3)
            {

            }
            else
            {
                Logger.GetInstance().Logging.Error("Unsupport type");
            }
        }

        public bool WriteStream(byte [] data, int length)
        {
            if (type == Type.Wav && mWavFileWriter != null)
            {
                mWavFileWriter.Write(data, 0, length);
                mWavFileWriter.Flush();
            }

            return true;
        }

        public bool WavToMP3(string waveFileName, string mp3FileName, int bitRate = 128)
        {
            bool result = true;
            try
            {
                using (var reader = new NAudio.Wave.WaveFileReader(waveFileName))
                using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                    reader.CopyTo(writer);

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (mWavFileWriter != null)
                    {
                        mWavFileWriter.Dispose();
                        mWavFileWriter = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AudioWriter()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
