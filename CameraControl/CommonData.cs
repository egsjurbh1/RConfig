/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/4/24
 * Time: 11:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace CameraControl
{
	/// <summary>
	/// Description of ConstData.
	/// </summary>
	public class CommonData
	{
		public static int CommandPort = 4500;
		public static int VideoPort = 4501;
		public static int HelloPort = 4502;
        public static int JpegPort = 55000;
		
		public static int GETBACKGROUND  = 1;
		public static int OPENEXE  = 2;
		public static int GETCONFIGFILE = 3;
		public static int REBOOT = 4;        
		public static int UPDATEEXE = 5;
		public static int RESETCONFIG = 6;
		public static int STARTVIDEO = 7;
		public static int ENDVIDEO = 8;
		public static int UPDATEC64 = 9;
		public static int CHOOSECAM = 10;
		public static int GETPHASECFG = 11;
        public static int GETCONFIG = 12;//海达SM-X-I当前配置
        public static int SETCONFIG = 13;
        public static int GETDAFAULTCFG = 14;//海达SM-X-I出厂配置
        public static int DEFAULTCONFIG = 15;
        public static int EE3_GETCONFIG = 16;  //EagleEye3相机参数配置
        public static int EE3_SETCONFIG = 17; 
        public static int CANCELCMD = 20;//取消当前配置

        public static int DEVICETYPEOFEE3 = 62; //EagleEye3设备类型 
        public static int DEVICETYPEOFEE3E = 63; //EagleEye3E设备类型
		public static int UDPCON = 99;
        public static int UDPSETTIME = 100;


        public static int Width1 = 704;
        public static int Height1 = 576;
        public static int FrmSize1 = 811008;
        public static int Width2 = 960;
		public static int Height2 = 544;
		public static int FrmSize2 = 1044480;
		public static byte[] Buffer1 = new byte[FrmSize1];
		public static byte[] HisBuf1 = new byte[FrmSize1 * 3];
        public static byte[] Buffer2 = new byte[FrmSize2];
        public static byte[] HisBuf2 = new byte[FrmSize2 * 3];
		public static Bitmap[] Bmps = new Bitmap[3];
		public static int curPos = 0;
		public static bool Flag = true;	
		
		public static int[,] zMo = new int[6,6]{
			{3,3,4,4,4,0}, {3,3,4,4,4,0}, {1,2,2,3,3,0},{0,1,2,2,2,0},{0,0,1,2,2,0},{0,0,0,0,0,0}
		};
	}
    public class EE3RunData
    {
        public static int MODE0 = 0;
        public static int MODE1 = 1;
        public static int MODE2 = 2;
    }
}
