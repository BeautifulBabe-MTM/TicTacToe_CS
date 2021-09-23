using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Client1
    {
        public string ipAddr;
        public int port;
        public string position;
        public Socket socket;
        public IPEndPoint iPEndPoint;
        public Client1(string iP_Address, int port, string pos)
        {
            this.ipAddr = iP_Address;
            this.port = port;
            this.socket = socket;
            this.position = pos;
        }
        public void CreateIPEndPoint()
        {
            this.iPEndPoint = new IPEndPoint(IPAddress.Parse(this.ipAddr), this.port);
        }
        public void SendPositionToServer(string pos)
        {
            this.socket.Send(Encoding.Unicode.GetBytes(pos.ToString()));
        }
        public void SendFileToServer()
        {
            this.socket.Send(File.ReadAllBytes(this.position));
        }
        public void GetAndSendSizeToServ()
        {
            string size = string.Empty;
            size = File.ReadAllBytes(this.position).Count().ToString();
            this.socket.Send(Encoding.Unicode.GetBytes(size));
        }
        public void CreateSocket()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect()
        {
            this.CreateIPEndPoint();
            this.CreateSocket();
        }
        public string GetMSG()
        {
            byte[] data = new byte[250];
            StringBuilder stringBuilder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = this.socket.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (this.socket.Available > 0);
            return stringBuilder.ToString();
        }

    }
}