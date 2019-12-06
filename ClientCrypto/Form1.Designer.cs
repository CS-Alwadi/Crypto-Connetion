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
            this.ServerPubKeyText = new System.Windows.Forms.TextBox();
            this.ServerHashText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SendedMSGText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            // ServerPubKeyText
            // 
            this.ServerPubKeyText.Location = new System.Drawing.Point(12, 74);
            this.ServerPubKeyText.Multiline = true;
            this.ServerPubKeyText.Name = "ServerPubKeyText";
            this.ServerPubKeyText.Size = new System.Drawing.Size(439, 121);
            this.ServerPubKeyText.TabIndex = 12;
            // 
            // ServerHashText
            // 
            this.ServerHashText.Location = new System.Drawing.Point(12, 247);
            this.ServerHashText.Multiline = true;
            this.ServerHashText.Name = "ServerHashText";
            this.ServerHashText.Size = new System.Drawing.Size(439, 121);
            this.ServerHashText.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Server Public Key";
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
            this.SendedMSGText.Size = new System.Drawing.Size(226, 294);
            this.SendedMSGText.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(546, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "SendedMsg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SendedMSGText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerHashText);
            this.Controls.Add(this.ServerPubKeyText);
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
        private System.Windows.Forms.TextBox ServerPubKeyText;
        private System.Windows.Forms.TextBox ServerHashText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SendedMSGText;
        private System.Windows.Forms.Label label3;
    }
}

