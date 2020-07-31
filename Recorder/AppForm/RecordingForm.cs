using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using AudioRecorderApps.Controller;
using WebSocketSharp;
using System.Text.RegularExpressions;
using AudioRecorderApps.Data;

namespace AudioRecorderApps
{
    public partial class AudioRecordingForms : Form
    {
        //Common
        private enum RecordingState
        {
            IDLE,
            STARTING,
            RECORDED
        }

        private const int onlineRecordingTabIndex = 0;
        private const int offlineRecordingTabIndex = 1;

        //This section for audio Offline Recording
        List<AudioFileProperty> mListOfflineAudioFile;
        private Queue<WaveInEventArgs> mRawAudioData;
        private int mRawAudioLength = 0;
        private readonly BackgroundWorker mBackgroundOfflineWorker;

        // This section for online recording
        private RecordingState mIsCapture = RecordingState.IDLE;
        private System.Windows.Forms.Timer mTimerCounter = null;
        private long mTimeCounter = 0;
        private bool mIsPause = false;
        private int mSelectedMicIndex = -1;

        private AudioStreamer mAudioStreamer = null;
        private Recorder mAudioRecorder = null;
        private AudioWriter mAudioWriter = null;

        /// Session Info
        private string mSessionId;
        private string mRecordingTime;
        private string mAudioName;

        private readonly BackgroundWorker mBackgroundWorker;
        private readonly object balanceLock = new object();

        //Delegate
        public delegate void UpdateContent(string content);
        public delegate void DUpdateConnectionStatus(bool status);
        public delegate void DUpdateMicValueBar(float volume);
        public delegate void DUpdateListView();
        //Websocket 
        private Queue<WaveInEventArgs> mErrorBufferData;

        public AudioRecordingForms()
        {
            InitializeComponent();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                cb_ListMicIn.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            }
            mRawAudioData = new Queue<WaveInEventArgs>();
            mListOfflineAudioFile = new List<AudioFileProperty>();

            mErrorBufferData = new Queue<WaveInEventArgs>();

            cb_ListMicIn.SelectedIndex = Int16.Parse(AppsSettings.GetInstance().DefaultMic);
            TB_AudioLength.Text = AppsSettings.GetInstance().DefaultAudioLength;

            mBackgroundWorker = new BackgroundWorker();
            mBackgroundWorker.WorkerSupportsCancellation = true;
            mBackgroundWorker.DoWork += StartRecording;
            mBackgroundWorker.WorkerReportsProgress = true;
            mBackgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            mBackgroundWorker.ProgressChanged += WorkerConnectionStateChange;


            mBackgroundOfflineWorker = new BackgroundWorker();
            mBackgroundOfflineWorker.WorkerSupportsCancellation = true;
            mBackgroundOfflineWorker.DoWork += StartOfflineRecording;
            mBackgroundOfflineWorker.WorkerReportsProgress = true;
            mBackgroundOfflineWorker.RunWorkerCompleted += worker_RunWorkerOfflineCompleted;
            mBackgroundOfflineWorker.ProgressChanged += WorkerPercentageStateChange;
    }

        private void BT_Start_Click(object sender, EventArgs e)
        {
            //Request.CreateOfflineSession("2000581", "Filename.mp3");
            //return;
            // I. Check the selection tab
            if (AudioRecording_Tab.SelectedIndex == onlineRecordingTabIndex) { 
                //1. Check the current state
                if (mIsCapture == RecordingState.RECORDED)
                {
                    //Stop capture here
                    if (mBackgroundWorker.IsBusy)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Logger.GetInstance().Logging.Info("Cancelling the Thread");
                        mBackgroundWorker.CancelAsync();
                    }
                }
                else if (mIsCapture == RecordingState.STARTING)
                {
                    MessageBox.Show("Vui lòng chờ, hệ thống đang khởi động", "Thông tin phiên",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.WaitCursor;
                    bool condition = CheckCondition();
                    if (condition == true)
                    {
                        mBackgroundWorker.RunWorkerAsync();
                        mIsCapture = RecordingState.STARTING;
                        this.AudioRecording_Tab.Selectable = false;
                    }
                }
            }
            else if (this.AudioRecording_Tab.SelectedIndex == offlineRecordingTabIndex)
            {
                //II. Offline Recording
                Logger.GetInstance().Logging.Info("Offline recording...");
                if (mIsCapture == RecordingState.RECORDED)
                {
                    //Stop capture here
                    if (mBackgroundOfflineWorker.IsBusy)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Logger.GetInstance().Logging.Info("Cancelling the Thread");
                        mBackgroundOfflineWorker.CancelAsync();
                    }
                }
                else if (mIsCapture == RecordingState.STARTING)
                {
                    MessageBox.Show("Vui lòng chờ, hệ thống đang khởi động", "Thông tin phiên",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.WaitCursor;
                    bool condition = CheckCondition();
                    if (condition == true)
                    {
                        mListOfflineAudioFile.Clear();
                        UpdateListView();
                        mBackgroundOfflineWorker.RunWorkerAsync();
                        mIsCapture = RecordingState.STARTING;
                        this.AudioRecording_Tab.Selectable = false;
                    }
                }
            }

        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            Logger.GetInstance().Logging.Info(String.Format("Number of Recording byte: {0}", args.BytesRecorded));
            if (mIsPause == false)
            {
                decorate_volumeter_bar(args);
                //Send Test message
                //Send Audio to Websocket Client

                while (mErrorBufferData.Count > 0)
                {
                    WaveInEventArgs data = mErrorBufferData.Peek();
                    bool sendStatus = SendAudioData(data);

                    if (sendStatus == false)
                    {
                        break;
                    }
                    Logger.GetInstance().Logging.Info(String.Format("Sending Message In Error Buffer"));
                    mErrorBufferData.Dequeue();
                }
                Logger.GetInstance().Logging.Info(String.Format("Number of Item in Error Buffer {0}", mErrorBufferData.Count));
                if (!SendAudioData(args))
                {
                    byte[] rawData = new byte[args.BytesRecorded];
                    args.Buffer.CopyTo(rawData, 0);
                    mErrorBufferData.Enqueue(new WaveInEventArgs(rawData, rawData.Length));
                }

            }
            if (this.RecordingForm_CB_SaveAudio.Checked)
            {
                if (mAudioWriter != null)
                {
                    mAudioWriter.WriteStream(args.Buffer, args.BytesRecorded);
                }
            }
        }

        #region This Section for audio online recording

        private void WorkerConnectionStateChange(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                Logger.GetInstance().Logging.Warn("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (e.ProgressPercentage == 1)
            {
                Logger.GetInstance().Logging.Warn("Không kết nối được Server.\n" +
                    "Kiểm tra lại cấu hình server dịch");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không kết nối được Server.\n" +
                    "Kiểm tra lại cấu hình server dịch", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (e.ProgressPercentage == 2)
            {
                Logger.GetInstance().Logging.Warn("Không khởi tạo được thiết bị ghi âm.\n" +
                    "Kiểm tra lại cấu hình thiết bị ghi âm");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.ProgressPercentage == 4)
            {
                ///Connection ok.
                InitRecordingGui();
            }
        }


        private void InitRecordingGui()
        {
            mIsCapture = RecordingState.RECORDED;
            BT_Start.Text = "Stop";
            mIsPause = false;
            BT_Pause.Enabled = true;
            BT_Pause.Text = "Pause";
            RT_FullSentence.Text = "";
            mErrorBufferData.Clear();
            CreateTimerCounter();
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void InitRecordingIdle()
        {
            mIsPause = false;
            BT_Start.Text = "Start";
            mIsCapture = RecordingState.IDLE;
            BT_Pause.Enabled = false;
            DestroyTimerCounter();
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
            this.AudioRecording_Tab.Selectable = true;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.GetInstance().Logging.Error(String.Format("Cannot init the session {0}", e.Error));
            ///Close all connection
            if (e.Cancelled)
            {
                ConvertToMp3();
            }
            StopRecording();
            InitRecordingIdle();
        }


        private void StopRecording()
        {
            DestroyAudioRecorder();
            DestroyAudioStreamer();
            CloseSessionInfo();
            DestroyAudioWriter();
        }

        private void StartRecording(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;


            bool isSuccessfull = CreateSessionInfo();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(0);
                Logger.GetInstance().Logging.Warn("Cannot create session info");
                return ;
            }
            isSuccessfull = CreateAudioStreamer();
            if (isSuccessfull == false)
            {
                Logger.GetInstance().Logging.Warn("Cannot create audio streammer");
                worker.ReportProgress(1);
                return;
            }
            isSuccessfull = CreateAudioRecorder();
            if (isSuccessfull == false)
            {
                Logger.GetInstance().Logging.Warn("Cannot create audio recorder");
                worker.ReportProgress(2);
                return;
            }
            isSuccessfull = CreateTimerCounter();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(3);
                return;
            }

            isSuccessfull = CreateAudioWriter();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(5);
                return;
            }

            worker.ReportProgress(4);
            while (worker.CancellationPending == false)
            {
                Thread.Sleep(1000);
            }
            e.Cancel = true;
            return;
        }

        private bool CreateSessionInfo()
        {
            bool isSuccessfull = Request.RequestChangeSessionStatus(mSessionId, 1);
            if (isSuccessfull == false)
            {
                return isSuccessfull;
            }
            isSuccessfull = Request.RequestCreateParentAudio(mSessionId, mAudioName);

            return isSuccessfull;
        }

        private bool CloseSessionInfo()
        {
            bool isSuccessfull = Request.RequestChangeSessionStatus(tb_SessionId.Text, 0);
            return isSuccessfull;
        }

        private bool CreateAudioStreamer()
        {
            // 1. Create Instance
            mAudioStreamer = new AudioStreamer();
            mAudioStreamer.MessageAvailable += OnWebsocketMessage;
            mAudioStreamer.Opened += OnWebsocketConnectionOpened;
            mAudioStreamer.Closed += OnWebsocketConnectionClosed;
            // 2. Create Connection
            mAudioStreamer.StartStreaming(Utils.ParsingWeboscketUrl(tb_SessionId.Text));
            return true;
        }

        private bool CheckCondition()
        {
            //1. Check session ID
            try
            {
                long sessionId = Int64.Parse(tb_SessionId.Text);
                //Request to check the session
            } catch(FormatException e)
            {
                MessageBox.Show("Thông tin phiên không chính xác\n" + e.Message, "Thông tin phiên",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } catch(ArgumentNullException e)
            {
                MessageBox.Show("Thông tin phiên không chính xác\n" + e.Message, "Thông tin phiên",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //2. Check Audio Length
            try
            {
                int sessionId = Int16.Parse(TB_AudioLength.Text);
                //Request to check the session
            }
            catch (FormatException e)
            {
                MessageBox.Show("Độ dài audio không chính xác\n" + e.Message, "Độ dài audio",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (ArgumentNullException e)
            {

                MessageBox.Show("Độ dài audio không chính xác\n" + e.Message, "Độ dài audio",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //3. Check Mic
            if (cb_ListMicIn.SelectedIndex < 0 || cb_ListMicIn.Items.Count <= 0)
            {
                return false;
            }

            //4. Check ten tep
            if (TB_FileName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Tên tệp không được bỏ trống", "Tên tệp",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                Regex check_pattern = new Regex(@"[\[\]/\\!@#$%^&*()]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = check_pattern.Matches(TB_FileName.Text.Trim());

                if (matches.Count > 0)
                {
                    MessageBox.Show("Tên tệp không được chứa các ký tự đặc biệt []/\\!@#$%^&*()", "Tên tệp",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (ValidateExistFile() != true)
            {
                return false;
            }

            return true;
        }

        private bool DestroyAudioStreamer()
        {
            if (mAudioStreamer != null)
            {
                mAudioStreamer.Dispose();
                mAudioStreamer = null;
            }
            return true;
        }

        private bool CreateAudioRecorder()
        {
            if (mAudioRecorder != null)
            {
                DestroyAudioRecorder();
            }

            if (mSelectedMicIndex != -1)
            {
                mAudioRecorder = new Recorder(mSelectedMicIndex);
                mAudioRecorder.RegisterDataAvailable(OnDataAvailable);
                mAudioRecorder.StartRecord();
                return true;
            }
            return false;
        }

        private bool DestroyAudioRecorder()
        {
            if (mAudioRecorder != null )
            {
                mAudioRecorder.Dispose();
                mAudioRecorder = null;
            }

            return true;
        }

        private bool CreateTimerCounter()
        {
            if (mTimerCounter != null)
            {
                mTimerCounter.Dispose();
                mTimerCounter = null;
            }
            mTimerCounter = new System.Windows.Forms.Timer { Interval = 1000 };
            mTimerCounter.Enabled = true;
            mTimerCounter.Tick += new EventHandler(OnTimerEvent);
            mTimeCounter = 0;

            return true;
        }

        private bool DestroyTimerCounter()
        {
            if (mTimerCounter != null)
            {
                mTimerCounter.Dispose();
                mTimerCounter = null;
            }

            return true;
        }


        private bool CreateAudioWriter()
        {
            string audio_path = GetWavAudioName();
            mAudioWriter = new AudioWriter(audio_path);
            return true;
        }

        private void ConvertToMp3()
        {
            string audio_path = GetWavAudioName();
            string mp3Path = GetMp3AudioPath();
            if (mAudioWriter != null)
            {
                mAudioWriter.Dispose(); //Release file 
                mAudioWriter.WavToMP3(audio_path, mp3Path);
            }
        }

        private bool DestroyAudioWriter()
        {
            if (mAudioWriter != null)
            {
                mAudioWriter.Dispose();
                mAudioWriter = null;
            }
            return true;
        }

        private bool ValidateExistFile()
        {
            // Audio path
            string audio_path = GetWavAudioName();
            if (File.Exists(audio_path))
            {
                DialogResult dr = MessageBox.Show("Tệp âm thanh đã tồn tại \n" +
                    "Bạn có muốn ghi đè?", "Hệ thống", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return false;

                if (Directory.Exists(GetAudioChildPath()))
                {
                    foreach (var file in Directory.GetFiles(GetAudioChildPath()))
                    {
                        File.Delete(file);
                    }
                }

                return true;
            }

            if (Directory.Exists(GetAudioChildPath()))
            {
                foreach (var file in Directory.GetFiles(GetAudioChildPath()))
                {
                    File.Delete(file);
                }
            }

            return true;
        }

        public void OnTimerEvent(object source, EventArgs e)
        {
            Logger.GetInstance().Logging.Info(String.Format("Time stamp {0}", mTimeCounter));
            if (mIsPause == false)
            {
                mTimeCounter += 1;
                TimeSpan time = TimeSpan.FromSeconds(mTimeCounter);
                string str = time.ToString(@"hh\:mm\:ss");
                LB_Timer.Text = str;
            }
        }

        private void decorate_volumeter_bar(WaveInEventArgs args)
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

        }

        private void tb_SessionId_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Session Id: {0}", tb_SessionId.Text,"");
            mSessionId = tb_SessionId.Text;
            mSessionId = mSessionId.Trim();
            Debug.WriteLine("Session Id: {0}", mSessionId, "");
        }

        private void tb_SessionId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void OnWebsocketMessage(object sender, AudioStreamTextArgs e)
        {
            if (e.Message.flag == "1")
            {
                RT_FullSentence.Invoke(new UpdateContent(UpdateLongText), e.Message.body);
            } else
            {
                RT_ShortSentence.Invoke(new UpdateContent(UpdateShortText), e.Message.body);
            }
        }

        private void OnWebsocketConnectionOpened(object sender, EventArgs e)
        {
            Debug.WriteLine("Connection ok: {}");
            LB_ConnectionState.Invoke(new DUpdateConnectionStatus(UpdateConectionStatus), true);
            //LB_ConnectionState.BackColor = Color.Green;
        }

        private void OnWebsocketConnectionClosed(object sender, CloseEventArgs e)
        {
            //LB_ConnectionState.BackColor = Color.Red;
            LB_ConnectionState.Invoke(new DUpdateConnectionStatus(UpdateConectionStatus), false);
            Debug.WriteLine("Connection Close: {0}", e.Reason, "");
        }

        private void SendTestWebsocketMessage()
        {
            string test_message = @"{'flag':'0', 'body':'xin chào các bạn nhé xin chào các bạn xin chào các bạn','speaker_type':'1', 'speaker_id': '10', 'unknown_speaker':'1', 'para':'2', 'script_index':'12', 'audio_base64':'0'}";
            mAudioStreamer.T_SendMessage(test_message);
        }

        private bool SendAudioData(WaveInEventArgs args)
        {
            if (mAudioStreamer != null)
            {
                AudioBinaryMessage message = new AudioBinaryMessage();
                message.sessionId = mSessionId;
                message.audio_length = mRecordingTime;
                var by = new byte[44 + args.BytesRecorded];
                System.Buffer.BlockCopy(args.Buffer, 0, by, 44, args.BytesRecorded);
                message.dataFile = Utils.Base64Encode(by, by.Length);
                return mAudioStreamer.SendMessage(message);
            }
            return false;
        }

        //Update GUI function

        private void UpdateListView()
        {
            ListAudioFile.Items.Clear();
            foreach (AudioFileProperty item in mListOfflineAudioFile)
            {
                ListViewItem viewItem = new ListViewItem();
                viewItem.Text = item.FileName;
                viewItem.ImageIndex = 0;
                viewItem.SubItems.Add(item.FileDuration.ToString());
                viewItem.SubItems.Add(item.status.ToString());
                viewItem.SubItems.Add(item.FullFilePath);
                //viewItem.SubItems.Add();
                ListAudioFile.Items.Add(viewItem);
            }
        }
        private void UpdateShortText(string text)
        {
            RT_ShortSentence.Text = text;
            RT_ShortSentence.ScrollToCaret();
        }

        private void UpdateLongText(string text)
        {
            RT_FullSentence.AppendText(String.Format(" {0}",text));
            // scroll it automatically
            RT_FullSentence.ScrollToCaret();
        }

        private void UpdateConectionStatus(bool status)
        {
            if (status == true)
            {
                LB_ConnectionState.BackColor = Color.Green;
            }
            else
            {
                LB_ConnectionState.BackColor = Color.Red;
            }
        }

        private void UpdateMicVolume(float volume)
        {
            VM_VolumeMeter.Amplitude = volume;
        }

        private void LB_Timer_Click(object sender, EventArgs e)
        {

        }

        private void BT_Pause_Click(object sender, EventArgs e)
        {
            if (mIsPause == false)
            {
                BT_Pause.Text = "Resume";
                mIsPause = true;
            }
            else
            {
                mIsPause = false;
                BT_Pause.Text = "Pause";
            }
        }

        private void cb_ListMicIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mSelectedMicIndex = cb_ListMicIn.SelectedIndex;
        }

        private void TB_AudioLength_TextChanged(object sender, EventArgs e)
        {
            mRecordingTime = TB_AudioLength.Text;
        }

        private void TB_FileName_TextChanged(object sender, EventArgs e)
        {
            mAudioName = TB_FileName.Text;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (mIsCapture == RecordingState.IDLE)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn tắt chương trình?", "Hệ thống", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) e.Cancel = true;
            } else if (mIsCapture == RecordingState.RECORDED)
            {
                MessageBox.Show("Không thể đóng khi đang ghi âm\n" +
                    "Kết thúc ghi âm rồi thử lại", "Hệ thống", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            } else if( mIsCapture == RecordingState.STARTING)
            {
                DialogResult dr = MessageBox.Show("Hệ thống đang khởi tạo \nBạn có muốn tắt chương trình?", "Hệ thống", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    StopRecording();
                }
            }
        }

        private void AudioRecordingForms_Load(object sender, EventArgs e)
        {
            // Assign title bar
            Version v = new Version(Application.
            ProductVersion);
            this.Text = String.Format("Audio Recorder And Translator {0}.{1}", v.Major, v.Minor);

            /*foreach (var file in Directory.GetFiles(@"C:\Users\hungnv\Desktop\Test\1"))
            {
                AudioFileProperty audioFile = new AudioFileProperty(file);
                mListOfflineAudioFile.Add(audioFile);
            }
            foreach (AudioFileProperty item in mListOfflineAudioFile)
            {
                ListViewItem viewItem = new ListViewItem();
                viewItem.Text = item.FileName;
                viewItem.ImageIndex = 0;
                viewItem.SubItems.Add(item.FileDuration.ToString());
                viewItem.SubItems.Add(item.FileDuration.ToString());
                viewItem.SubItems.Add(item.FullFilePath);
                //viewItem.SubItems.Add();
                ListAudioFile.Items.Add(viewItem);
            }*/
        }

        private void RefreshDataView(List<AudioFileProperty> list)
        {
            this.ListAudioFile.Invoke(new DUpdateListView(UpdateListView));
        }
        #endregion

        #region This Session for audio offline recording

        public bool InitOfflineRecording()
        {

            return true;
        }

        private void WorkerPercentageStateChange(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                Logger.GetInstance().Logging.Warn("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.ProgressPercentage == 1)
            {
                Logger.GetInstance().Logging.Warn("Không kết nối được Server.\n" +
                    "Kiểm tra lại cấu hình server dịch");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không kết nối được Server.\n" +
                    "Kiểm tra lại cấu hình server dịch", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (e.ProgressPercentage == 2)
            {
                Logger.GetInstance().Logging.Warn("Không khởi tạo được thiết bị ghi âm.\n" +
                    "Kiểm tra lại cấu hình thiết bị ghi âm");
                ///Connection errrors. Close all connection
                MessageBox.Show("Không thể khởi tạo thông tin phiên\n" +
                    "Tạo phiên mới hoặc xóa thông tin cũ và thử lại", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.ProgressPercentage == 4)
            {
                ///Connection ok.
                InitRecordingGui();
            }
        }
        private void worker_RunWorkerOfflineCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.GetInstance().Logging.Error(String.Format("Cannot init the session {0}", e.Error));
            ///Close all connection
            if (e.Cancelled)
            {
                ConvertToMp3();
            }

            StopOfflineRecording();
            InitRecordingIdle();
        }

        private bool CreateOfflineSessionInfo()
        {
            //bool isSuccessfull = Request.RequestChangeSessionStatus(mSessionId, 1);
            bool isSuccessfull = Request.CreateOfflineSession(mSessionId, mAudioName + ".mp3");
            if (isSuccessfull == false)
            {
                return isSuccessfull;
            }
            

            return isSuccessfull;
        }

        private bool CloseOfflineSession()
        {
            bool isSuccessfull = Request.RequestChangeSessionStatus(tb_SessionId.Text, 0);
            return isSuccessfull;
        }

        private void OnOfflineDataAvailable(object sender, WaveInEventArgs args)
        {
            if (mIsPause == false)
            {
                decorate_volumeter_bar(args);
                //Send Test message
                //Send Audio to Websocket Client
                lock (balanceLock)
                {
                    byte[] rawAudio = new byte[args.BytesRecorded];
                    args.Buffer.CopyTo(rawAudio,0);
                    mRawAudioData.Enqueue(new WaveInEventArgs(rawAudio, args.BytesRecorded));
                    mRawAudioLength += args.BytesRecorded;
                }
            }
            if (this.RecordingForm_CB_SaveAudio.Checked)
            {
                if (mAudioWriter != null)
                {
                    mAudioWriter.WriteStream(args.Buffer, args.BytesRecorded);
                }
            }
        }

        private bool CreateOfflineAudioRecorder()
        {
            if (mAudioRecorder != null)
            {
                DestroyOfflineAudioRecorder();
            }

            if (mSelectedMicIndex != -1)
            {
                mAudioRecorder = new Recorder(mSelectedMicIndex);
                mAudioRecorder.RegisterDataAvailable(OnOfflineDataAvailable);
                mAudioRecorder.StartRecord();
                return true;
            }
            return false;
        }

        private bool DestroyOfflineAudioRecorder()
        {
            if (mAudioRecorder != null)
            {
                mAudioRecorder.Dispose();
                mAudioRecorder = null;
            }

            return true;
        }

        private void StartOfflineRecording(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            bool isSuccessfull = CreateOfflineSessionInfo();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(0);
                Logger.GetInstance().Logging.Warn("Cannot create session info");
                return;
            }
            isSuccessfull = CreateOfflineAudioRecorder();
            if (isSuccessfull == false)
            {
                Logger.GetInstance().Logging.Warn("Cannot create audio recorder");
                worker.ReportProgress(2);
                return;
            }
            isSuccessfull = CreateTimerCounter();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(3);
                return;
            }

            isSuccessfull = CreateAudioWriter();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(5);
                return;
            }

            worker.ReportProgress(4);
            while (worker.CancellationPending == false)
            {
                ProcessRawAudioData();
                Thread.Sleep(1000);
            }

            ProcessRawAudioData(true);
            e.Cancel = true;
            return;
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
                path = SaveAllAudioFromBufferFile();
            }
            //2. Save to wav file and Put in list audio buffer

            if (path != null)
            {
                AudioFileProperty mp3File = new AudioFileProperty(path);

                mListOfflineAudioFile.Add(mp3File);
            }
            //4. Send to backend server
            sendAudioFileToBackend();

            //5. Update View
            RefreshDataView(mListOfflineAudioFile);
        }

        private void sendAudioFileToBackend()
        {
            double startSplit = 0;
            for (int i = 0; i < mListOfflineAudioFile.Count; ++i )
            {
                AudioFileProperty file = mListOfflineAudioFile[i];

                if (file.status == AudioFileProperty.TranslateStatus.Not_Sent)
                {
                    bool sendResult = Request.UploadFileToServer(mSessionId, file.FullFilePath, startSplit);
                    if (sendResult == false)
                    {
                        break;
                    }
                    else
                    {
                        file.status = AudioFileProperty.TranslateStatus.Sent;
                        mListOfflineAudioFile[i] = file;
                    }
                }
                startSplit += file.DurationInSecond;
            }
        }

        private bool IsEnoughData()
        {
            double audioLengthInMinute = mRawAudioLength / (16000 * 2 * 60);

            return audioLengthInMinute >= Int16.Parse(mRecordingTime) ? true : false;
        }

        private string SaveAllAudioFromBufferFile()
        {
            lock (balanceLock)
            {
                if (mRawAudioData.Count < 20)
                {
                    mRawAudioData.Clear();
                    mRawAudioLength = 0;
                    return null;
                }
            }
            try
            {
                string fileName = GetWavAudioChildName((mListOfflineAudioFile.Count + 1).ToString());
                AudioWriter wavWriter = new AudioWriter(fileName);
                lock (balanceLock)
                {
                    while (mRawAudioData.Count > 0)
                    {
                        WaveInEventArgs rawData = mRawAudioData.Dequeue();
                        wavWriter.WriteStream(rawData.Buffer, rawData.BytesRecorded);
                    }
                    mRawAudioLength = 0;
                }
                wavWriter.Dispose();
                string mp3FileName = GetMp3AudioChildPath((mListOfflineAudioFile.Count + 1).ToString());
                wavWriter.WavToMP3(fileName, mp3FileName);
                //Remove wavfile
                File.Delete(fileName);
                return mp3FileName;
            }
            catch
            {
                return null;
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
                int remainAudioLength = 0;
                AudioWriter wavWriter = new AudioWriter(fileName);
                lock (balanceLock)
                {
                    while (saveAudioLengthInSecond < Int16.Parse(mRecordingTime) * 60 && mRawAudioData.Count > 0)
                    {
                        WaveInEventArgs rawData = mRawAudioData.Dequeue();
                        if (saveAudioLengthInSecond >= (Int16.Parse(mRecordingTime) * 60 - 3))
                        {
                            remainRawData.Enqueue(rawData);
                            remainAudioLength += rawData.BytesRecorded;
                        }

                        wavWriter.WriteStream(rawData.Buffer, rawData.BytesRecorded);

                        savedAudioLengthInArrayLength += rawData.BytesRecorded;
                        saveAudioLengthInSecond = savedAudioLengthInArrayLength / (16000 * 2);
                    }

                    while (mRawAudioData.Count > 0)
                    {
                        remainRawData.Enqueue(mRawAudioData.Dequeue());
                    }
                    mRawAudioData = remainRawData;
                    mRawAudioLength = remainAudioLength;
                }
                wavWriter.Dispose();
                string mp3FileName = GetMp3AudioChildPath((mListOfflineAudioFile.Count + 1).ToString());
                wavWriter.WavToMP3(fileName, mp3FileName);
                //Remove wavfile
                File.Delete(fileName);
                return mp3FileName;
            }
            catch
            {
                return null;
            }
        }

        private void StopOfflineRecording()
        {
            DestroyOfflineAudioRecorder();
            CloseOfflineSession();
            DestroyAudioWriter();
            DestroyTimerCounter();
        }

        #endregion

        #region Common Function
        private string GetAudioPath()
        {
            string path = Path.Combine(AppsSettings.GetInstance().DataDir);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private string GetAudioChildPath()
        {
            string path = Path.Combine(AppsSettings.GetInstance().DataDir, mAudioName);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private string GetWavAudioName(string name = "")
        {
            if (name.IsNullOrEmpty())
            {
                return Path.Combine(GetAudioPath(), mAudioName + ".wav");
            } else
            {
                return Path.Combine(GetAudioPath(), name + ".wav");
            }
        }

        private string GetMp3AudioPath(string name = "")
        {
            if (name.IsNullOrEmpty())
            {
                return Path.Combine(GetAudioPath(), mAudioName + ".mp3");
            }
            else
            {
                return Path.Combine(GetAudioPath(), name + ".mp3");
            }
        }

        private string GetWavAudioChildName(string name = "")
        {
            if (name.IsNullOrEmpty())
            {
                return Path.Combine(GetAudioChildPath(), mAudioName + ".wav");
            }
            else
            {
                return Path.Combine(GetAudioChildPath(), name + ".wav");
            }
        }

        private string GetMp3AudioChildPath(string name = "")
        {
            if (name.IsNullOrEmpty())
            {
                return Path.Combine(GetAudioChildPath(), mAudioName + ".mp3");
            }
            else
            {
                return Path.Combine(GetAudioChildPath(), name + ".mp3");
            }
        }

        #endregion

        private void ListAudioFile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
