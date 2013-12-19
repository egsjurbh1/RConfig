using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 4545);
            IPAddress ipa =  IPAddress.Broadcast;
            sock.Bind(ipep);
            sock.Listen(0);
            Socket s = sock.Accept();
            byte[] rcv = new byte[128];
            s.Receive(rcv);
            Console.WriteLine(rcv[0]);
           
        }
    }
}
