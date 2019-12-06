using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;

namespace ServerCrypt
{
    enum CryptoCommand
    {
        SEND_PUBLICKEY,
        SEND_HASH,
        SEND_MSG
    };
    public partial class Form1 : Form
    {   
        
        public class RsaEncrypt
        {
            private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
            private RSAParameters _privateKey;
            private RSAParameters _publicKey;

            public RsaEncrypt()
            {
                _privateKey = csp.ExportParameters(true);
                _publicKey = csp.ExportParameters(false);
            }

            public string GetPublicKey()
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(typeof(RSAParameters));
                xs.Serialize(sw, _publicKey);
                return sw.ToString();
            }
            public string Encrypt(string inputBuffer)
            {
                csp = new RSACryptoServiceProvider();
                csp.ImportParameters(_publicKey);
                return Convert.ToBase64String(csp.Encrypt(Encoding.Unicode.GetBytes(inputBuffer), false));
            }
            public string Decrypt(string inputBuffer)
            {
                csp.ImportParameters(_privateKey);
                return Encoding.Unicode.GetString(csp.Decrypt(Convert.FromBase64String(inputBuffer),false));
            }
        }
        
        TcpListener server;
        TcpClient client;
        Thread serverThread = null;
        RsaEncrypt rsaObj;
        bool isInit = false;
        bool isClosed = false;
        CryptoCommand connectionStep;

        public Form1()
        {
            InitializeComponent();
            rsaObj = new RsaEncrypt();
            pubKeyText.Text = rsaObj.GetPublicKey();
            this.IsServerStartLable.ForeColor = Color.Red;
        }
        public static string GetKeyString(RSAParameters publicKey)
        {

            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        private static string GetHash(string inputBuffer)
        {
            SHA256 hashValue = SHA256.Create();
            return BitConverter.ToString(hashValue.ComputeHash(Encoding.UTF8.GetBytes(inputBuffer)));
        }

        private void OnClickStartServer(object sender, EventArgs e)
        {
            if (serverThread != null)
                return;

            ThreadIDLable.Text = "Thread ID = " + Thread.GetDomainID().ToString();
            serverThread = new Thread(()=>{
                client = server.AcceptTcpClient();
                byte[] in_b = new byte[client.ReceiveBufferSize];
                NetworkStream in_stream = client.GetStream();

                while (client.Connected)
                {
                    Invoke((MethodInvoker)(() =>
                    {
                    if (in_stream.CanRead && in_stream.DataAvailable)
                        {
                            byte[] buf = new byte[client.ReceiveBufferSize];
                            if(connectionStep == CryptoCommand.SEND_PUBLICKEY)
                            {

                            }
                        }
                    }));
                }
                client.Close();
                in_stream.Close();
            });

            isClosed = false;
            server = new TcpListener(IPAddress.Any, 2553);
            server.Start();
            this.IsServerStartLable.Text = "Server TCP Started";
            this.IsServerStartLable.ForeColor = Color.Green;
           
            this.serverThread.Start();
        }
        private void CloseServerOnClick(object sender, EventArgs e)
        {
            if(serverThread != null)
            {
                isClosed = true;
                this.IsServerStartLable.Text = "Server TCP NOT Started";
                this.IsServerStartLable.ForeColor = Color.Red;
                serverThread.Abort();
                serverThread = null;
                server.Stop();
            }
        }
        

       
    }
}
