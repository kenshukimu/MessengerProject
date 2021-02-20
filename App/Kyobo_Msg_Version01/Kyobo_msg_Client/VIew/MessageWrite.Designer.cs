namespace Kyobo_Msg_Client
{
    partial class MessageFormBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageFormBox));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.btn_addFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.messageBody = new Kyobo_Msg_Client.WebEditor();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.download_panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_rcv_user = new System.Windows.Forms.RichTextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.download_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.groupBox1.Controls.Add(this.lvFileList);
            this.groupBox1.Controls.Add(this.btn_addFile);
            this.groupBox1.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 448);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 93);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[첨부파일]";
            // 
            // lvFileList
            // 
            this.lvFileList.HideSelection = false;
            this.lvFileList.Location = new System.Drawing.Point(6, 26);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(848, 64);
            this.lvFileList.TabIndex = 10;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            // 
            // btn_addFile
            // 
            this.btn_addFile.Location = new System.Drawing.Point(860, 26);
            this.btn_addFile.Name = "btn_addFile";
            this.btn_addFile.Size = new System.Drawing.Size(169, 64);
            this.btn_addFile.TabIndex = 9;
            this.btn_addFile.Text = "파일첨부";
            this.btn_addFile.UseVisualStyleBackColor = true;
            this.btn_addFile.Click += new System.EventHandler(this.btn_addFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // messageBody
            // 
            this.messageBody.BodyHtml = null;
            this.messageBody.BodyText = null;
            this.messageBody.DocumentText = resources.GetString("messageBody.DocumentText");
            this.messageBody.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.messageBody.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.messageBody.FontSize = Kyobo_Msg_Client.FontSize.Three;
            this.messageBody.Location = new System.Drawing.Point(12, 105);
            this.messageBody.Name = "messageBody";
            this.messageBody.Size = new System.Drawing.Size(1036, 337);
            this.messageBody.TabIndex = 0;
            // 
            // download_panel
            // 
            this.download_panel.BackColor = System.Drawing.Color.Bisque;
            this.download_panel.Controls.Add(this.label4);
            this.download_panel.Controls.Add(this.label22);
            this.download_panel.Controls.Add(this.label21);
            this.download_panel.Controls.Add(this.progressBar1);
            this.download_panel.Location = new System.Drawing.Point(269, 208);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Thistle;
            this.panel1.Controls.Add(this.txt_rcv_user);
            this.panel1.Controls.Add(this.btn_Send);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 86);
            this.panel1.TabIndex = 11;
            // 
            // txt_rcv_user
            // 
            this.txt_rcv_user.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_rcv_user.Location = new System.Drawing.Point(113, 4);
            this.txt_rcv_user.Name = "txt_rcv_user";
            this.txt_rcv_user.ReadOnly = true;
            this.txt_rcv_user.Size = new System.Drawing.Size(761, 76);
            this.txt_rcv_user.TabIndex = 12;
            this.txt_rcv_user.Text = "";
            this.txt_rcv_user.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_rcv_user_MouseClick_1);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(880, 3);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(146, 77);
            this.btn_Send.TabIndex = 11;
            this.btn_Send.Text = "보내기";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("HY견고딕", 12F);
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "받는 사람";
            // 
            // MessageFormBox
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 550);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.download_panel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.messageBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageFormBox";
            this.Text = "메세지보내기";
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MessageFormBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MessageFormBox_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.download_panel.ResumeLayout(false);
            this.download_panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WebEditor messageBody;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_addFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListView lvFileList;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel download_panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txt_rcv_user;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.Label label2;
    }
}