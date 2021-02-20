namespace Kyobo_Msg_Client
{
    partial class NoticeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.NoticeDay = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NoticeName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NoticeTitle = new System.Windows.Forms.Label();
            this.lb_NoticeTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.download_panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.NoticeBody = new System.Windows.Forms.WebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_getFIle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.download_panel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.NoticeDay);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.NoticeName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.NoticeTitle);
            this.panel1.Controls.Add(this.lb_NoticeTitle);
            this.panel1.Location = new System.Drawing.Point(6, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 71);
            this.panel1.TabIndex = 0;
            // 
            // NoticeDay
            // 
            this.NoticeDay.AutoSize = true;
            this.NoticeDay.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NoticeDay.Location = new System.Drawing.Point(601, 40);
            this.NoticeDay.Name = "NoticeDay";
            this.NoticeDay.Size = new System.Drawing.Size(17, 21);
            this.NoticeDay.TabIndex = 5;
            this.NoticeDay.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(503, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "작  성  일 :";
            // 
            // NoticeName
            // 
            this.NoticeName.AutoSize = true;
            this.NoticeName.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NoticeName.Location = new System.Drawing.Point(120, 40);
            this.NoticeName.Name = "NoticeName";
            this.NoticeName.Size = new System.Drawing.Size(17, 21);
            this.NoticeName.TabIndex = 3;
            this.NoticeName.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(22, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "작 성 자 :";
            // 
            // NoticeTitle
            // 
            this.NoticeTitle.AutoSize = true;
            this.NoticeTitle.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NoticeTitle.Location = new System.Drawing.Point(120, 10);
            this.NoticeTitle.Name = "NoticeTitle";
            this.NoticeTitle.Size = new System.Drawing.Size(17, 21);
            this.NoticeTitle.TabIndex = 1;
            this.NoticeTitle.Text = "-";
            // 
            // lb_NoticeTitle
            // 
            this.lb_NoticeTitle.AutoSize = true;
            this.lb_NoticeTitle.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_NoticeTitle.Location = new System.Drawing.Point(22, 10);
            this.lb_NoticeTitle.Name = "lb_NoticeTitle";
            this.lb_NoticeTitle.Size = new System.Drawing.Size(82, 21);
            this.lb_NoticeTitle.TabIndex = 0;
            this.lb_NoticeTitle.Text = "제     목 :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.download_panel);
            this.panel2.Controls.Add(this.NoticeBody);
            this.panel2.Location = new System.Drawing.Point(12, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(929, 352);
            this.panel2.TabIndex = 1;
            // 
            // download_panel
            // 
            this.download_panel.BackColor = System.Drawing.Color.Bisque;
            this.download_panel.Controls.Add(this.label4);
            this.download_panel.Controls.Add(this.label22);
            this.download_panel.Controls.Add(this.label21);
            this.download_panel.Controls.Add(this.progressBar1);
            this.download_panel.Location = new System.Drawing.Point(192, 88);
            this.download_panel.Name = "download_panel";
            this.download_panel.Size = new System.Drawing.Size(522, 135);
            this.download_panel.TabIndex = 9;
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
            // NoticeBody
            // 
            this.NoticeBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NoticeBody.Location = new System.Drawing.Point(0, 0);
            this.NoticeBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.NoticeBody.Name = "NoticeBody";
            this.NoticeBody.Size = new System.Drawing.Size(929, 352);
            this.NoticeBody.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lvFileList);
            this.panel3.Controls.Add(this.btn_getFIle);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(6, 453);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(935, 83);
            this.panel3.TabIndex = 2;
            // 
            // lvFileList
            // 
            this.lvFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvFileList.Enabled = false;
            this.lvFileList.HideSelection = false;
            this.lvFileList.Location = new System.Drawing.Point(106, 9);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Scrollable = false;
            this.lvFileList.Size = new System.Drawing.Size(676, 66);
            this.lvFileList.TabIndex = 8;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "파일명";
            this.columnHeader1.Width = 800;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "변경파일명";
            this.columnHeader2.Width = 0;
            // 
            // btn_getFIle
            // 
            this.btn_getFIle.Location = new System.Drawing.Point(788, 9);
            this.btn_getFIle.Name = "btn_getFIle";
            this.btn_getFIle.Size = new System.Drawing.Size(136, 66);
            this.btn_getFIle.TabIndex = 6;
            this.btn_getFIle.Text = "파일받기";
            this.btn_getFIle.UseVisualStyleBackColor = true;
            this.btn_getFIle.Click += new System.EventHandler(this.btn_getFIle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "파일첨부";
            // 
            // NoticeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 540);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoticeView";
            this.Text = "공지사항보기";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.download_panel.ResumeLayout(false);
            this.download_panel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb_NoticeTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_getFIle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label NoticeTitle;
        private System.Windows.Forms.WebBrowser NoticeBody;
        private System.Windows.Forms.Label NoticeDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label NoticeName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel download_panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}