using System;
using System.IO;
using NAudio;
namespace Recorder{
	public class Recorder : IDisposable
	{
		public enum SampleRate
        {
			SampleRate_8000 = 8000,
			SampleRate_16000 = 16000,
			SampleRate_22050 = 22050,
			SampleRate_44100 = 44100
		}

		public enum Channel
        {
			Channel_Mono = 1,
			Channel_Stereo = 2
        }

		private NAudio.Wave.WaveInEvent mRecorder = null;
		private NAudio.Wave.WaveFormat mOutputFormat = null;
		private NAudio.Wave.WaveFileWriter mWriter = null;

		public Recorder(int deviceIndex, SampleRate sampleRate = Recorder.SampleRate.SampleRate_16000, Channel channel = Recorder.Channel.Channel_Mono)
		{
			mOutputFormat = new NAudio.Wave.WaveFormat((int)sampleRate, (int)channel);
			this.InitRecorder(deviceIndex);
		}

		public void StartRecord()
        {
			if (mRecorder != null)
            {
				mRecorder.StartRecording();
            }
        }

		public void RegisterDataAvailable( EventHandler<NAudio.Wave.WaveInEventArgs> handler)
        {
			if (mRecorder != null)
            {
				mRecorder.DataAvailable += handler;
            }
        }
		public void RegisterStop(EventHandler<NAudio.Wave.StoppedEventArgs> handler)
		{
			if (mRecorder != null)
			{
				mRecorder.RecordingStopped += handler;
			}
		}

		public void StopRecord()
        {
			if (mRecorder != null)
			{
				mRecorder.StopRecording();
				mRecorder.Dispose();
				mRecorder = null;
			}

			if (mWriter != null)
			{
				mWriter.Dispose();
				mWriter = null;
			}
		}

        public void Dispose()
        {
			this.StopRecord();
        }

		void InitRecorder(int deviceIndex)
        {
			if (mRecorder == null)
            {
				mRecorder = new NAudio.Wave.WaveInEvent();
				mRecorder.DeviceNumber = deviceIndex;
				mRecorder.WaveFormat = mOutputFormat;
				mRecorder.NumberOfBuffers = 5;
				mRecorder.BufferMilliseconds = 300; //3200

				Logger.GetInstance().Logging.Info(String.Format("Number of buffer {0}", mRecorder.NumberOfBuffers));
			}
			
        }
	}
}
