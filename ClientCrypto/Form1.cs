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
                BitConverter.GetBytes(this.public_key).CopyTo(ret, 0);
                BitConverter.GetBytes(this.n).CopyTo(ret, 8);
                return ret;
            }
            public byte[] GetPKForServer()
            {
                byte[] ret = new byte[13 + GetHash(get_publickey_pairs()).Length];
                byte[] data = new byte[13];
                data[0] = (byte)CryptoCommand.SEND_PUBLICKEY;
                get_publickey_pairs().CopyTo(ret, 1);
                GetHash(get_publickey_pairs()).CopyTo(ret, 13);
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
                    encrypted_byte = BitConverter.ToDouble(msg, i * 8);
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
        
        TcpClient connection;
        NetworkStream in_stream;
        bool isConnected = false;
        bool isInit = false;
        Thread clientThread = null;
        rsa_st rsa_client_obj;
        string serverPublicKey;
        int serverProv = 0;
        CryptoCommand connectionSteps;
        bool IsStepDone;
        const int prov = 2048;
        bool canSendCommand = false;
        bool started = false;

        public Form1()
        {
            InitializeComponent();
            rsa_client_obj = new rsa_st(5, 7);
            string test = "omar";
            byte [] encrypted_data = rsa_client_obj.Encrypt(test);

            string decrypted_data = rsa_client_obj.Decrypt(encrypted_data);
            rsa_client_obj.set_server_paramters(rsa_client_obj.get_publickey_pairs());
            connectionSteps = CryptoCommand.SEND_PUBLICKEY;
            IsStepDone = false;
            canSendCommand = true;
        }

        public static string GetKeyString(RSAParameters publicKey)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        private static byte[] GetHash(byte[] inputBuffer)
        {
            SHA256 hashValue = SHA256.Create();
            return hashValue.ComputeHash(inputBuffer);
        }
        
        private byte[] GetIncommingBytes(byte [] msg)
        {
            byte[] ret = new byte[12];
            for(int i = 0; i < 13; i++)
            {
                ret[i] = msg[i + 1];
            }
            return ret;
        }
        private byte[] GetIncomingHash(byte[] msg)
        {
            byte[] ret = new byte[msg.Length - 13];
            for(int i = 0; i < msg.Length - 13;i++)
            {
                ret[i] = msg[13 + i];
            }
            return ret;
        }

        private static bool IsHashesEquals(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
                return false;

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }

        private void ProcessingServerMSG(Byte [] buf)
        {
            int c = (int)buf[0]; 
            CryptoCommand curCommand;
            curCommand = (CryptoCommand)c;

            if(curCommand == CryptoCommand.SEND_PUBLICKEY)
            {
                byte[] incomming_hash = new byte[32];
                Array.Copy(buf, 13, incomming_hash, 0, 32);
                byte[] incomming_data = new byte[12];
                Array.Copy(buf, 1, incomming_data, 0, 12);
                byte[] actual_hash = GetHash(incomming_data);
                if(IsHashesEquals(actual_hash,incomming_hash))
                {
                    label2.ForeColor = Color.Green;
                    ServerPubKeyLable.ForeColor = Color.Green;
                    rsa_client_obj.set_server_paramters(incomming_data);
                    connectionSteps = CryptoCommand.SEND_MSG;
                }
            }
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
                        if(in_stream.CanRead)
                        {
                           if(connectionSteps != CryptoCommand.SEND_MSG)
                            {
                                
                                if(!started)
                                {
                                    byte[] buf = new byte[connection.ReceiveBufferSize];
                                    byte [] send_buf = rsa_client_obj.GetPKForServer();
                                    in_stream.Write(send_buf, 0, send_buf.Length);
                                    started = true;
                                }

                                else
                                {
                                    if(in_stream.DataAvailable)
                                    {
                                        byte[] buf = new byte[connection.ReceiveBufferSize];
                                        byte[] incomming_msg = new byte[in_stream.Read(buf, 0, buf.Length)];
                                        Array.Copy(buf, 0, incomming_msg, 0, incomming_msg.Length);
                                        ProcessingServerMSG(incomming_msg);
                                        ServerPubK.Text = "The server public key pairs is [ " + rsa_client_obj.second_Pk.ToString() + " , " + rsa_client_obj.second_n.ToString() + " ]";
                                        ServerHashText.Text = BitConverter.ToString(GetHash(rsa_client_obj.get_publickey_pairs()),0);
                                        //in_stream.Write(buf, 0, buf.Length);
                                    }
                                }
                                    
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
            if(connectionSteps == CryptoCommand.SEND_MSG)
            {
                if (in_stream.CanWrite)
                {
                    byte[] sended_msg = rsa_client_obj.Encrypt_For_Server(OutMsg.Text,rsa_client_obj.second_Pk,rsa_client_obj.second_n);
                    SendedMSGText.Text = BitConverter.ToString(sended_msg, 0);
                    in_stream.Write(sended_msg, 0, sended_msg.Length);
                }
            }
        }
    }
}
