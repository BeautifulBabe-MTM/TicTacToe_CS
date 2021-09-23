using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Server1
    {
        public int port;
        public int bytes;
        public IPEndPoint iPEndPoint;
        public StringBuilder stringBuilder;
        public byte[] data;
        public Socket socket;
        public string str;
        public int[,] field;
        public char[,] fieldGUI ;
        public int win;
        public string pos;
        public string[] pos2;
        public int X;
        public int Y;
        public Dictionary<string, int> count_Words;
        public Socket socketClient;

        public Server1()
        {
            this.port = 8000;
            this.iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.stringBuilder = new StringBuilder();
            this.data = new byte[256];
            this.str = String.Empty;
            this.field = new int[3, 3] { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
            this.fieldGUI = new char[3, 3];
            this.win = 0;
            this.pos = " ";
            this.X = 0;
            this.Y = 0;
            this.count_Words = new Dictionary<string, int>();
        }

        public void ShutDown()
        {
            this.socket.Shutdown(SocketShutdown.Both);
            this.socket.Close();
        }
        public void Bind()
        {

            this.socket.Bind(iPEndPoint);
            this.socket.Listen(10);
            this.socketClient = socket.Accept();
        }
        public void sendMsg(string sms)
        {
            this.data = Encoding.Unicode.GetBytes(sms);
            this.socketClient.Send(data);
        }
        public void GetMsg()
        {
            int bytes = 0;
            byte[] data = new byte[256];
            this.stringBuilder.Clear();
            do
            {
                bytes = socketClient.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socketClient.Available > 0);
            socketClient.Send(Encoding.Unicode.GetBytes("Welcome to the server..."));
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}