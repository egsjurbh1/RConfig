/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/7/6
 * Time: 12:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace CameraControl
{
	/// <summary>
	/// Description of PhaseForm.
	/// </summary>
	public partial class PhaseForm : Form
	{
		public PhaseForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			btOk.DialogResult = DialogResult.OK;
			
			Init();
		}
		
		void Init(){
			picB1.SizeMode = PictureBoxSizeMode.StretchImage;
			picB2.SizeMode = PictureBoxSizeMode.StretchImage;
			picB3.SizeMode = PictureBoxSizeMode.StretchImage;
			picB4.SizeMode = PictureBoxSizeMode.StretchImage;
			
			if (File.Exists("1.bmp")) picB1.Image = Image.FromFile("1.bmp");
			if (File.Exists("2.bmp")) picB2.Image = Image.FromFile("2.bmp");
			if (File.Exists("3.bmp")) picB3.Image = Image.FromFile("3.bmp");
			if (File.Exists("4.bmp")) picB4.Image = Image.FromFile("4.bmp");
		}
		
		int GetValue(){
			carNum[0] = (int)ndcar1.Value;
			carNum[1] = (int)ndcar2.Value;
			carNum[2] = (int)ndcar3.Value;
			carNum[3] = (int)ndcar4.Value;
			
			ph1[0] = cbA0.SelectedIndex;
			ph1[1] = cbB0.SelectedIndex;
			ph1[2] = cbC0.SelectedIndex;
			ph1[3] = cbD0.SelectedIndex;
			
			ph2[0] = cbA1.SelectedIndex;
			ph2[1] = cbB1.SelectedIndex;
			ph2[2] = cbC1.SelectedIndex;
			ph2[3] = cbD1.SelectedIndex;
			
			tm[0] = (int)ndA.Value;
			tm[1] = (int)ndB.Value;
			tm[2] = (int)ndC.Value;
			tm[3] = (int)ndD.Value;
			
			int res = 0;
			for(int i=0; i<4; i++){
				if (ph1[i] > 0 || ph2[i] > 0) res = i;
				if (ph1[i] < 0) ph1[i] = 0;
				if (ph2[i] < 0) ph2[i] = 0;
			}
			return res;
		}
		
		private int[] carNum = new int[4];
		private int[] ph1 = new int[4];
		private int[] ph2 = new int[4];
		private int[] tm = new int[4];
 		
		void BtOkClick(object sender, EventArgs e)
		{
			
			FileStream fs = new FileStream("phase.cfg", FileMode.Create);
			BinaryWriter bw = new BinaryWriter(fs);
			RedGreen rg = new RedGreen();
			int tot = GetValue();
			
			for(int k=0; k<4; k++){
				bw.Write(Encoding.ASCII.GetBytes(string.Format("{0} {1} {2}\r\n", ph1[k], ph2[k], tm[k])));
				if (k<tot){
					int[,] res = rg.Generate(carNum[k], carNum[k+1], tm[k]);
					bw.Write(Encoding.ASCII.GetBytes(string.Format("{0} {1} ",carNum[k], carNum[k+1])));
					for(int i=0; i<=carNum[k]; i++) for(int j=0; j<=carNum[k+1]; j++) 
						bw.Write(Encoding.ASCII.GetBytes(string.Format("{0} ", res[i,j] + tm[k])));
					bw.Write(Encoding.ASCII.GetBytes("\r\n"));
				}
				
				if (k==tot){
					int[,] res = rg.Generate(carNum[k], carNum[0], tm[k]);
					bw.Write(Encoding.ASCII.GetBytes(string.Format("{0} {1} ", carNum[k], carNum[0])));
					for(int i=0; i<=carNum[k]; i++) for(int j=0; j<=carNum[0]; j++) 
						bw.Write(Encoding.ASCII.GetBytes(string.Format("{0}  ", res[i,j] + tm[k])));
					bw.Write(Encoding.ASCII.GetBytes("\r\n"));
				}
			}
			
			bw.Close();
			fs.Close();
		}
	}
}
