/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/7/9
 * Time: 11:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace CameraControl
{
	/// <summary>
	/// Description of ExpFrom.
	/// </summary>
	public partial class ExpFrom : Form
	{
		private ComboBox[,] combx = new ComboBox[5,5];
		public ExpFrom()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.Save.DialogResult = DialogResult.OK;
			Init();
		}
		
		public void Init(){
			FileStream fs = new FileStream("exp.cfg", FileMode.Open);
			BinaryReader br = new BinaryReader(fs);
			for(int i=0; i<5; i++){
				for(int j=0; j<5; j++){
					CommonData.zMo[i,j] = br.ReadInt32();
				}
			}
			br.Close();
			fs.Close();
			
			for(int i=0; i<5; i++){
				for(int j=0; j<5; j++){
					ComboBox temp = new ComboBox();
					temp.Size = new Size(58, 30);
					temp.Items.Add("很短");
					temp.Items.Add("短");
					temp.Items.Add("适中");
					temp.Items.Add("长");
					temp.Items.Add("很长");
					temp.Location = new Point(j*60, i*30);
					temp.SelectedIndex = CommonData.zMo[i,j];
					combx[i, j] = temp;
					cbPan.Controls.Add(temp);
					
				}
			}
			
		}
		
		void SaveClick(object sender, EventArgs e)
		{
			FileStream fs = new FileStream("exp.cfg", FileMode.Create);
			BinaryWriter bw = new BinaryWriter(fs);
			for(int i=0; i<5; i++){
				for(int j=0; j<5; j++){
					int pos = combx[i, j].SelectedIndex;
					CommonData.zMo[i, j] = pos;
					bw.Write(pos);
				}
			}
			bw.Close();
			fs.Close();
		}
	}
}
