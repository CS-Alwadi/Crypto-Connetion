namespace ServerCrypt
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.InMSG = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IsServerStartLable = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ThreadIDLable = new System.Windows.Forms.Label();
            this.ClientPKLable = new System.Windows.Forms.Label();
            this.ClientPubKeyText = new System.Windows.Forms.TextBox();
            this.ClientHashTextBox = new System.Windows.Forms.TextBox();
            this.ClientHashLable = new System.Windows.Forms.Label();
            this.DecryptedMSGTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InMSG
            // 
            this.InMSG.Location = new System.Drawing.Point(12, 10);
            this.InMSG.Name = "InMSG";
            this.InMSG.Size = new System.Drawing.Size(297, 22);
            this.InMSG.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "MSG";
            // 
            // IsServerStartLable
            // 
            this.IsServerStartLable.AutoSize = true;
            this.IsServerStartLable.Location = new System.Drawing.Point(623, 17);
            this.IsServerStartLable.Name = "IsServerStartLable";
            this.IsServerStartLable.Size = new System.Drawing.Size(165, 17);
            this.IsServerStartLable.TabIndex = 3;
            this.IsServerStartLable.Text = "Server TCP NOT Started";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(490, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Start Server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnClickStartServer);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(626, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Close Server";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CloseServerOnClick);
            // 
            // ThreadIDLable
            // 
            this.ThreadIDLable.AutoSize = true;
            this.ThreadIDLable.Location = new System.Drawing.Point(391, 13);
            this.ThreadIDLable.Name = "ThreadIDLable";
            this.ThreadIDLable.Size = new System.Drawing.Size(71, 17);
            this.ThreadIDLable.TabIndex = 6;
            this.ThreadIDLable.Text = "Thread ID";
            // 
            // ClientPKLable
            // 
            this.ClientPKLable.AutoSize = true;
            this.ClientPKLable.Location = new System.Drawing.Point(198, 48);
            this.ClientPKLable.Name = "ClientPKLable";
            this.ClientPKLable.Size = new System.Drawing.Size(113, 17);
            this.ClientPKLable.TabIndex = 7;
            this.ClientPKLable.Text = "Client Public Key";
            // 
            // ClientPubKeyText
            // 
            this.ClientPubKeyText.Location = new System.Drawing.Point(12, 68);
            this.ClientPubKeyText.Multiline = true;
            this.ClientPubKeyText.Name = "ClientPubKeyText";
            this.ClientPubKeyText.Size = new System.Drawing.Size(439, 121);
            this.ClientPubKeyText.TabIndex = 11;
            // 
            // ClientHashTextBox
            // 
            this.ClientHashTextBox.Location = new System.Drawing.Point(12, 233);
            this.ClientHashTextBox.Multiline = true;
            this.ClientHashTextBox.Name = "ClientHashTextBox";
            this.ClientHashTextBox.Size = new System.Drawing.Size(439, 102);
            this.ClientHashTextBox.TabIndex = 12;
            // 
            // ClientHashLable
            // 
            this.ClientHashLable.AutoSize = true;
            this.ClientHashLable.Location = new System.Drawing.Point(207, 203);
            this.ClientHashLable.Name = "ClientHashLable";
            this.ClientHashLable.Size = new System.Drawing.Size(118, 17);
            this.ClientHashLable.TabIndex = 13;
            this.ClientHashLable.Text = "Client Hash value";
            // 
            // DecryptedMSGTextBox
            // 
            this.DecryptedMSGTextBox.Location = new System.Drawing.Point(490, 68);
            this.DecryptedMSGTextBox.Multiline = true;
            this.DecryptedMSGTextBox.Name = "DecryptedMSGTextBox";
            this.DecryptedMSGTextBox.Size = new System.Drawing.Size(300, 300);
            this.DecryptedMSGTextBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(585, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Decrypted MSG";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DecryptedMSGTextBox);
            this.Controls.Add(this.ClientHashLable);
            this.Controls.Add(this.ClientHashTextBox);
            this.Controls.Add(this.ClientPubKeyText);
            this.Controls.Add(this.ClientPKLable);
            this.Controls.Add(this.ThreadIDLable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.IsServerStartLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InMSG);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label IsServerStartLable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label ThreadIDLable;
        private System.Windows.Forms.TextBox InMSG;
        private System.Windows.Forms.Label ClientPKLable;
        private System.Windows.Forms.TextBox ClientPubKeyText;
        private System.Windows.Forms.TextBox ClientHashTextBox;
        private System.Windows.Forms.Label ClientHashLable;
        private System.Windows.Forms.TextBox DecryptedMSGTextBox;
        private System.Windows.Forms.Label label2;
    }
}

