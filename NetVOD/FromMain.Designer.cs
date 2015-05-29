namespace NetVOD
{
    partial class FromMain
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.comboBoxOutputDriver = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.labelNowTime = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvPlayList = new System.Windows.Forms.DataGridView();
            this.btnImport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClearPlayList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(749, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is an experimental WORK IN PROGRESS. Will demo use of new AudioFileReader cl" +
    "ass and used to debug PlaybackStopped event";
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(91, 29);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 21);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(173, 28);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 21);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // comboBoxOutputDriver
            // 
            this.comboBoxOutputDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputDriver.FormattingEnabled = true;
            this.comboBoxOutputDriver.Location = new System.Drawing.Point(128, 74);
            this.comboBoxOutputDriver.Name = "comboBoxOutputDriver";
            this.comboBoxOutputDriver.Size = new System.Drawing.Size(227, 20);
            this.comboBoxOutputDriver.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "For Debug Purposes";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(10, 29);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 21);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // labelNowTime
            // 
            this.labelNowTime.AutoSize = true;
            this.labelNowTime.Location = new System.Drawing.Point(436, 37);
            this.labelNowTime.Name = "labelNowTime";
            this.labelNowTime.Size = new System.Drawing.Size(35, 12);
            this.labelNowTime.TabIndex = 6;
            this.labelNowTime.Text = "00:00";
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.Location = new System.Drawing.Point(476, 37);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(35, 12);
            this.labelTotalTime.TabIndex = 6;
            this.labelTotalTime.Text = "00:00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(385, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 21);
            this.button1.TabIndex = 7;
            this.button1.Text = "MP3 Reposition";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnMp3RepositionTestClick);
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Location = new System.Drawing.Point(307, 34);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(96, 15);
            this.volumeSlider1.TabIndex = 5;
            this.volumeSlider1.VolumeChanged += new System.EventHandler(this.volumeSlider1_VolumeChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(18, 145);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 8;
            this.btnPrev.Text = "Prve";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(128, 145);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 432);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(757, 150);
            this.dataGridView1.TabIndex = 9;
            // 
            // dgvPlayList
            // 
            this.dgvPlayList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayList.Location = new System.Drawing.Point(18, 215);
            this.dgvPlayList.Name = "dgvPlayList";
            this.dgvPlayList.RowTemplate.Height = 23;
            this.dgvPlayList.Size = new System.Drawing.Size(757, 150);
            this.dgvPlayList.TabIndex = 9;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(247, 144);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "导入信息";
            // 
            // btnClearPlayList
            // 
            this.btnClearPlayList.Location = new System.Drawing.Point(403, 143);
            this.btnClearPlayList.Name = "btnClearPlayList";
            this.btnClearPlayList.Size = new System.Drawing.Size(136, 23);
            this.btnClearPlayList.TabIndex = 12;
            this.btnClearPlayList.Text = "ClearPlayList";
            this.btnClearPlayList.UseVisualStyleBackColor = true;
            this.btnClearPlayList.Click += new System.EventHandler(this.btnClearPlayList_Click);
            // 
            // FromMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 594);
            this.Controls.Add(this.btnClearPlayList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dgvPlayList);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelTotalTime);
            this.Controls.Add(this.labelNowTime);
            this.Controls.Add(this.volumeSlider1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxOutputDriver);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.label1);
            this.Name = "FromMain";
            this.Text = "Server";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ComboBox comboBoxOutputDriver;
        private System.Windows.Forms.Label label2;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label labelNowTime;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dgvPlayList;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClearPlayList;

    }
}

