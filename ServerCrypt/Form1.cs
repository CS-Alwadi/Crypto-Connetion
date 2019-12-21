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
using System.Collections.Generic;

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
        class rsa_st
        {
            int p, q, n;
            double phi;
            double public_key = 2;
            double private_key = 1;
            public int second_n;
            public double second_Pk;
            Dictionary<char, double> rsa_map;
            private int gcd(int a, int b)
            {
                int t;
                while (true)
                {
                    t = a % b;
                    if (t == 0)
                        return b;
                    a = b;
                    b = t;
                }
            }
            public void set_server_paramters(byte[] msg)
            {
                second_Pk = BitConverter.ToDouble(msg, 0);
                second_n = BitConverter.ToInt32(msg, 8);
            }
            private void rsa_map_init()
            {
                for (int i = 1; i < 27; i++)
                {
                    rsa_map.Add((char)((int)i + 96), i);
                }
            }
            public byte[] get_publickey_pairs()
            {
                byte[] ret = new byte[4 + 8];
                BitConverter.GetBytes(public_key).CopyTo(ret, 0);
                BitConverter.GetBytes(n).CopyTo(ret, 8);
                return ret;
            }
            public byte[] GetPKForClient()
            {
                byte[] ret = new byte[13 + GetHash(get_publickey_pairs()).Length];
                ret[0] = (byte)CryptoCommand.SEND_PUBLICKEY;
                Array.Copy(get_publickey_pairs(), 0, ret, 1, 12);
                double pk = BitConverter.ToDouble(ret, 1);
                int tn = BitConverter.ToInt32(ret, 9);
                Array.Copy(GetHash(get_publickey_pairs()),0, ret,13,32);
                return ret;
            }
            public rsa_st(int q, int p)
            {
                rsa_map = new Dictionary<char, double>();
                rsa_map_init();
                this.p = p;
                this.q = q;
                this.n = q * p;
                this.phi = (p - 1) * (q - 1);
                while (public_key < this.phi)
                {
                    int track = this.gcd((int)public_key, (int)phi);
                    if (track == 1 && (public_key != p && public_key != q))
                        break;
                    else
                        public_key++;
                }
                while (true)
                {
                    if (((private_key * public_key) % phi) == 1)
                        break;
                    private_key++;
                }

            }
            public byte[] Encrypt(string msg)
            {
                byte[] ret = new byte[msg.Length * 8];

                for (int i = 0; i < msg.Length; i++)
                {
                    double alphabit;
                    rsa_map.TryGetValue(msg[i], out alphabit);
                    double res = Math.Pow(alphabit, public_key) % n;
                    byte[] res_bytes = BitConverter.GetBytes(res);
                    res_bytes.CopyTo(ret, i * 8);
                }
                return ret;
            }
            public byte[] Encrypt_For_Server(string msg, double pub_key, int priv_key)
            {
                byte[] ret = new byte[msg.Length * 8];

                for (int i = 0; i < msg.Length; i++)
                {
                    double alphabit;
                    rsa_map.TryGetValue(msg[i], out alphabit);
                    double res = Math.Pow(alphabit, pub_key) % priv_key;
                    byte[] res_bytes = BitConverter.GetBytes(res);
                    res_bytes.CopyTo(ret, i * 8);
                }
                return ret;
            }
            public string Decrypt(byte[] msg)
            {
                string ret = "";
                double encrypted_byte;
                for (int i = 0; i < msg.Length / 8; i++)
                {
                    byte[] b = new byte[8];
                    Array.Copy(msg, i * 8, b, 0, 8);
                    encrypted_byte = BitConverter.ToDouble(b,0);
                    encrypted_byte = Math.Pow(encrypted_byte, private_key) % n;
                    foreach (KeyValuePair<char, double> pair in rsa_map)
                    {
                        if (pair.Value == encrypted_byte)
                        {
                            ret += pair.Key;
                            break;
                        }
                    }
                }
                return ret;
            }
        }
        public class RsaEncrypt
        {
            private RSACryptoServiceProvider csp;
            private RSAParameters _privateKey;
            private RSAParameters _publicKey;

            public RsaEncrypt(int prov)
            {
                csp = new RSACryptoServiceProvider(prov);
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
        
        bool isInit = false;
        bool isClosed = false;
        CryptoCommand connectionStep;
        const int serverProv = 2048;
        string sendedPK;
        int clientProv;
        rsa_st rsa_object;
        NetworkStream in_stream;
        public Form1()
        {
            InitializeComponent();
            rsa_object = new rsa_st(3, 11);
            //byte[] dec = rsa_object.Encrypt_For_Server("mzta", 7, 33);
            //string dec_str = rsa_object.Decrypt(dec);
            //this.IsServerStartLable.ForeColor = Color.Red;
            connectionStep = CryptoCommand.SEND_PUBLICKEY;
        }
        
        private static byte[] GetHash(byte[] inputBuffer)
        {
            SHA256 hashValue = SHA256.Create();
            return hashValue.ComputeHash(inputBuffer);
        }
        private static bool IsHashesEquals(byte[] first,byte[] second)
        {
            if (first.Length != second.Length)
                return false;
            
            for(int i = 0;i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }
        private bool SetNewConnectionStep(byte[] str)
        {
            int c = (int)str[0];
            CryptoCommand curCommand;
           // int.TryParse(s_comand, out c);
            curCommand = (CryptoCommand)c;
            byte[] curhash = new byte[32];
            Array.Copy(str, 13, curhash, 0, 32);
            if(CryptoCommand.SEND_PUBLICKEY == curCommand)
            {
                byte[] in_data = new byte[12];
                Array.Copy(str, 1, in_data, 0, in_data.Length);
                byte[] calculated_hash = GetHash(in_data);
                if (IsHashesEquals(curhash,calculated_hash))
                {
                    rsa_object.set_server_paramters(in_data);
                    ClientPubKeyText.Text = "The client public key pairs is [ " + rsa_object.second_Pk.ToString() + " , " + rsa_object.second_n.ToString() + " ]";
                    ClientPKLable.ForeColor = Color.Green;
                    ClientHashLable.ForeColor = Color.Green;
                    ClientHashTextBox.Text = BitConverter.ToString(GetHash(rsa_object.get_publickey_pairs()),0);
                    if (in_stream.CanWrite)
                    {
                        byte[] sended_msg = rsa_object.GetPKForClient();
                        in_stream.Write(sended_msg, 0, sended_msg.Length);
                        connectionStep = CryptoCommand.SEND_MSG;
                    }
                    
                    return true;
                }
            }
            return false;
        }

        private void OnClickStartServer(object sender, EventArgs e)
        {
            if (serverThread != null)
                return;

            ThreadIDLable.Text = "Thread ID = " + Thread.GetDomainID().ToString();
            serverThread = new Thread(()=>{
                client = server.AcceptTcpClient();
                byte[] in_b = new byte[client.ReceiveBufferSize];
                in_stream = client.GetStream();
                int alif = 0;
                while (client.Connected)
                {
                    Invoke((MethodInvoker)(() =>
                    {
                    if (in_stream.CanRead && in_stream.DataAvailable)
                        {
                            byte[] buf = new byte[client.ReceiveBufferSize];
                            if (connectionStep != CryptoCommand.SEND_MSG)
                            {
                                byte[] incomming_msg = new byte[in_stream.Read(buf, 0, client.ReceiveBufferSize)];
                                Array.Copy(buf, 0, incomming_msg, 0,incomming_msg.Length);
                                SetNewConnectionStep(incomming_msg);
                            }
                            else if(connectionStep == CryptoCommand.SEND_MSG)
                            {

                                byte[] incomming_msg = new byte[in_stream.Read(buf, 0, client.ReceiveBufferSize)];
                                Array.Copy(buf, 0, incomming_msg, 0, incomming_msg.Length);
                                DecryptedMSGTextBox.Text = BitConverter.ToString(incomming_msg,0);
                                InMSG.Text = rsa_object.Decrypt(incomming_msg);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
