using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
namespace ClientCrypto
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
                return Encoding.Unicode.GetString(csp.Decrypt(Convert.FromBase64String(inputBuffer), false));
            }
        }
        TcpClient connection;
        NetworkStream in_stream;
        bool isConnected = false;
        bool isInit = false;
        Thread clientThread = null;
        RsaEncrypt rsaObj;
        string serverPublicKey;
        CryptoCommand connectionSteps;
        bool IsStepDone;

        public Form1()
        {
            InitializeComponent();
            rsaObj = new RsaEncrypt();
            connectionSteps = CryptoCommand.SEND_PUBLICKEY;
            IsStepDone = false;
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
        private string CreateResponseToServer(CryptoCommand step)
        {
            string buf = "";
            switch(step)
            {
                case CryptoCommand.SEND_PUBLICKEY:
                    buf = CryptoCommand.SEND_PUBLICKEY + rsaObj.GetPublicKey();
                    break;
                case CryptoCommand.SEND_HASH:
                    buf = CryptoCommand.SEND_HASH + GetHash(rsaObj.GetPublicKey());
                    break;
            }
            return buf;
        }
        private static void SetTextForObjects(byte [] buf)
        {

        }
        private void OnConnectClick(object sender, EventArgs e)
        {
            if (isConnected)
                return;
            clientThread = new Thread(() =>
            {
            while (connection.Connected)
            {
                Invoke((MethodInvoker)(()=>
                    {
                        byte [] buf = new byte[connection.ReceiveBufferSize];
                        if(in_stream.CanRead)
                        {
                           if(connectionSteps != CryptoCommand.SEND_MSG)
                            {
                                in_stream.Write(Encoding.ASCII.GetBytes(CreateResponseToServer(connectionSteps)),
                                                0,
                                                connection.SendBufferSize);
                                in_stream.Read(buf, 0, connection.ReceiveBufferSize);
                                string s_connectionSteps = buf[0].ToString();
                                SetTextForObjects(buf);
                                int s = 0;
                                Int32.TryParse(s_connectionSteps, out s);
                                connectionSteps = (CryptoCommand)s;
                            }
                        }

                }));
                   
                }
            });
            isConnected = true;
            connection = new TcpClient();
            connection.Connect("localhost", 2553);
            in_stream = connection.GetStream();
            clientThread.Start();
        }

        private void OnDisconnectClick(object sender, EventArgs e)
        {
            if(isConnected == true)
            {
                isConnected = false;
                connection.Dispose();
                connection.Close();
            }
        }

        private void OnSendClick(object sender, EventArgs e)
        {
            if(connection.Connected && connectionSteps == CryptoCommand.SEND_MSG)
            {
                byte[] buf = new byte[connection.SendBufferSize];
                
                
                if(in_stream.CanWrite)
                    in_stream.Write(buf, 0, OutMsg.Text.Length);
            }
        }
    }
}
