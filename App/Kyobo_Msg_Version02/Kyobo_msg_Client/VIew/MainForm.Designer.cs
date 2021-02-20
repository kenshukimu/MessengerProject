namespace Kyobo_Msg_Client
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rb2 = new System.Windows.Forms.RichTextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_UserInfo = new System.Windows.Forms.Button();
            this.btn_message_mgs = new System.Windows.Forms.Button();
            this.lab_name = new System.Windows.Forms.Label();
            this.lab_loginID = new System.Windows.Forms.Label();
            this.profileBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_schedule = new System.Windows.Forms.Button();
            this.btn_role = new System.Windows.Forms.Button();
            this.btn_mgs = new System.Windows.Forms.Button();
            this.btn_hompage = new System.Windows.Forms.Button();
            this.btn_webHard = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.cbx01 = new System.Windows.Forms.ComboBox();
            this.txt_notice_search = new System.Windows.Forms.TextBox();
            this.lv_notice = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.noticeAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.noticeDel = new System.Windows.Forms.ToolStripMenuItem();
            this.noticeModify = new System.Windows.Forms.ToolStripMenuItem();
            this.KICO메신저 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.받은파일폴더열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btn_search);
            this.splitContainer1.Panel2.Controls.Add(this.cbx01);
            this.splitContainer1.Panel2.Controls.Add(this.txt_notice_search);
            this.splitContainer1.Panel2.Controls.Add(this.lv_notice);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(962, 810);
            this.splitContainer1.SplitterDistance = 555;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rb2);
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Location = new System.Drawing.Point(0, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(959, 487);
            this.panel2.TabIndex = 1;
            // 
            // rb2
            // 
            this.rb2.Location = new System.Drawing.Point(630, 316);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(279, 143);
            this.rb2.TabIndex = 5;
            this.rb2.Text = "";
            this.rb2.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(959, 487);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.Url = new System.Uri("http://52.141.56.159:8090/showUserList.do", System.UriKind.Absolute);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_UserInfo);
            this.panel1.Controls.Add(this.btn_message_mgs);
            this.panel1.Controls.Add(this.lab_name);
            this.panel1.Controls.Add(this.lab_loginID);
            this.panel1.Controls.Add(this.profileBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_schedule);
            this.panel1.Controls.Add(this.btn_role);
            this.panel1.Controls.Add(this.btn_mgs);
            this.panel1.Controls.Add(this.btn_hompage);
            this.panel1.Controls.Add(this.btn_webHard);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 52);
            this.panel1.TabIndex = 0;
            // 
            // btn_UserInfo
            // 
            this.btn_UserInfo.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_UserInfo.FlatAppearance.BorderSize = 0;
            this.btn_UserInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_UserInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_UserInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UserInfo.Image = global::Kyobo_Msg_Client.Properties.Resources.uv;
            this.btn_UserInfo.Location = new System.Drawing.Point(413, 10);
            this.btn_UserInfo.Name = "btn_UserInfo";
            this.btn_UserInfo.Size = new System.Drawing.Size(80, 30);
            this.btn_UserInfo.TabIndex = 10;
            this.btn_UserInfo.UseVisualStyleBackColor = true;
            this.btn_UserInfo.Click += new System.EventHandler(this.btn_UserInfo_Click);
            // 
            // btn_message_mgs
            // 
            this.btn_message_mgs.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.mv;
            this.btn_message_mgs.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_message_mgs.FlatAppearance.BorderSize = 0;
            this.btn_message_mgs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_message_mgs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_message_mgs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_message_mgs.Location = new System.Drawing.Point(499, 10);
            this.btn_message_mgs.Name = "btn_message_mgs";
            this.btn_message_mgs.Size = new System.Drawing.Size(80, 30);
            this.btn_message_mgs.TabIndex = 9;
            this.btn_message_mgs.UseVisualStyleBackColor = true;
            this.btn_message_mgs.Click += new System.EventHandler(this.btn_message_mgs_Click);
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lab_name.ForeColor = System.Drawing.Color.White;
            this.lab_name.Location = new System.Drawing.Point(56, 13);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(17, 21);
            this.lab_name.TabIndex = 8;
            this.lab_name.Text = "-";
            // 
            // lab_loginID
            // 
            this.lab_loginID.AutoSize = true;
            this.lab_loginID.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lab_loginID.Location = new System.Drawing.Point(50, 11);
            this.lab_loginID.Name = "lab_loginID";
            this.lab_loginID.Size = new System.Drawing.Size(0, 21);
            this.lab_loginID.TabIndex = 7;
            this.lab_loginID.Visible = false;
            // 
            // profileBox
            // 
            this.profileBox.Location = new System.Drawing.Point(7, 4);
            this.profileBox.Name = "profileBox";
            this.profileBox.Size = new System.Drawing.Size(43, 43);
            this.profileBox.TabIndex = 6;
            this.profileBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(588, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "바로가기 :";
            // 
            // btn_schedule
            // 
            this.btn_schedule.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_schedule.FlatAppearance.BorderSize = 0;
            this.btn_schedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_schedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_schedule.Image = global::Kyobo_Msg_Client.Properties.Resources.calendar;
            this.btn_schedule.Location = new System.Drawing.Point(904, 1);
            this.btn_schedule.Name = "btn_schedule";
            this.btn_schedule.Size = new System.Drawing.Size(48, 48);
            this.btn_schedule.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_schedule, "시험일정");
            this.btn_schedule.UseVisualStyleBackColor = true;
            this.btn_schedule.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_role
            // 
            this.btn_role.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_role.FlatAppearance.BorderSize = 0;
            this.btn_role.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_role.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_role.Image = global::Kyobo_Msg_Client.Properties.Resources.role;
            this.btn_role.Location = new System.Drawing.Point(850, 1);
            this.btn_role.Name = "btn_role";
            this.btn_role.Size = new System.Drawing.Size(48, 48);
            this.btn_role.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_role, "담당자 및 담당업무");
            this.btn_role.UseVisualStyleBackColor = true;
            this.btn_role.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_mgs
            // 
            this.btn_mgs.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_mgs.FlatAppearance.BorderSize = 0;
            this.btn_mgs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_mgs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_mgs.Image = global::Kyobo_Msg_Client.Properties.Resources.blog;
            this.btn_mgs.Location = new System.Drawing.Point(796, 1);
            this.btn_mgs.Name = "btn_mgs";
            this.btn_mgs.Size = new System.Drawing.Size(48, 48);
            this.btn_mgs.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_mgs, "자격평가");
            this.btn_mgs.UseVisualStyleBackColor = true;
            this.btn_mgs.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_hompage
            // 
            this.btn_hompage.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_hompage.FlatAppearance.BorderSize = 0;
            this.btn_hompage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_hompage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_hompage.Image = global::Kyobo_Msg_Client.Properties.Resources.homepage;
            this.btn_hompage.Location = new System.Drawing.Point(742, 1);
            this.btn_hompage.Name = "btn_hompage";
            this.btn_hompage.Size = new System.Drawing.Size(48, 48);
            this.btn_hompage.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btn_hompage, "홈페이지");
            this.btn_hompage.UseVisualStyleBackColor = true;
            this.btn_hompage.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_webHard
            // 
            this.btn_webHard.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.btn_webHard.FlatAppearance.BorderSize = 0;
            this.btn_webHard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.btn_webHard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Thistle;
            this.btn_webHard.Image = global::Kyobo_Msg_Client.Properties.Resources.drive;
            this.btn_webHard.Location = new System.Drawing.Point(688, 1);
            this.btn_webHard.Name = "btn_webHard";
            this.btn_webHard.Size = new System.Drawing.Size(48, 48);
            this.btn_webHard.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_webHard, "웹하드");
            this.btn_webHard.UseVisualStyleBackColor = true;
            this.btn_webHard.Click += new System.EventHandler(this.btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.attention;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btn_search
            // 
            this.btn_search.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.btn_search;
            this.btn_search.FlatAppearance.BorderColor = System.Drawing.Color.NavajoWhite;
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.NavajoWhite;
            this.btn_search.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_search.Location = new System.Drawing.Point(891, 9);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(58, 30);
            this.btn_search.TabIndex = 4;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // cbx01
            // 
            this.cbx01.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbx01.FormattingEnabled = true;
            this.cbx01.Items.AddRange(new object[] {
            "전체",
            "제목",
            "작성자",
            "내용"});
            this.cbx01.Location = new System.Drawing.Point(507, 12);
            this.cbx01.Name = "cbx01";
            this.cbx01.Size = new System.Drawing.Size(76, 25);
            this.cbx01.TabIndex = 3;
            // 
            // txt_notice_search
            // 
            this.txt_notice_search.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_notice_search.Location = new System.Drawing.Point(589, 12);
            this.txt_notice_search.Name = "txt_notice_search";
            this.txt_notice_search.Size = new System.Drawing.Size(296, 25);
            this.txt_notice_search.TabIndex = 2;
            // 
            // lv_notice
            // 
            this.lv_notice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lv_notice.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lv_notice.HideSelection = false;
            this.lv_notice.Location = new System.Drawing.Point(12, 48);
            this.lv_notice.MultiSelect = false;
            this.lv_notice.Name = "lv_notice";
            this.lv_notice.Size = new System.Drawing.Size(939, 191);
            this.lv_notice.TabIndex = 1;
            this.lv_notice.UseCompatibleStateImageBehavior = false;
            this.lv_notice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.lv_notice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "순번";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "제목";
            this.columnHeader2.Width = 500;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "작성자";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "작성일자";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "작성자ID";
            this.columnHeader5.Width = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(50, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "[공지사항]";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noticeAdd,
            this.noticeDel,
            this.noticeModify});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 70);
            // 
            // noticeAdd
            // 
            this.noticeAdd.Name = "noticeAdd";
            this.noticeAdd.Size = new System.Drawing.Size(146, 22);
            this.noticeAdd.Text = "공지사항추가";
            this.noticeAdd.Click += new System.EventHandler(this.noticeAdd_Click);
            // 
            // noticeDel
            // 
            this.noticeDel.Name = "noticeDel";
            this.noticeDel.Size = new System.Drawing.Size(146, 22);
            this.noticeDel.Text = "공지사항삭제";
            this.noticeDel.Click += new System.EventHandler(this.noticeDel_Click);
            // 
            // noticeModify
            // 
            this.noticeModify.Name = "noticeModify";
            this.noticeModify.Size = new System.Drawing.Size(146, 22);
            this.noticeModify.Text = "공지사항수정";
            this.noticeModify.Click += new System.EventHandler(this.noticeModify_Click);
            // 
            // KICO메신저
            // 
            this.KICO메신저.ContextMenuStrip = this.contextMenuStrip2;
            this.KICO메신저.Icon = ((System.Drawing.Icon)(resources.GetObject("KICO메신저.Icon")));
            this.KICO메신저.Text = "notifyIcon1";
            this.KICO메신저.Visible = true;
            this.KICO메신저.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.KICO메신저.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.받은파일폴더열기ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(171, 48);
            // 
            // 받은파일폴더열기ToolStripMenuItem
            // 
            this.받은파일폴더열기ToolStripMenuItem.Name = "받은파일폴더열기ToolStripMenuItem";
            this.받은파일폴더열기ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.받은파일폴더열기ToolStripMenuItem.Text = "받은파일폴더열기";
            this.받은파일폴더열기ToolStripMenuItem.Click += new System.EventHandler(this.menuOpenFile_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.menu_close_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 810);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "KICO 메신저";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.MouseHover += new System.EventHandler(this.mouse_hover);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lv_notice;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem noticeAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_schedule;
        private System.Windows.Forms.Button btn_role;
        private System.Windows.Forms.Button btn_mgs;
        private System.Windows.Forms.Button btn_hompage;
        private System.Windows.Forms.Button btn_webHard;
        private System.Windows.Forms.NotifyIcon KICO메신저;
        private System.Windows.Forms.Label lab_loginID;
        private System.Windows.Forms.PictureBox profileBox;
        private System.Windows.Forms.RichTextBox rb2;
        private System.Windows.Forms.Label lab_name;
        private System.Windows.Forms.ToolStripMenuItem noticeDel;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripMenuItem noticeModify;
        private System.Windows.Forms.Button btn_message_mgs;
        private System.Windows.Forms.Button btn_UserInfo;
        private System.Windows.Forms.TextBox txt_notice_search;
        private System.Windows.Forms.ComboBox cbx01;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 받은파일폴더열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
    }
}

