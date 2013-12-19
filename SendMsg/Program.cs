using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SendMsg
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                byte[] buffer = new byte[8];

                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 4502);

                sock.Bind(ipep);

                sock.Receive(buffer);

                sock.Close();



                //Console.Write();
                Console.WriteLine( ++count );

                Socket ss = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("10.60.139.65"), 4502);
                ss.Connect(ip);

                byte[] snd = new byte[8];

                Array.Copy(BitConverter.GetBytes(0), snd, 4);
                Array.Copy(BitConverter.GetBytes(2), 0, buffer, 4, 4);

                ss.Send(snd);
                ss.Close();

            }


        }
    }
}
