/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/4/23
 * Time: 19:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CameraControl
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class CommonFunc
	{
		private static int[] fac_1_4075 = new int[256];
		private static int[] fac_m_0_3455 = new int[256];
		private static int[] fac_m_0_7169 = new int[256];
		private static int[] fac_1_779 = new int[256];
		
		public static void InitConvertTable(){
			for(int i=0; i<256; i++){
				fac_1_4075[i] = (int)(1.4075 * (i-128));
				fac_m_0_3455[i] = (int)(-0.3455) * (i-128);
				fac_m_0_7169[i] = (int)(-0.7169) * (i-128);
				fac_1_779[i] = (int)(1.779 * (i-128));
			}
		}
		
		private static Rectangle rect1 = new Rectangle(0, 0, CommonData.Width1, CommonData.Height1);
		private static int FrSize1 =CommonData.Width1 * CommonData.Height1 * 3;
		private static byte[] rgb1 = new byte[FrSize1];

        private static Rectangle rect2 = new Rectangle(0, 0, CommonData.Width2, CommonData.Height2);
        private static int FrSize2 = CommonData.Width2 * CommonData.Height2 * 3;
        private static byte[] rgb2 = new byte[FrSize2];
    

    
    //图像转换YUV420→RGB
        public static Bitmap ConvertYUV420(byte[] yuvFrame, int width, int height)
        {

            int r, g, b;
            int cury = 0;
            int curu = height * width;
            int curv = curu + 1;
            int cur = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    r = fac_1_4075[yuvFrame[curv]] + yuvFrame[cury];
                    g = fac_m_0_3455[yuvFrame[curu]] + fac_m_0_7169[yuvFrame[curv]] + yuvFrame[cury];
                    b = fac_1_779[yuvFrame[curu]] + yuvFrame[cury];

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    rgb1[cur + 2] = (byte)r;
                    rgb1[cur + 1] = (byte)g;
                    rgb1[cur] = (byte)b;

                    cur += 3;
                    cury++;
                    if ((x & 1) == 1)
                    {
                        curu += 2;
                        curv += 2;
                    }
                }
            }

            Bitmap bm = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData bmData = bm.LockBits(rect1, ImageLockMode.ReadWrite, bm.PixelFormat);
            IntPtr pData = bmData.Scan0;
            Marshal.Copy(rgb1, 0, pData, FrSize1);
            bm.UnlockBits(bmData);

            return bm;
        }



      //图像转换YUV422→RGB
		public static Bitmap ConvertYUV422(byte[] yuvFrame, int width, int height){
		
			int r, g, b;
			int cury = 1;
			int curu = 0;
			int curv = 2;
			int cur = 0;
			//double t = 0.0;
			
			for(int y = 0; y < height; y++){
				for(int x = 0; x < width; x++){
					
					r = fac_1_4075[yuvFrame[curv]] + yuvFrame[cury];
					g = fac_m_0_3455[yuvFrame[curu]] + fac_m_0_7169[yuvFrame[curv]] + yuvFrame[cury];
					b = fac_1_779[yuvFrame[curu]] + yuvFrame[cury];
					
					if (r < 0) r = 0; else if (r > 255) r = 255;
					if (g < 0) g = 0; else if (g > 255) g = 255;
					if (b < 0) b = 0; else if (b > 255) b = 255;
					
					rgb2[cur + 2] = (byte)r;
					rgb2[cur + 1] = (byte)g;
					rgb2[cur    ] = (byte)b;
					
					cur += 3;
                    cury += 2;
					if ((x & 1) == 1) {
						curu += 4;
						curv += 4;
					}
				}
			}
			
			Bitmap bm = new Bitmap(width, height, PixelFormat.Format24bppRgb);
			
			BitmapData bmData = bm.LockBits(rect2, ImageLockMode.ReadWrite, bm.PixelFormat);
			IntPtr pData = bmData.Scan0;
			Marshal.Copy(rgb2, 0, pData, FrSize2);
			bm.UnlockBits(bmData);
		
			return bm;
		}
        
	}
}
