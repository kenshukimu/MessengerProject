namespace Kyobo_Msg_Client
{
    partial class findUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(findUser));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvrcvUserList = new System.Windows.Forms.ListBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_set = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 301);
            this.panel1.TabIndex = 0;
            // 
            // lvrcvUserList
            // 
            this.lvrcvUserList.FormattingEnabled = true;
            this.lvrcvUserList.ItemHeight = 12;
            this.lvrcvUserList.Location = new System.Drawing.Point(309, 57);
            this.lvrcvUserList.Name = "lvrcvUserList";
            this.lvrcvUserList.Size = new System.Drawing.Size(237, 304);
            this.lvrcvUserList.TabIndex = 1;
            // 
            // btn_add
            // 
            this.btn_add.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.btn_fixadd;
            this.btn_add.Location = new System.Drawing.Point(245, 119);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(55, 55);
            this.btn_add.TabIndex = 2;
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_del
            // 
            this.btn_del.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.btn_fixdel;
            this.btn_del.Location = new System.Drawing.Point(245, 223);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(55, 55);
            this.btn_del.TabIndex = 3;
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "사용자찾기";
            // 
            // btn_set
            // 
            this.btn_set.BackgroundImage = global::Kyobo_Msg_Client.Properties.Resources.btn_ok;
            this.btn_set.Location = new System.Drawing.Point(423, 3);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(123, 48);
            this.btn_set.TabIndex = 5;
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(305, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "받을 사람";
            // 
            // findUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(563, 365);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_set);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.lvrcvUserList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "findUser";
            this.Text = "사용자찾기";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lvrcvUserList;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Label label2;
    }
}