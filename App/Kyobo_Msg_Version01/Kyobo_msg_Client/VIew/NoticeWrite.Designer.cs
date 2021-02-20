namespace Kyobo_Msg_Client
{
    partial class NoticeWrite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeWrite));
            this.panel2 = new System.Windows.Forms.Panel();
            this.download_panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.NoticeBody = new Kyobo_Msg_Client.WebEditor();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BoardNo = new System.Windows.Forms.Label();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.btn_openFiile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Snd_msg = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.limitDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_NoticeTItle = new System.Windows.Forms.TextBox();
            this.NoticeTitle = new System.Windows.Forms.Label();
            this.chkLimit = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.download_panel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.download_panel);
            this.panel2.Controls.Add(this.NoticeBody);
            this.panel2.Location = new System.Drawing.Point(12, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(929, 345);
            this.panel2.TabIndex = 1;
            // 
            // download_panel
            // 
            this.download_panel.BackColor = System.Drawing.Color.Bisque;
            this.download_panel.Controls.Add(this.label4);
            this.download_panel.Controls.Add(this.label22);
            this.download_panel.Controls.Add(this.label21);
            this.download_panel.Controls.Add(this.progressBar1);
            this.download_panel.Location = new System.Drawing.Point(215, 102);
            this.download_panel.Name = "download_panel";
            this.download_panel.Size = new System.Drawing.Size(522, 135);
            this.download_panel.TabIndex = 1;
            this.download_panel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(16, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "업로드파일명";
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
            this.label21.Size = new System.Drawing.Size(133, 25);
            this.label21.TabIndex = 36;
            this.label21.Text = "업로드 진행률";
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
            this.NoticeBody.BodyHtml = null;
            this.NoticeBody.BodyText = null;
            this.NoticeBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NoticeBody.DocumentText = resources.GetString("NoticeBody.DocumentText");
            this.NoticeBody.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NoticeBody.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NoticeBody.FontSize = Kyobo_Msg_Client.FontSize.Three;
            this.NoticeBody.Location = new System.Drawing.Point(0, 0);
            this.NoticeBody.Name = "NoticeBody";
            this.NoticeBody.Size = new System.Drawing.Size(929, 345);
            this.NoticeBody.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BoardNo);
            this.panel3.Controls.Add(this.lvFileList);
            this.panel3.Controls.Add(this.btn_openFiile);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(12, 453);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(929, 83);
            this.panel3.TabIndex = 2;
            // 
            // BoardNo
            // 
            this.BoardNo.AutoSize = true;
            this.BoardNo.Location = new System.Drawing.Point(10, 61);
            this.BoardNo.Name = "BoardNo";
            this.BoardNo.Size = new System.Drawing.Size(0, 12);
            this.BoardNo.TabIndex = 10;
            this.BoardNo.Visible = false;
            // 
            // lvFileList
            // 
            this.lvFileList.HideSelection = false;
            this.lvFileList.Location = new System.Drawing.Point(91, 3);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Scrollable = false;
            this.lvFileList.Size = new System.Drawing.Size(574, 77);
            this.lvFileList.TabIndex = 9;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            // 
            // btn_openFiile
            // 
            this.btn_openFiile.Location = new System.Drawing.Point(671, 3);
            this.btn_openFiile.Name = "btn_openFiile";
            this.btn_openFiile.Size = new System.Drawing.Size(124, 77);
            this.btn_openFiile.TabIndex = 8;
            this.btn_openFiile.Text = "파일첨부";
            this.btn_openFiile.UseVisualStyleBackColor = true;
            this.btn_openFiile.Click += new System.EventHandler(this.btn_openFiile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "파일첨부";
            // 
            // btn_Snd_msg
            // 
            this.btn_Snd_msg.Location = new System.Drawing.Point(785, 12);
            this.btn_Snd_msg.Name = "btn_Snd_msg";
            this.btn_Snd_msg.Size = new System.Drawing.Size(121, 31);
            this.btn_Snd_msg.TabIndex = 6;
            this.btn_Snd_msg.Text = "등록하기";
            this.btn_Snd_msg.UseVisualStyleBackColor = true;
            this.btn_Snd_msg.Click += new System.EventHandler(this.btn_Snd_msg_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // limitDate
            // 
            this.limitDate.Location = new System.Drawing.Point(638, 49);
            this.limitDate.Name = "limitDate";
            this.limitDate.Size = new System.Drawing.Size(190, 21);
            this.limitDate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(557, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "만료일 :";
            // 
            // txt_NoticeTItle
            // 
            this.txt_NoticeTItle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_NoticeTItle.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_NoticeTItle.Location = new System.Drawing.Point(118, 12);
            this.txt_NoticeTItle.Name = "txt_NoticeTItle";
            this.txt_NoticeTItle.Size = new System.Drawing.Size(661, 29);
            this.txt_NoticeTItle.TabIndex = 2;
            // 
            // NoticeTitle
            // 
            this.NoticeTitle.AutoSize = true;
            this.NoticeTitle.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NoticeTitle.Location = new System.Drawing.Point(20, 14);
            this.NoticeTitle.Name = "NoticeTitle";
            this.NoticeTitle.Size = new System.Drawing.Size(92, 21);
            this.NoticeTitle.TabIndex = 0;
            this.NoticeTitle.Text = "제       목 :";
            // 
            // chkLimit
            // 
            this.chkLimit.AutoSize = true;
            this.chkLimit.Location = new System.Drawing.Point(834, 51);
            this.chkLimit.Name = "chkLimit";
            this.chkLimit.Size = new System.Drawing.Size(72, 16);
            this.chkLimit.TabIndex = 7;
            this.chkLimit.Text = "만료설정";
            this.chkLimit.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.chkLimit);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.NoticeTitle);
            this.panel1.Controls.Add(this.btn_Snd_msg);
            this.panel1.Controls.Add(this.txt_NoticeTItle);
            this.panel1.Controls.Add(this.limitDate);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 84);
            this.panel1.TabIndex = 3;
            // 
            // NoticeWrite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 540);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NoticeWrite";
            this.Text = "공지사항추가등록/수정";
            this.panel2.ResumeLayout(false);
            this.download_panel.ResumeLayout(false);
            this.download_panel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Snd_msg;
        private WebEditor NoticeBody;
        private System.Windows.Forms.Button btn_openFiile;
        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel download_panel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label BoardNo;
        private System.Windows.Forms.DateTimePicker limitDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_NoticeTItle;
        private System.Windows.Forms.Label NoticeTitle;
        private System.Windows.Forms.CheckBox chkLimit;
        private System.Windows.Forms.Panel panel1;
    }
}