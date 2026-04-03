using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using AudioRecorderApps.Controller;
using WebSocketSharp;
using AudioRecorderApps.Data;
using AudioRecorderApps;
using System.Media;

namespace Recorder.AppForm
{
    public partial class AudioTesting : Form
    {
        private enum RecordingState
        {
            IDLE,
            STARTING,
            RECORDED,
            STOPPING
        }

        private enum WorkerStatus
        {
            CANNOT_CREATE_AUDIO_RECORDER = 2,
            ALL_CONDITION_DONE = 4
        }

        const int AUDIO_SAMPEL_RATE_HZ = 16000;
        const int SampleLengthInSecond = 10;

        int mTestFileIndex;
        // Private data
        private Queue<WaveInEventArgs> mRawAudioData;
        private readonly BackgroundWorker mBackgroundWorker;
        private RecordingState mIsCapture = RecordingState.IDLE;
        private AudioRecorderApps.Recorder mAudioRecorder = null;

        private int mSelectedMicIndex = -1;
        private long mRawAudioLength = 0;

        private readonly object balanceLock = new object();

        // Delegate
        public delegate void DUpdateListView();
        public delegate void DUpdateMicValueBar(float volume);

        List<AudioTestResult> mListOfflineAudioFile;
        SoundPlayer mPlayer;

        public AudioTesting()
        {
            InitializeComponent();

            InitializeData();


            // Init the data
            mRawAudioData = new Queue<WaveInEventArgs>();
            CB_ListMic.SelectedIndex = Int16.Parse(AppsSettings.GetInstance().DefaultMic);
            mListOfflineAudioFile = new List<AudioTestResult>();
            // Init the worker
            mBackgroundWorker = new BackgroundWorker();
            mBackgroundWorker.WorkerSupportsCancellation = true;
            mBackgroundWorker.DoWork += StartRecording;
            mBackgroundWorker.WorkerReportsProgress = true;
            mBackgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            mBackgroundWorker.ProgressChanged += WorkerConnectionStateChange;

            mTestFileIndex = 0;
        }

        private void InitializeData()
        {
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                CB_ListMic.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            }

        }

        private void WorkerConnectionStateChange(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == (int)WorkerStatus.CANNOT_CREATE_AUDIO_RECORDER)
            {
                AppLogger.GetInstance().Logging.Warn("Không khởi tạo được thiết bị ghi âm.\n" +
                    "Kiểm tra lại cấu hình thiết bị ghi âm");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.ProgressPercentage == (int)WorkerStatus.ALL_CONDITION_DONE)
            {
                /// Connection ok.
                InitRecordingGui();
            }
        }

        private void InitRecordingGui()
        {
            mIsCapture = RecordingState.RECORDED;
            btn_test.Text = "Kết thúc";
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void StartRecording(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            bool isSuccessfull;

            isSuccessfull = CreateAudioRecorder();
            if (isSuccessfull == false)
            {
                AppLogger.GetInstance().Logging.Warn("Cannot create audio recorder");
                worker.ReportProgress((int)WorkerStatus.CANNOT_CREATE_AUDIO_RECORDER);
                return;
            }

            worker.ReportProgress(4);
            while (worker.CancellationPending == false)
            {
                Thread.Sleep(100);
                ProcessRawAudioData(false);
            }
            e.Cancel = true;
            return;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///Close all connection
            StopRecording();
            /*if (e.Cancelled)
            {
                ConvertToMp3();
            }*/
            InitRecordingIdle();
        }


        private bool CreateAudioRecorder()
        {
            if (mAudioRecorder != null)
            {
                DestroyAudioRecorder();
            }

            if (mSelectedMicIndex != -1)
            {
                mAudioRecorder = new AudioRecorderApps.Recorder(mSelectedMicIndex);
                mAudioRecorder.RegisterDataAvailable(OnDataAvailable);
                mAudioRecorder.StartRecord();
                return true;
            }
            return false;
        }

        private bool DestroyAudioRecorder()
        {
            if (mAudioRecorder != null)
            {
                mAudioRecorder.Dispose();
                mAudioRecorder = null;
            }

            return true;
        }

        private void CB_ListMic_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectedMicIndex = CB_ListMic.SelectedIndex;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            decorate_volumeter_bar(args);
            //Send Test message
            //Send Audio to Websocket Client
            lock (balanceLock)
            {
                try
                {
                    byte[] rawAudio = new byte[args.BytesRecorded];
                    args.Buffer.CopyTo(rawAudio, 0);
                    mRawAudioData.Enqueue(new WaveInEventArgs(rawAudio, rawAudio.Length));
                    mRawAudioLength += args.BytesRecorded;
                }
                catch
                {

                }
            }
        }

        private void StopRecording()
        {
            DestroyAudioRecorder();
            InitRecordingIdle();
        }

        private void InitRecordingIdle()
        {
            
            btn_test.Text = "Start";
            CB_ListMic.Enabled = true;
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
            mIsCapture = RecordingState.IDLE;
        }

        private void decorate_volumeter_bar(WaveInEventArgs args)
        {
            try
            {
                float max = 0;
                // interpret as 16 bit audio
                for (int index = 0; index < args.BytesRecorded; index += 2)
                {
                    short sample = (short)((args.Buffer[index + 1] << 8) |
                                            args.Buffer[index + 0]);

                    // to floating point
                    var sample32 = sample / 32768f;
                    // absolute value 
                    if (sample32 < 0) sample32 = -sample32;
                    // is this the max value?
                    if (sample32 > max) max = sample32;

                }
                VM_VolumeMeter.Invoke(new DUpdateMicValueBar(UpdateMicVolume), max);
            } catch
            {

            }
            
        }

        private void UpdateMicVolume(float volume)
        {
            VM_VolumeMeter.Amplitude = volume;
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            if (mIsCapture == RecordingState.RECORDED)
            {
                //Stop capture here
                if (mBackgroundWorker.IsBusy)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    AppLogger.GetInstance().Logging.Info("Cancelling the Thread");
                    mIsCapture = RecordingState.STOPPING;
                    this.btn_test.Text = "Stoping";
                    mBackgroundWorker.CancelAsync();
                }
            }
            else if (mIsCapture == RecordingState.STARTING)
            {
                MessageBox.Show("Vui lòng chờ, hệ thống đang khởi động", "Thông tin phiên",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (mIsCapture == RecordingState.IDLE)
            {
                // Set cursor as default arrow
                Cursor.Current = Cursors.WaitCursor;
                mBackgroundWorker.RunWorkerAsync();

                this.btn_test.Text = "...";
                mIsCapture = RecordingState.STARTING;
            }
            else if (mIsCapture == RecordingState.STOPPING)
            {
                MessageBox.Show("Hệ thống đang tắt\nVui lòng chờ...", "Thông tin phiên",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool IsEnoughData()
        {
            double audioLengthInSecond = mRawAudioLength / (AUDIO_SAMPEL_RATE_HZ * 2);

            return audioLengthInSecond >= SampleLengthInSecond ? true : false;
        }

        private void ProcessRawAudioData(bool is_finish = false)
        {
            string path = null;
            if (is_finish == false)
            {
                //1. Check total length off audio
                if (IsEnoughData() == false)
                {
                    return;
                }
                path = SaveAudioFromBufferToFile();
            }
            else
            {
            }
            //2. Save to wav file and Put in list audio buffer
            if (path != null)
            {
                AudioTestResult mp3File = new AudioTestResult(path, "");

                mListOfflineAudioFile.Add(mp3File);
            }
            else
            {
                AppLogger.GetInstance().Logging.Error("Cannot save file to current path");
            }
            //4. Send to backend server
            sendAudioFileToBackend();

            //5. Update View
            RefreshDataView(mListOfflineAudioFile);
        }

        private void sendAudioFileToBackend()
        { 
            for (int i = 0; i < mListOfflineAudioFile.Count; ++i)
            {
                AudioTestResult file = mListOfflineAudioFile[i];

                if (file.status == AudioTestResult.TranslateStatus.Not_Sent)
                {
                    AudioTestServerReponseMessage rmsg = Request.UploadFileToTestServer(file.FullFilePath);
                    if (rmsg != null)
                    {
                        if(rmsg.data != null)
                        {

                            if (string.IsNullOrWhiteSpace(rmsg.data.full_text))
                            {
                                file.volume = "Không có âm thanh";
                            }
                            else
                            {
                                file.Text = rmsg.data.full_text;
                                file.volume = "Có âm thanh";
                            }
                        }
                        file.status = AudioTestResult.TranslateStatus.Sent;
                        mListOfflineAudioFile[i] = file;
                    }
                    else
                    {
                        
                    }
                }
            }

        }

        private void RefreshDataView(List<AudioTestResult> list)
        {
            this.ListTestSample.Invoke(new DUpdateListView(UpdateListView));
        }

        private void UpdateListView()
        {
            ListTestSample.Items.Clear();
            int index = 0;
            foreach (AudioTestResult item in mListOfflineAudioFile)
            {
                ListViewItem viewItem = new ListViewItem();
                viewItem.Text = index.ToString();
                viewItem.ImageIndex = 0;
                viewItem.SubItems.Add(item.FileName);
                viewItem.SubItems.Add(SampleLengthInSecond.ToString());
                viewItem.SubItems.Add(item.volume);
                viewItem.SubItems.Add(item.status.ToString());
                viewItem.SubItems.Add(item.Text);
                ListTestSample.Items.Add(viewItem);
                index += 1;
            }
        }

        private string SaveAudioFromBufferToFile()
        {
            try
            {
                string fileName = GetWavAudioChildName((mListOfflineAudioFile.Count + 1).ToString());
                Queue<WaveInEventArgs> remainRawData = new Queue<WaveInEventArgs>();

                double savedAudioLengthInArrayLength = 0;
                double saveAudioLengthInSecond = 0;
                AudioWriter wavWriter = new AudioWriter(fileName);
                lock (balanceLock)
                {
                    while (saveAudioLengthInSecond < SampleLengthInSecond  && mRawAudioData.Count > 0)
                    {
                        WaveInEventArgs rawData = mRawAudioData.Dequeue();
                        mRawAudioLength -= rawData.BytesRecorded;
                        wavWriter.WriteStream(rawData.Buffer, rawData.BytesRecorded);

                        savedAudioLengthInArrayLength += rawData.BytesRecorded;
                        saveAudioLengthInSecond = savedAudioLengthInArrayLength / (16000 * 2);
                    }

                }
                wavWriter.Dispose();
                return fileName;
            }
            catch
            {
                return null;
            }
        }

        private string GetWavAudioChildName(string name = "")
        {
            if (name.IsNullOrEmpty())
            {
                return Path.Combine(GetAudioChildPath(), mTestFileIndex.ToString()+ ".wav");
            }
            else
            {
                string uuid = Guid.NewGuid().ToString("N").Substring(0, 4);
                return Path.Combine(GetAudioChildPath(), name + "_" + uuid + ".wav");
            }
        }

        private string GetAudioChildPath()
        {
            string path = Path.Combine(AppsSettings.GetInstance().DataDir, "test");

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private void VM_VolumeMeter_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ListTestSample_DoubleClick(object sender, EventArgs e)
        {
            AppLogger.GetInstance().Logging.Info("Double click");
        }

        private void ListTestSample_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AppLogger.GetInstance().Logging.Info("Double click - V2");
            ListViewHitTestInfo hit = ListTestSample.HitTest(e.Location);
            if (hit.Item != null && hit.SubItem != null)
            {
                int rowIndex = hit.Item.Index;
                int columnIndex = hit.Item.SubItems.IndexOf(hit.SubItem);

                if(mPlayer!= null)
                {
                    mPlayer.Stop();
                }
                mPlayer = new SoundPlayer(mListOfflineAudioFile[rowIndex].FullFilePath);
                AppLogger.GetInstance().Logging.Info("Play Audio at: " + mListOfflineAudioFile[rowIndex].FullFilePath);
                mPlayer.Play();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (mIsCapture == RecordingState.RECORDED)
            {
                MessageBox.Show("Không thể đóng khi kiểm tra ghi âm\n" +
                    "Kết thúc kiểm tra rồi thử lại", "Hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else if (mIsCapture == RecordingState.STARTING)
            {
               
            }
        }
    }

}
