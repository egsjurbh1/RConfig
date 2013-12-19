/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/4/22
 * Time: 21:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System.IO;

namespace CameraControl
{
	/// <summary>
	/// Description of SingletonSocket.
	/// </summary>
	public sealed class SingletonSocket
	{
        private static SingletonSocket instance = new SingletonSocket();
		public static SingletonSocket Instance {
			get {
				return instance;
			}
		}
		
		private SingletonSocket()
		{
		}
		
		private MainForm mainForm;
		
		public void SetForm(MainForm main){
			mainForm = main;
		}
		
		//向在线设备广播消息
		public void SayHello()
        {
			//创建UDP广播Socket
            Socket sSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Broadcast, CommonData.HelloPort);
			sSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
			byte[] buffer = new byte[8];
			Array.Copy(BitConverter.GetBytes(CommonData.UDPCON), buffer, 4);
			Array.Copy(BitConverter.GetBytes(0), 0, buffer, 4, 4);
			sSock.SendTo(buffer, ipep);
			sSock.Close();          
		}

        //广播时间
        public void BroadCastTime() 
        {
            //创建UDP广播Socket
            Socket sSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Broadcast, CommonData.HelloPort);
            sSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
			
            //获取当前时间
            byte[] time = Encoding.ASCII.GetBytes(DateTime.Now.ToString("HH:mm:ss yyyy-MM-dd"));
            int size = time.Length;
            byte[] buffer = new byte[size+1];
            Array.Copy(BitConverter.GetBytes(100), buffer, 4);
            Array.Copy(BitConverter.GetBytes(size), 0, buffer, 4, 4);
            //发送
            sSock.SendTo(buffer, ipep);

            Array.Copy(time, 0, buffer, 0, size);
            buffer[size] = 0;
            sSock.SendTo(buffer, ipep);
            sSock.Close();
        }
		
		private Socket cSock = null;
		
		public void CloseFindClients(){
			if (cSock != null){
				cSock.Close();
			}
		}
		
        //统计显示在线设备
		public void FindClients()
		{
            //创建Socket,TCP服务端
            cSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ipep = new IPEndPoint(IPAddress.Any, CommonData.HelloPort);
			cSock.Bind(ipep);
            cSock.Listen(500);	

            int count = 0;
			while(true){
				Socket s;
				try{
					s = cSock.Accept();
                    count++;//统计在线设备数量
				}catch(Exception e){
                    
					break;
				}

                //创建连接具体在线设备子线程
                ParameterizedThreadStart pts = new ParameterizedThreadStart(UpdateClients);
                Thread thd = new Thread(pts);
                thd.Start(s);

                if (mainForm.Disposing || mainForm.IsDisposed) break;
			}
			cSock.Close();
		}

        //更新获取在线设备信息
        public void UpdateClients(object sock) 
        {
            byte[] rcv = new byte[128];
            Socket s = sock as Socket;
            //接收设备消息
            int res = s.Receive(rcv, 8, 0);

            if (res != 8)
            {
                s.Close();
                return;
            }

            int cmd = BitConverter.ToInt32(rcv, 0);

            if (cmd == 1)
            {
                return;
            }
            if (cmd == 0)
            {
                int tot = BitConverter.ToInt32(rcv, 4);
                InfoDelegate infoDglt = new InfoDelegate(mainForm.UpdateCameraInfo);
                CameraInfo cameraInfo = new CameraInfo();
                cameraInfo.IP = ((IPEndPoint)(s.RemoteEndPoint)).Address.ToString();

                //更新设备信息
                if (tot == CommonData.DEVICETYPEOFEE3)
                {
                    cameraInfo.camera.Add("EagleEye3智能相机");                   
                    CameraInfo.DeviceType = CommonData.DEVICETYPEOFEE3;
                }
                else if(tot==CommonData.DEVICETYPEOFEE3E)
                {
                    cameraInfo.camera.Add("EagleEye3E智能相机");
                    CameraInfo.DeviceType = CommonData.DEVICETYPEOFEE3E;
                }
                else if (tot == 1)
                {
                    cameraInfo.camera.Add("海达SM-X-I型");
                    CameraInfo.DeviceType = 1;
                }
                if (mainForm.Disposing || mainForm.IsDisposed) return;
                IAsyncResult iar = mainForm.BeginInvoke(infoDglt, cameraInfo);
                iar.AsyncWaitHandle.WaitOne();
                mainForm.EndInvoke(iar);
            }
            s.Close();
        }

		//接收图像YUV420|704*576
		public void RecvImagesYUV420()
		{
			//创建Socket服务端，接收图像流数据
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, CommonData.VideoPort);
			Socket vSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			vSock.Bind(ipep);
			vSock.Listen(0);
			Socket c = vSock.Accept();
            int cur = 0;
			int tot = 0;
            int size = 0;
            size = CommonData.FrmSize1;
            
			while(CommonData.Flag){               
                cur = c.Receive(CommonData.Buffer1, tot, size - tot, 0);                
				tot += cur;
				if (tot == size){
					if (!CommonData.Flag || mainForm.Disposing || mainForm.IsDisposed) break;
					VideoDelegate videoDglt = new VideoDelegate(mainForm.UpdateVideo);
					IAsyncResult iar = mainForm.BeginInvoke(videoDglt);
					iar.AsyncWaitHandle.WaitOne();
					mainForm.EndInvoke(iar);
					tot = 0;
				}
			}
			c.Close();
			vSock.Close();
		}
        //接收图像YUV422|960*544
        public void RecvImagesYUV422()
        {
            //创建Socket服务端，接收图像流
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, CommonData.VideoPort);
            Socket vSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            vSock.Bind(ipep);
            vSock.Listen(0);
            Socket c = vSock.Accept();
            int cur = 0;
            int tot = 0;
            int size = 0;
            size = CommonData.FrmSize2;

            while (CommonData.Flag)
            {

                cur = c.Receive(CommonData.Buffer2, tot, size - tot, 0);               
                tot += cur;
                if (tot == size)
                {
                    if (!CommonData.Flag || mainForm.Disposing || mainForm.IsDisposed) break;
                    VideoDelegate videoDglt = new VideoDelegate(mainForm.UpdateVideo);
                    IAsyncResult iar = mainForm.BeginInvoke(videoDglt);
                    iar.AsyncWaitHandle.WaitOne();
                    mainForm.EndInvoke(iar);
                    tot = 0;
                }
            }
            c.Close();
            vSock.Close();
        }
        //接受EE3的JPEG图像流
        public void RecvImagesJPEG()
        {
           
        }
		
		private Socket comSock = null;
		
		public bool CheckConnection(){
			if (comSock == null || !comSock.Connected){
				return false;	
			}
			return true;
		}
		//初始化控制客户端
		public bool InitComSock(string ip){
			comSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), CommonData.CommandPort);
			try{
				comSock.Connect(ipep);
				return true;
			}
			catch(Exception e){
				return false;
			}	
		}
        //初始化JPEG图像传输客户端
        public bool InitJpegClientSock(string ip)
        {
            comSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), CommonData.JpegPort);
            try
            {
                comSock.Connect(ipep);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
		
		public void CloseComSock(){
			if (comSock != null) comSock.Close();
			comSock = null;
		}

        //发送控制命令
		public int SendCommand(int command, int arg){
			byte[] snd = new byte[8];
			Array.Copy(BitConverter.GetBytes(command), 0, snd, 0, 4);
			Array.Copy(BitConverter.GetBytes(arg), 0, snd, 4, 4);
			//发送控制信息
            comSock.Send(snd, 8, 0);
			
			byte[] rcv = new byte[256];
            //接收反馈信息
			int res = comSock.Receive(rcv, rcv.Length, 0);
			int cmd = BitConverter.ToInt32(rcv, 0);
			if (cmd == command){
				int ret = BitConverter.ToInt32(rcv, 4);
				return ret;
			}
			return 0;
		}

		//发送配置信息
		public int SendCommand(int command, int length, byte[] buffer){
			byte[] snd = new byte[8];
			Array.Copy(BitConverter.GetBytes(command), 0, snd, 0, 4);
			Array.Copy(BitConverter.GetBytes(length), 0, snd, 4, 4);
            //发送控制信息
			comSock.Send(snd, 8, 0);
            //发送配置数据
			comSock.Send(buffer, length, 0);
			byte[] rcv = new byte[256];
            //接收反馈信息
			int res = comSock.Receive(rcv, rcv.Length, 0);
			int cmd = BitConverter.ToInt32(rcv, 0);
			if (cmd == command){
				int ret = BitConverter.ToInt32(rcv, 4);
				return ret;
			}
			return 0;
		}

        //接收配置信息
        public MemoryStream SendCfgCommand(int command, int arg) 
        {
            byte[] snd = new byte[8];
            Array.Copy(BitConverter.GetBytes(command), 0, snd, 0, 4);
            Array.Copy(BitConverter.GetBytes(arg), 0, snd, 4, 4);
            comSock.Send(snd, 8, 0);

            byte[] rcv = new byte[256];
            int res = comSock.Receive(rcv, rcv.Length, 0);
            int cmd = BitConverter.ToInt32(rcv, 0);

            MemoryStream ms = new MemoryStream();
            if (command == cmd) 
            {
                int count = BitConverter.ToInt32(rcv, 4);
                int cur = comSock.Receive(rcv, count, 0);
                if (cur == count)
                {
                    ms.Write(rcv, 0, count);
                }
            }
            
            return ms;
        }
          
	}
}
