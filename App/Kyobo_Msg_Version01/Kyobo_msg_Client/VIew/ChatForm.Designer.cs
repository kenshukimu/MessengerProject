namespace Kyobo_Msg_Client
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.chatHisBox = new System.Windows.Forms.RichTextBox();
            this.btn_sendMessage = new System.Windows.Forms.Button();
            this.labUserId = new System.Windows.Forms.Label();
            this.lv_chatPerson = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.sendTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chatHisBox
            // 
            this.chatHisBox.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chatHisBox.Location = new System.Drawing.Point(179, 16);
            this.chatHisBox.Name = "chatHisBox";
            this.chatHisBox.ReadOnly = true;
            this.chatHisBox.Size = new System.Drawing.Size(378, 366);
            this.chatHisBox.TabIndex = 0;
            this.chatHisBox.Text = "";
            // 
            // btn_sendMessage
            // 
            this.btn_sendMessage.Location = new System.Drawing.Point(503, 388);
            this.btn_sendMessage.Name = "btn_sendMessage";
            this.btn_sendMessage.Size = new System.Drawing.Size(54, 56);
            this.btn_sendMessage.TabIndex = 2;
            this.btn_sendMessage.Text = "보내기";
            this.btn_sendMessage.UseVisualStyleBackColor = true;
            this.btn_sendMessage.Click += new System.EventHandler(this.btn_sendMessage_Click);
            // 
            // labUserId
            // 
            this.labUserId.AutoSize = true;
            this.labUserId.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labUserId.ForeColor = System.Drawing.Color.White;
            this.labUserId.Location = new System.Drawing.Point(12, 25);
            this.labUserId.Name = "labUserId";
            this.labUserId.Size = new System.Drawing.Size(17, 21);
            this.labUserId.TabIndex = 6;
            this.labUserId.Text = "-";
            // 
            // lv_chatPerson
            // 
            this.lv_chatPerson.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_chatPerson.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lv_chatPerson.HideSelection = false;
            this.lv_chatPerson.Location = new System.Drawing.Point(12, 86);
            this.lv_chatPerson.Name = "lv_chatPerson";
            this.lv_chatPerson.Size = new System.Drawing.Size(161, 358);
            this.lv_chatPerson.TabIndex = 7;
            this.lv_chatPerson.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "아이디";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "역할";
            this.columnHeader2.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "대화참여자";
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(179, 388);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(318, 56);
            this.sendTextBox.TabIndex = 9;
            this.sendTextBox.Text = "";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(564, 450);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lv_chatPerson);
            this.Controls.Add(this.labUserId);
            this.Controls.Add(this.btn_sendMessage);
            this.Controls.Add(this.chatHisBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatHisBox;
        private System.Windows.Forms.Button btn_sendMessage;
        private System.Windows.Forms.Label labUserId;
        private System.Windows.Forms.ListView lv_chatPerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.RichTextBox sendTextBox;
    }
}