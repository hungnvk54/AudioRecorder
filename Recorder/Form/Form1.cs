using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using WebSocketSharp;

namespace Recorder
{
    public partial class Form1 : Form
    {
        private enum RecordingState
        {
            IDLE,
            STARTING,
            RECORDED
        }
        private RecordingState mIsCapture = RecordingState.IDLE;
        private System.Windows.Forms.Timer mTimerCounter = null;
        private long mTimeCounter = 0;
        private bool mIsPause = false;
        private int mSelectedMicIndex = -1;

        private AudioStreamer mAudioStreamer = null;
        private Recorder mAudioRecorder = null;

        /// Session Info
        private string mSessionId;
        private string mRecordingTime;
        private string mAudioName;

        private readonly BackgroundWorker mBackgroundWorker;

        //Delegate
        public delegate void UpdateContent(string content);
        public delegate void DUpdateConnectionStatus(bool status);
        public delegate void DUpdateMicValueBar(float volume);
        //Websocket 
        private Queue<WaveInEventArgs> mErrorBufferData;
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                cb_ListMicIn.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            }

            mErrorBufferData = new Queue<WaveInEventArgs>();

            cb_ListMicIn.SelectedIndex = Int16.Parse(Settings.GetInstance().DefaultMic);
            TB_AudioLength.Text = Settings.GetInstance().DefaultAudioLength;

            mBackgroundWorker = new BackgroundWorker();
            mBackgroundWorker.WorkerSupportsCancellation = true;
            mBackgroundWorker.DoWork += StartRecording;
            mBackgroundWorker.WorkerReportsProgress = true;
            mBackgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            mBackgroundWorker.ProgressChanged += WorkerConnectionStateChange;
        }

        private void WorkerConnectionStateChange(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                Logger.GetInstance().Logging.Warn("Init system failed");
                ///Connection errrors. Close all connection
                MessageBox.Show("Khởi tạo hệ thống không thành công", "Thông tin phiên",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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
        }

        private void InitRecordingIdle()
        {
            mIsPause = false;
            BT_Start.Text = "Start";
            mIsCapture = RecordingState.IDLE;
            BT_Pause.Enabled = false;
            DestroyTimerCounter();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.GetInstance().Logging.Error(String.Format("Cannot init the session {0}", e.Error));
            ///Close all connection
            StopRecording();
            InitRecordingIdle();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Open form to setting the url
            var setting = new SettingForm();
            setting.ShowDialog();
        }

        private void BT_Start_Click(object sender, EventArgs e)
        {
            //1. Check the current state
            if (mIsCapture == RecordingState.RECORDED)
            {
                //Stop capture here
                if (mBackgroundWorker.IsBusy)
                {
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
                bool condition = CheckCondition();
                if (condition == true)
                {
                    mBackgroundWorker.RunWorkerAsync();
                    mIsCapture = RecordingState.STARTING;
                }
            }

        }

        private void StopRecording()
        {
            DestroyAudioRecorder();
            DestroyAudioStreamer();
            CloseSessionInfo();
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
                worker.ReportProgress(0);
                return;
            }
            isSuccessfull = CreateAudioRecorder();
            if (isSuccessfull == false)
            {
                Logger.GetInstance().Logging.Warn("Cannot create audio recorder");
                worker.ReportProgress(0);
                return;
            }
            isSuccessfull = CreateTimerCounter();
            if (isSuccessfull == false)
            {
                worker.ReportProgress(0);
                return;
            }
            worker.ReportProgress(1);
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
                MessageBox.Show("Thông tin phiên không chính xác", "Thông tin phiên",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } catch(ArgumentNullException e)
            {
                MessageBox.Show("Thông tin phiên không chính xác", "Thông tin phiên",
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
                MessageBox.Show("Độ dài audio không chính xác", "Độ dài audio",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (ArgumentNullException e)
            {

                MessageBox.Show("Độ dài audio không chính xác", "Độ dài audio",
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
                    mErrorBufferData.Enqueue(args);
                }

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
    }
}
