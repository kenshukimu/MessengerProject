namespace Kyobo_Msg_Server
{
    partial class ServerMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerMain));
            this.lvMainControl = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.recvBox = new System.Windows.Forms.RichTextBox();
            this.lv_roomList = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.lab_presentCnt = new System.Windows.Forms.Label();
            this.btn_ServerStart = new System.Windows.Forms.Button();
            this.serverStartTime1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serverStartTime2 = new System.Windows.Forms.Label();
            this.lab_ip = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lab_port = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMainControl
            // 
            this.lvMainControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvMainControl.GridLines = true;
            this.lvMainControl.HideSelection = false;
            this.lvMainControl.Location = new System.Drawing.Point(12, 82);
            this.lvMainControl.Name = "lvMainControl";
            this.lvMainControl.Size = new System.Drawing.Size(445, 328);
            this.lvMainControl.TabIndex = 0;
            this.lvMainControl.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "로그인ID";
            this.columnHeader0.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "연결상태";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "아이피";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "접속시간";
            this.columnHeader3.Width = 200;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 479);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(474, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl
            // 
            this.tssl.Name = "tssl";
            this.tssl.Size = new System.Drawing.Size(110, 17);
            this.tssl.Text = "Server Stop Listen!!";
            // 
            // recvBox
            // 
            this.recvBox.Location = new System.Drawing.Point(12, 416);
            this.recvBox.Name = "recvBox";
            this.recvBox.Size = new System.Drawing.Size(49, 32);
            this.recvBox.TabIndex = 3;
            this.recvBox.Text = "";
            this.recvBox.Visible = false;
            // 
            // lv_roomList
            // 
            this.lv_roomList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7});
            this.lv_roomList.GridLines = true;
            this.lv_roomList.HideSelection = false;
            this.lv_roomList.Location = new System.Drawing.Point(67, 416);
            this.lv_roomList.Name = "lv_roomList";
            this.lv_roomList.Size = new System.Drawing.Size(45, 28);
            this.lv_roomList.TabIndex = 4;
            this.lv_roomList.UseCompatibleStateImageBehavior = false;
            this.lv_roomList.Visible = false;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "방번호";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(216, 425);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "현재접속인원 :";
            // 
            // lab_presentCnt
            // 
            this.lab_presentCnt.AutoSize = true;
            this.lab_presentCnt.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lab_presentCnt.ForeColor = System.Drawing.Color.White;
            this.lab_presentCnt.Location = new System.Drawing.Point(324, 425);
            this.lab_presentCnt.Name = "lab_presentCnt";
            this.lab_presentCnt.Size = new System.Drawing.Size(18, 19);
            this.lab_presentCnt.TabIndex = 6;
            this.lab_presentCnt.Text = "-";
            // 
            // btn_ServerStart
            // 
            this.btn_ServerStart.BackgroundImage = global::Kyobo_Msg.Properties.Resources.start_server;
            this.btn_ServerStart.Location = new System.Drawing.Point(165, 12);
            this.btn_ServerStart.Name = "btn_ServerStart";
            this.btn_ServerStart.Size = new System.Drawing.Size(292, 64);
            this.btn_ServerStart.TabIndex = 2;
            this.btn_ServerStart.UseVisualStyleBackColor = true;
            this.btn_ServerStart.Click += new System.EventHandler(this.btn_ServerStart_Click);
            // 
            // serverStartTime1
            // 
            this.serverStartTime1.AutoSize = true;
            this.serverStartTime1.Font = new System.Drawing.Font("나눔스퀘어라운드 Regular", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.serverStartTime1.ForeColor = System.Drawing.Color.White;
            this.serverStartTime1.Location = new System.Drawing.Point(14, 39);
            this.serverStartTime1.Name = "serverStartTime1";
            this.serverStartTime1.Size = new System.Drawing.Size(12, 14);
            this.serverStartTime1.TabIndex = 8;
            this.serverStartTime1.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "서버시작시간 :";
            // 
            // serverStartTime2
            // 
            this.serverStartTime2.AutoSize = true;
            this.serverStartTime2.Font = new System.Drawing.Font("나눔스퀘어라운드 Regular", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.serverStartTime2.ForeColor = System.Drawing.Color.White;
            this.serverStartTime2.Location = new System.Drawing.Point(12, 61);
            this.serverStartTime2.Name = "serverStartTime2";
            this.serverStartTime2.Size = new System.Drawing.Size(12, 14);
            this.serverStartTime2.TabIndex = 9;
            this.serverStartTime2.Text = "-";
            // 
            // lab_ip
            // 
            this.lab_ip.AutoSize = true;
            this.lab_ip.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lab_ip.ForeColor = System.Drawing.Color.White;
            this.lab_ip.Location = new System.Drawing.Point(73, 455);
            this.lab_ip.Name = "lab_ip";
            this.lab_ip.Size = new System.Drawing.Size(18, 19);
            this.lab_ip.TabIndex = 11;
            this.lab_ip.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 455);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "서버IP :";
            // 
            // lab_port
            // 
            this.lab_port.AutoSize = true;
            this.lab_port.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lab_port.ForeColor = System.Drawing.Color.White;
            this.lab_port.Location = new System.Drawing.Point(309, 455);
            this.lab_port.Name = "lab_port";
            this.lab_port.Size = new System.Drawing.Size(18, 19);
            this.lab_port.TabIndex = 12;
            this.lab_port.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(216, 455);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "서버PORT :";
            // 
            // ServerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(474, 501);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lab_port);
            this.Controls.Add(this.lab_ip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serverStartTime2);
            this.Controls.Add(this.serverStartTime1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lab_presentCnt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lv_roomList);
            this.Controls.Add(this.recvBox);
            this.Controls.Add(this.btn_ServerStart);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvMainControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerMain";
            this.Text = "교보채팅서버";
            this.Load += new System.EventHandler(this.FrmMainControl_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMainControl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl;
        private System.Windows.Forms.Button btn_ServerStart;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.RichTextBox recvBox;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lv_roomList;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_presentCnt;
        private System.Windows.Forms.Label serverStartTime1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label serverStartTime2;
        private System.Windows.Forms.Label lab_ip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lab_port;
        private System.Windows.Forms.Label label6;
    }
}

