namespace ClientCrypto
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
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.OutMsg = new System.Windows.Forms.TextBox();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.SendBtn = new System.Windows.Forms.Button();
            this.SendLable = new System.Windows.Forms.Label();
            this.ServerHashText = new System.Windows.Forms.TextBox();
            this.ServerPubKeyLable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SendedMSGText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ServerPubK = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(587, 415);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(103, 23);
            this.ConnectBtn.TabIndex = 0;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.OnConnectClick);
            // 
            // OutMsg
            // 
            this.OutMsg.Location = new System.Drawing.Point(12, 12);
            this.OutMsg.Name = "OutMsg";
            this.OutMsg.Size = new System.Drawing.Size(439, 22);
            this.OutMsg.TabIndex = 1;
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Location = new System.Drawing.Point(696, 415);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(92, 23);
            this.DisconnectBtn.TabIndex = 2;
            this.DisconnectBtn.Text = "Disconnect";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.OnDisconnectClick);
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(506, 415);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 3;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.OnSendClick);
            // 
            // SendLable
            // 
            this.SendLable.AutoSize = true;
            this.SendLable.Location = new System.Drawing.Point(27, 54);
            this.SendLable.Name = "SendLable";
            this.SendLable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SendLable.Size = new System.Drawing.Size(0, 17);
            this.SendLable.TabIndex = 4;
            // 
            // ServerHashText
            // 
            this.ServerHashText.Location = new System.Drawing.Point(12, 247);
            this.ServerHashText.Multiline = true;
            this.ServerHashText.Name = "ServerHashText";
            this.ServerHashText.Size = new System.Drawing.Size(439, 121);
            this.ServerHashText.TabIndex = 13;
            // 
            // ServerPubKeyLable
            // 
            this.ServerPubKeyLable.AutoSize = true;
            this.ServerPubKeyLable.Location = new System.Drawing.Point(151, 54);
            this.ServerPubKeyLable.Name = "ServerPubKeyLable";
            this.ServerPubKeyLable.Size = new System.Drawing.Size(120, 17);
            this.ServerPubKeyLable.TabIndex = 14;
            this.ServerPubKeyLable.Text = "Server Public Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Server HASH";
            // 
            // SendedMSGText
            // 
            this.SendedMSGText.Location = new System.Drawing.Point(484, 74);
            this.SendedMSGText.Multiline = true;
            this.SendedMSGText.Name = "SendedMSGText";
            this.SendedMSGText.Size = new System.Drawing.Size(300, 300);
            this.SendedMSGText.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(607, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "SendedMsg";
            // 
            // ServerPubK
            // 
            this.ServerPubK.Location = new System.Drawing.Point(12, 74);
            this.ServerPubK.Multiline = true;
            this.ServerPubK.Name = "ServerPubK";
            this.ServerPubK.Size = new System.Drawing.Size(439, 121);
            this.ServerPubK.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SendedMSGText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerPubKeyLable);
            this.Controls.Add(this.ServerHashText);
            this.Controls.Add(this.ServerPubK);
            this.Controls.Add(this.SendLable);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.OutMsg);
            this.Controls.Add(this.ConnectBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.TextBox OutMsg;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label SendLable;
        private System.Windows.Forms.TextBox ServerHashText;
        private System.Windows.Forms.Label ServerPubKeyLable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SendedMSGText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ServerPubK;
    }
}

