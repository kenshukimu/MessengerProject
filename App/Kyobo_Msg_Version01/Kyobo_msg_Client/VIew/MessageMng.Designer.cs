namespace Kyobo_Msg_Client
{
    partial class MessageMng
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageMng));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.msgList = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvRcv = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvSend = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_search = new System.Windows.Forms.Button();
            this.cbx01 = new System.Windows.Forms.ComboBox();
            this.txt_notice_search = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_rtn_message = new System.Windows.Forms.Button();
            this.btn_donwload = new System.Windows.Forms.Button();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.download_panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.messageBody = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_cindex = new System.Windows.Forms.Label();
            this.lb_userNm = new System.Windows.Forms.RichTextBox();
            this.lb_date = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_donwload2 = new System.Windows.Forms.Button();
            this.lvFileList2 = new System.Windows.Forms.ListView();
            this.lvRcvResult = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.msgList.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.download_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.msgList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.btn_search);
            this.splitContainer1.Panel2.Controls.Add(this.cbx01);
            this.splitContainer1.Panel2.Controls.Add(this.txt_notice_search);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1060, 620);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 0;
            // 
            // msgList
            // 
            this.msgList.Controls.Add(this.tabPage1);
            this.msgList.Controls.Add(this.tabPage2);
            this.msgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgList.Location = new System.Drawing.Point(0, 0);
            this.msgList.Name = "msgList";
            this.msgList.SelectedIndex = 0;
            this.msgList.Size = new System.Drawing.Size(296, 620);
            this.msgList.TabIndex = 0;
            this.msgList.SelectedIndexChanged += new System.EventHandler(this.msgList_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvRcv);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 594);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "받은 메세지";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvRcv
            // 
            this.lvRcv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4});
            this.lvRcv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRcv.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lvRcv.HideSelection = false;
            this.lvRcv.Location = new System.Drawing.Point(3, 3);
            this.lvRcv.MultiSelect = false;
            this.lvRcv.Name = "lvRcv";
            this.lvRcv.Size = new System.Drawing.Size(282, 588);
            this.lvRcv.TabIndex = 0;
            this.lvRcv.UseCompatibleStateImageBehavior = false;
            this.lvRcv.SelectedIndexChanged += new System.EventHandler(this.lvRcv_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "cindex";
            this.columnHeader5.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "보낸사람";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "받은일자";
            this.columnHeader4.Width = 150;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvSend);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(378, 594);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "보낸메세지";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvSend
            // 
            this.lvSend.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader1,
            this.columnHeader2});
            this.lvSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSend.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lvSend.HideSelection = false;
            this.lvSend.Location = new System.Drawing.Point(3, 3);
            this.lvSend.MultiSelect = false;
            this.lvSend.Name = "lvSend";
            this.lvSend.Size = new System.Drawing.Size(372, 588);
            this.lvSend.TabIndex = 0;
            this.lvSend.UseCompatibleStateImageBehavior = false;
            this.lvSend.SelectedIndexChanged += new System.EventHandler(this.lvSend_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "cindex";
            this.columnHeader6.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "받은사람";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "보낸일자";
            this.columnHeader2.Width = 150;
            // 
            // btn_search
            // 
            this.btn_search.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.btn_search;
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_search.Location = new System.Drawing.Point(683, 12);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(63, 28);
            this.btn_search.TabIndex = 7;
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
            this.cbx01.Location = new System.Drawing.Point(415, 13);
            this.cbx01.Name = "cbx01";
            this.cbx01.Size = new System.Drawing.Size(76, 25);
            this.cbx01.TabIndex = 6;
            // 
            // txt_notice_search
            // 
            this.txt_notice_search.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_notice_search.Location = new System.Drawing.Point(497, 13);
            this.txt_notice_search.Name = "txt_notice_search";
            this.txt_notice_search.Size = new System.Drawing.Size(180, 25);
            this.txt_notice_search.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.NavajoWhite;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_rtn_message);
            this.panel3.Controls.Add(this.btn_donwload);
            this.panel3.Controls.Add(this.lvFileList);
            this.panel3.Location = new System.Drawing.Point(2, 536);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(746, 80);
            this.panel3.TabIndex = 2;
            // 
            // btn_rtn_message
            // 
            this.btn_rtn_message.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btn_rtn_message.Location = new System.Drawing.Point(654, 3);
            this.btn_rtn_message.Name = "btn_rtn_message";
            this.btn_rtn_message.Size = new System.Drawing.Size(87, 71);
            this.btn_rtn_message.TabIndex = 2;
            this.btn_rtn_message.Text = "회신";
            this.btn_rtn_message.UseVisualStyleBackColor = true;
            this.btn_rtn_message.Click += new System.EventHandler(this.btn_rtn_message_Click);
            // 
            // btn_donwload
            // 
            this.btn_donwload.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btn_donwload.Location = new System.Drawing.Point(545, 3);
            this.btn_donwload.Name = "btn_donwload";
            this.btn_donwload.Size = new System.Drawing.Size(107, 71);
            this.btn_donwload.TabIndex = 1;
            this.btn_donwload.Text = "파일받기";
            this.btn_donwload.UseVisualStyleBackColor = true;
            this.btn_donwload.Click += new System.EventHandler(this.btn_donwload_Click);
            // 
            // lvFileList
            // 
            this.lvFileList.HideSelection = false;
            this.lvFileList.Location = new System.Drawing.Point(6, 3);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(533, 72);
            this.lvFileList.TabIndex = 0;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.download_panel);
            this.panel2.Controls.Add(this.messageBody);
            this.panel2.Location = new System.Drawing.Point(3, 163);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(743, 371);
            this.panel2.TabIndex = 1;
            // 
            // download_panel
            // 
            this.download_panel.BackColor = System.Drawing.Color.Bisque;
            this.download_panel.Controls.Add(this.label4);
            this.download_panel.Controls.Add(this.label22);
            this.download_panel.Controls.Add(this.label21);
            this.download_panel.Controls.Add(this.progressBar1);
            this.download_panel.Location = new System.Drawing.Point(61, 100);
            this.download_panel.Name = "download_panel";
            this.download_panel.Size = new System.Drawing.Size(522, 135);
            this.download_panel.TabIndex = 10;
            this.download_panel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(16, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "다운로드파일명";
            this.label4.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(313, 54);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 25);
            this.label22.TabIndex = 37;
            this.label22.Text = "남은시간";
            this.label22.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(16, 54);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(152, 25);
            this.label21.TabIndex = 36;
            this.label21.Text = "다운로드 진행률";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 92);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(479, 27);
            this.progressBar1.TabIndex = 31;
            // 
            // messageBody
            // 
            this.messageBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBody.Location = new System.Drawing.Point(0, 0);
            this.messageBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.messageBody.Name = "messageBody";
            this.messageBody.Size = new System.Drawing.Size(743, 371);
            this.messageBody.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Thistle;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lb_cindex);
            this.panel1.Controls.Add(this.lb_userNm);
            this.panel1.Controls.Add(this.lb_date);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 111);
            this.panel1.TabIndex = 0;
            // 
            // lb_cindex
            // 
            this.lb_cindex.AutoSize = true;
            this.lb_cindex.Location = new System.Drawing.Point(599, 86);
            this.lb_cindex.Name = "lb_cindex";
            this.lb_cindex.Size = new System.Drawing.Size(38, 12);
            this.lb_cindex.TabIndex = 9;
            this.lb_cindex.Text = "label3";
            this.lb_cindex.Visible = false;
            // 
            // lb_userNm
            // 
            this.lb_userNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_userNm.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_userNm.Location = new System.Drawing.Point(97, 5);
            this.lb_userNm.Name = "lb_userNm";
            this.lb_userNm.ReadOnly = true;
            this.lb_userNm.Size = new System.Drawing.Size(643, 60);
            this.lb_userNm.TabIndex = 8;
            this.lb_userNm.Text = "";
            // 
            // lb_date
            // 
            this.lb_date.AutoSize = true;
            this.lb_date.Font = new System.Drawing.Font("HY견고딕", 12F);
            this.lb_date.Location = new System.Drawing.Point(94, 86);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(19, 16);
            this.lb_date.TabIndex = 7;
            this.lb_date.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HY견고딕", 12F);
            this.label1.Location = new System.Drawing.Point(3, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "받은 일자 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("HY견고딕", 12F);
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "보낸 사람 :";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.NavajoWhite;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lvRcvResult);
            this.panel4.Controls.Add(this.btn_donwload2);
            this.panel4.Controls.Add(this.lvFileList2);
            this.panel4.Location = new System.Drawing.Point(3, 536);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(746, 80);
            this.panel4.TabIndex = 3;
            // 
            // btn_donwload2
            // 
            this.btn_donwload2.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btn_donwload2.Location = new System.Drawing.Point(633, 3);
            this.btn_donwload2.Name = "btn_donwload2";
            this.btn_donwload2.Size = new System.Drawing.Size(107, 71);
            this.btn_donwload2.TabIndex = 1;
            this.btn_donwload2.Text = "파일받기";
            this.btn_donwload2.UseVisualStyleBackColor = true;
            this.btn_donwload2.Click += new System.EventHandler(this.btn_donwload2_Click);
            // 
            // lvFileList2
            // 
            this.lvFileList2.HideSelection = false;
            this.lvFileList2.Location = new System.Drawing.Point(392, 2);
            this.lvFileList2.Name = "lvFileList2";
            this.lvFileList2.Size = new System.Drawing.Size(235, 72);
            this.lvFileList2.TabIndex = 0;
            this.lvFileList2.UseCompatibleStateImageBehavior = false;
            // 
            // lvRcvResult
            // 
            this.lvRcvResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRcvResult.HideSelection = false;
            this.lvRcvResult.Location = new System.Drawing.Point(6, 2);
            this.lvRcvResult.Name = "lvRcvResult";
            this.lvRcvResult.Size = new System.Drawing.Size(380, 72);
            this.lvRcvResult.TabIndex = 2;
            this.lvRcvResult.UseCompatibleStateImageBehavior = false;
            // 
            // MessageMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 620);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageMng";
            this.Text = "메세지 관리";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.msgList.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.download_panel.ResumeLayout(false);
            this.download_panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl msgList;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvRcv;
        private System.Windows.Forms.ListView lvSend;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.WebBrowser messageBody;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btn_donwload;
        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.RichTextBox lb_userNm;
        private System.Windows.Forms.Panel download_panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ComboBox cbx01;
        private System.Windows.Forms.TextBox txt_notice_search;
        private System.Windows.Forms.Button btn_rtn_message;
        private System.Windows.Forms.Label lb_cindex;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_donwload2;
        private System.Windows.Forms.ListView lvFileList2;
        private System.Windows.Forms.ListView lvRcvResult;
    }
}