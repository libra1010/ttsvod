using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetFramework;
using NetFramework.AsyncSocketServer;
using NAudio.Wave;
using System.Diagnostics;
using Common;

namespace NetVOD
{
    public partial class FromMain : Form, IPlayer
    {

        private IWavePlayer wavePlayer;
        private AudioFileReader file;
        private string fileName;
        private IServerSocket _server;

        private SongCollection _collection;


        private IList<string> _conections = new List<string>();

        public FromMain()
        {


            InitializeComponent();
            EnableButtons(false);
            //CheckForIllegalCrossThreadCalls = false;
            PopulateOutputDriverCombo();

            _collection = new SongCollection(this);
            _collection.CollectionChanged += _collection_CollectionChanged;

            _server = new IServerSocket(10, 1024 * 4);
            _server.Init();
            _server.Start("192.168.1.216", 2112);
            _server.OnClientConnect += _server_OnClientConnect;
            _server.OnClientRead += _server_OnClientRead;
            _server.OnClientDisconnect += _server_OnClientDisconnect;

            

            this.Disposed += new EventHandler(SimplePlaybackPanel_Disposed);
            this.timer1.Interval = 250;
            this.timer1.Tick += new EventHandler(timer1_Tick);

        }


        void _collection_CollectionChanged(object sender, EventArgs e)
        {
            foreach (var item in _conections)
            {
                _server.Send(item, NetCommand.PLAYLIST, Newtonsoft.Json.JsonConvert.SerializeObject(_collection));
            }
        }


        void _server_OnClientDisconnect(object sender, AsyncUserToken e)
        {
            for (int i = 0; i < _conections.Count; i++)
            {
                if (_conections[i] == e.ConnectionId)
                {
                    _conections.RemoveAt(i);
                    break;
                }
            }
        }

        void _server_OnClientConnect(object sender, AsyncUserToken e)
        {
            _conections.Add(e.ConnectionId);
            //发送同步歌单
            var command = NetCommand.PLAYLIST + Newtonsoft.Json.JsonConvert.SerializeObject(_collection);
            var buffer = System.Text.UTF8Encoding.Default.GetBytes(command);
            e.Socket.Send(buffer);
        }

        void _server_OnClientRead(object sender, AsyncUserToken e)
        {
            byte[] buffer = new byte[e.BytesReceived];
            Array.Copy(e.ReceiveBuffer, e.Offset, buffer, 0, e.BytesReceived);

            var message = System.Text.UTF8Encoding.Default.GetString(buffer);
            System.Diagnostics.Debug.WriteLine("Message:{0}", message);
            if (message.Substring(0,7) == NetCommand.SEARCH)
            {
                this.Search(message.Substring(7), e);   
            }
            CommandHelper.Excute(message, this);
            if (message.Substring(0, 7) == NetCommand.REFRESH_DATA)
            {
                _server.Send(e.ConnectionId, NetCommand.REFRESH_DATA, string.Empty);
            }

        }



        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}", (int)ts.TotalMinutes, ts.Seconds);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (file != null)
            {
                labelNowTime.Text = FormatTimeSpan(file.CurrentTime);
                labelTotalTime.Text = FormatTimeSpan(file.TotalTime);
            }
        }

        void SimplePlaybackPanel_Disposed(object sender, EventArgs e)
        {
            CleanUp();
        }

        private void PopulateOutputDriverCombo()
        {
            comboBoxOutputDriver.Items.Add("WaveOut Window Callbacks");
            comboBoxOutputDriver.Items.Add("WaveOut Function Callbacks");
            comboBoxOutputDriver.Items.Add("WaveOut Event Callbacks");
            comboBoxOutputDriver.SelectedIndex = 0;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (fileName == null) fileName = SelectInputFile();
            if (fileName != null)
            {
                BeginPlayback(fileName);
            }
        }

        private static string SelectInputFile()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }

            return null;
        }



        private void BeginPlayback(string filename)
        {
            //Debug.Assert(this.wavePlayer == null);
            this.wavePlayer = CreateWavePlayer();
            this.file = new AudioFileReader(filename);
            this.file.Volume = volumeSlider1.Volume;
            this.wavePlayer.Init(file);
            this.wavePlayer.PlaybackStopped += wavePlayer_PlaybackStopped;
            this.wavePlayer.Play();
            EnableButtons(true);
            timer1.Enabled = true; // timer for updating current time label
        }

        private int GetIndex()
        {
            return comboBoxOutputDriver.SelectedIndex;
        }

        private IWavePlayer CreateWavePlayer()
        {
            Func<int> func = GetIndex;
            switch ((int)comboBoxOutputDriver.Invoke(func))
            {
                case 2:
                    return new WaveOutEvent();
                case 1:
                    return new WaveOut(WaveCallbackInfo.FunctionCallback());
                case 0:
                default:
                    return new WaveOut();
            }
        }

        private void EnableButtons(bool playing)
        {
            buttonPlay.Enabled = !playing;
            buttonStop.Enabled = playing;
            buttonOpen.Enabled = !playing;
        }

        void wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            Debug.Assert(!this.InvokeRequired, "PlaybackStopped on wrong thread");
            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            CleanUp();
            EnableButtons(false);
            timer1.Enabled = false;
            labelNowTime.Text = "00:00";
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
            this.Next();
        }

        private void CleanUp()
        {
            if (this.file != null)
            {
                this.file.Dispose();
                this.file = null;
            }
            if (this.wavePlayer != null)
            {
                this.wavePlayer.Dispose();
                this.wavePlayer = null;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.wavePlayer.Stop();
            // don't set button states now, we'll wait for our PlaybackStopped to come
        }

        private void volumeSlider1_VolumeChanged(object sender, EventArgs e)
        {
            if (this.file != null)
            {
                this.file.Volume = volumeSlider1.Volume;
            }
        }




        private void buttonOpen_Click(object sender, EventArgs e)
        {
            fileName = SelectInputFile();
        }

        private void OnMp3RepositionTestClick(object sender, EventArgs e)
        {
            var filename = SelectInputFile();
            if (filename == null) return;
            var wo = new WaveOut();
            var af = new AudioFileReader(filename);
            wo.Init(af);
            var f = new Form();
            var b = new Button() { Text = "Play" };
            b.Click += (s, a) => wo.Play();
            var b2 = new Button() { Text = "Stop", Left = b.Right };
            b2.Click += (s, a) => wo.Stop();
            var b3 = new Button { Text = "Rewind", Left = b2.Right };
            b3.Click += (s, a) => af.Position = 0;
            f.FormClosed += (s, a) => { wo.Dispose(); af.Dispose(); };
            f.Controls.Add(b);
            f.Controls.Add(b2);
            f.Controls.Add(b3);
            f.ShowDialog(this);
        }






        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.Prev();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Next();
        }


        public void Next()
        {
            Action ac = () =>
            {
                CleanUp();
                var song = _collection.Next();
                if (song != null)
                {
                    var path = DataHelper.GetPath(song.ID);
                    BeginPlayback(path);
                }
            };
            this.Invoke(ac);
        }

        public void Prev()
        {
            Action ac = () =>
            {
                CleanUp();
                var song = _collection.Prev();
                if (song != null)
                {
                    var path = DataHelper.GetPath(song.ID);
                    BeginPlayback(path);
                }
            };
            this.Invoke(ac);
        }


        public void Search(string search,AsyncUserToken e)
        {
           var list =  DataHelper.Search(search);
           _server.Send(e.ConnectionId, NetCommand.LIST, Newtonsoft.Json.JsonConvert.SerializeObject(list)); 
            
        }




        public void AddToPlayList(int id)
        {
            var song = DataHelper.GetById(id);
            if (song != null)
            {
                var any = _collection.Any(s => s.ID == id);
                if (!any)
                {
                    _collection.Add(song);
                }
            }
            this.Invoke(new Action(() => {
                this.dgvPlayList.DataSource = null;
                this.dgvPlayList.DataSource = _collection;
            }));
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = true;
            //fileDialog.Title = "请选择文件";
            //fileDialog.Filter = "MP3(*.mp3)|*.mp3";

            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string file = fileDialog.FileName;

            //    var pi = file.Replace(@"\\",@"\").LastIndexOf(@"\");
            //    var pe = file.LastIndexOf(".");

            //    var name = file.Substring(pi, pe - pi).Replace(@"\\", string.Empty);

            //    Mp3FileInfo mp3 = new Mp3FileInfo(file);
            //    var info = mp3.GetMp3();
                
            //    MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
            this.btnImport.Enabled = false;
            RefreshData();
            this.btnImport.Enabled = true;
            
        }

        

        public void RefreshData()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["path"];
            var files =  System.IO.Directory.GetFiles(path);

            this.Invoke(new Action(() => { this.label3.Text = "导入信息：开始导入"; }));

            foreach (var item in files)
            {
                var arry = item.Split('.');
                if (arry.Length < 2)
                {
                    return;
                }
                var subx = arry[1];
                string file = item;
                var pi = file.Replace(@"\\", @"\").LastIndexOf(@"\");
                var pe = file.LastIndexOf(".");
                var name = file.Substring(pi, pe - pi).Replace("\\", string.Empty);

                if (subx.Equals("MP3",StringComparison.CurrentCultureIgnoreCase))
                {
                    DataHelper.Add(name,item);
                    this.Invoke(new Action(() => { this.label3.Text = string.Format("导入信息：导入歌曲：{0} 完成", name); }));
                    
                }
            }
            this.Invoke(new Action(() => { this.label3.Text = "导入信息：全部完成"; }));
            
        }



        public void Play(int id)
        {
            Action ac = () =>
            {
                CleanUp();
                var song = _collection.GetById(id);
                if (song != null)
                {
                    var path = DataHelper.GetPath(song.ID);
                    BeginPlayback(path);
                }
            };
            this.Invoke(ac);
        }

        private void btnClearPlayList_Click(object sender, EventArgs e)
        {

            ClearPlayList();
            this.Invoke(new Action(() => {
                this.dgvPlayList.DataSource = null;
            }));
        }


        public void ClearPlayList()
        {
            this._collection.Clear();

            foreach (var item in _conections)
            {
                _server.Send(item, NetCommand.CLEAR_PLAYLIST, string.Empty);
            }
        }


        public void RemovePlayList(int id)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                if (_collection[i].ID == id)
                {
                    _collection.RemoveAt(i);
                    return;
                }
            }
        }
    }


        

   
}
