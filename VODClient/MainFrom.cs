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
using NetFramework.AsyncSocketClient;
using Common;



namespace VODClient
{
    public interface IClientPlayer
    {
        void BindList(IList<Song> list);
        void BindPlayList(IList<Song> list);

        void SetImportEnable();

        void ClearPlayList();

    }

    public partial class MainFrom : Form, IClientPlayer
    {
        IClientScoket _client = new IClientScoket();
        public MainFrom()
        {   
            InitializeComponent();

            tsslVersion.Text = "Version:"+Application.ProductVersion;

           
            _client.ReceivedDatagram += client_ReceivedDatagram;
            _client.OnDisconnected += _client_OnDisconnected;
            _client.OnConnected += _client_OnConnected;
            this.dgvSearchResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResult.CellDoubleClick += dgvSearchResult_CellDoubleClick;


            this.btnSearch.Enabled = false;
            this.btnImport.Enabled = false;
            //this.lvwSearchResult.BeginUpdate();

            this.dgvSearchResult.DataSource = new List<Song>();
            SetDgvListColumnStyle(dgvSearchResult);

            this.dgvPlayList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayList.CellDoubleClick += dgvPlayList_CellDoubleClick;

        }

        void dgvPlayList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvPlayList.Rows[e.RowIndex];
            var value = row.Cells[0].Value.ToString();
            _client.Send(NetCommand.PLAY, value);
        }

        void dgvSearchResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvSearchResult.Rows[e.RowIndex];
            var value = row.Cells[0].Value.ToString();
            _client.Send(NetCommand.PLAYLISTADD, value);
        }

        void _client_OnConnected(object sender, DSCClientConnectedEventArgs e)
        {
            this.Invoke(new Action(() => { 
                this.btnSearch.Enabled = true;
                this.btnImport.Enabled = true;
            }));
        }

        void _client_OnDisconnected(object sender, DSCClientConnectedEventArgs e)
        {
            this.Invoke(new Action(() => {
                this.btnConnect.Enabled = true;
                this.btnImport.Enabled = false;
            }));
        }

        void client_ReceivedDatagram(object sender, DSCClientDataInEventArgs e)
        {
            byte[] buffer = e.Data;
            var message = System.Text.UTF8Encoding.Default.GetString(buffer);
            CommandHelper.Excute(message, this);
            var cmd = message.Substring(0, 7);
            if (cmd == NetCommand.REFRESH_DATA)
            {
                this.Invoke(new Action(() =>
                {
                    this.btnImport.Enabled = true;
                }));
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!_client.IsConnected)
            {
                _client.Send(System.Text.UTF8Encoding.Default.GetBytes(NetCommand.NEXT));
            }
            else
            {
                MessageBox.Show("未连接至服务器");
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
           
            var ip = this.txtBoxIp.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("请输入要链接的IP地址");
                return;
            }
            this.btnConnect.Enabled = false;

            _client.Connect(ip, 2112);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var txt = txtSearch.Text.Trim();
            _client.Send(NetCommand.SEARCH, txt);
        }





        public void BindList(IList<Song> list)
        {
            this.Invoke(new Action<IList<Song>>(s =>
            {
                this.dgvSearchResult.DataSource = null;
                this.dgvSearchResult.DataSource = s;
                SetDgvListColumnStyle(dgvSearchResult);

            }), list);
        }

        private void SetDgvListColumnStyle(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(222, 231, 222);

            if (dgv.Columns.Contains("ID"))
            {
                dgv.Columns["ID"].Width = 50;
                dgv.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgv.Columns.Contains("Name"))
            {
                dgv.Columns["Name"].MinimumWidth = 100;
                dgv.Columns["Name"].Width = 200;
                dgv.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            if (dgv.Columns.Contains("Singer"))
            {
                dgv.Columns["Singer"].Width = 100;
                dgv.Columns["Singer"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void BindPlayList(IList<Song> list)
        {
            this.Invoke(new Action<IList<Song>>(s => {
                this.dgvPlayList.DataSource = null;
                this.dgvPlayList.DataSource = s;
                SetDgvListColumnStyle(dgvPlayList);               

            }),list);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.btnImport.Enabled = false;
            _client.Send(NetCommand.REFRESH_DATA, string.Empty);
        }


        public void SetImportEnable()
        {
            this.Invoke(new Action(() => { this.btnImport.Enabled = true; }));
        }


        public void ClearPlayList()
        {
            this.Invoke(new Action(() =>
            {
                this.dgvPlayList.DataSource = null;
            }));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void MainFrom_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

    }//end class
}
