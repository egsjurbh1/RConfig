/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/6/21
 * Time: 15:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CameraControl
{
	/// <summary>
	/// Description of InputForm.
	/// </summary>
	public partial class InputForm : Form
	{
		private string camIP;
        private string srvIP;
        private int srvPort;
        private int camPX;
        private int camPY;
        private int sendZq;
        private int stime;
        private int etime;
        private int numNt;
        private int XQ;
		
		public string CamIP{
            set { camIP = value; }
			get { return camIP; }
		}
		public string SrvIP{
            set { srvIP = value;}
            get { return srvIP; }
		}

        public int SrvPort
        {
            set { srvPort = value; }
            get { return srvPort; }
        }
		
		public int CamPX{
            set { camPX = value; }
            get { return camPX; }
		}

        public int CamPY {
            set { camPY = value; }
            get { return camPY; }
        }

        public int Stime {
            set { stime = value; }
            get { return stime; }
        }

        public int Etime {
            set { etime = value; }
            get { return etime; }
        }

        public int SendZq {
            set { sendZq = value; }
            get { return sendZq; }
        }

        public int NumNt {
            set { numNt = value; }
            get { return numNt; }
        }
		
		public InputForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            ITB_Cam = new IPTextBox();
            ITB_Cam.Location = new Point(120, 15);
            ITB_Srv = new IPTextBox();
            ITB_Srv.Location = new Point(120, 55);

            this.Controls.Add(ITB_Cam);
            this.Controls.Add(ITB_Srv);

            PreInit();
            Init();

            
            
		}

        void PreInit() {
            camIP = "0.0.0.0";
            camPX = 0;
            camPY = 0;
            srvIP = "0.0.0.0";
            srvPort = 0;
            sendZq = 60;
            stime = 300;
            etime = 1110;
            numNt = 10;
            button1.DialogResult = DialogResult.OK;

        }

        public void Init() {
            ITB_Cam.Text = camIP;
            TB_PX.Text = camPX.ToString();
            TB_PY.Text = camPY.ToString();
            ITB_Srv.Text = srvIP;
            TB_Port.Text = srvPort.ToString();
            TB_ZQ.Text = sendZq.ToString();

            if (stime == 0 && etime == 24 * 60)
            {
                CB_ALLDAY.Checked = true;
                TPS.Enabled = false;
                TPE.Enabled = false;
            }
            else
            {
                TPS.Value = new DateTime(2012, 12, 31, stime / 60, stime % 60, 0);
                TPE.Value = new DateTime(2012, 12, 31, etime / 60, etime % 60, 0);
                TB_NT.Text = numNt.ToString();
            }
        }
		
		void Button2Click(object sender, EventArgs e)
		{
            MemoryStream ms = SingletonSocket.Instance.SendCfgCommand(CommonData.GETDAFAULTCFG, 0);

            if (ms.Length == 0)
            {
                MessageBox.Show("当前无默认配置，请设置！");
            }
            else
            {
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CamIP = sr.ReadLine();
                SrvIP = sr.ReadLine();
                SrvPort = Convert.ToInt32(sr.ReadLine());
                CamPX = Convert.ToInt32(sr.ReadLine());
                CamPY = Convert.ToInt32(sr.ReadLine());
                XQ = Convert.ToInt32(sr.ReadLine());
                sendZq = Convert.ToInt32(sr.ReadLine());
                Stime = Convert.ToInt32(sr.ReadLine());
                Etime = Convert.ToInt32(sr.ReadLine());
                NumNt = Convert.ToInt32(sr.ReadLine());
                sr.Close();

                Init();
            }

            ms.Close();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			if (TB_Port.Text == "") TB_Port.Text = "0";
            if (TB_PX.Text == "") TB_PX.Text = "0";
            if (TB_PY.Text == "") TB_PY.Text = "0";
            if (TB_ZQ.Text == "") TB_ZQ.Text = "0";
            if (TB_NT.Text == "") TB_NT.Text = "0";            

            camIP = ITB_Cam.Text;
            camPX = Int32.Parse(TB_PX.Text);
            camPY = Int32.Parse(TB_PY.Text);
            srvIP = ITB_Srv.Text;
            srvPort = Int32.Parse(TB_Port.Text);
            sendZq = Int32.Parse(TB_ZQ.Text);
            if (CB_ALLDAY.CheckState == CheckState.Checked)
            {
                stime = 0;
                etime = 24 * 60;
            }
            else
            {
                stime = TPS.Value.Hour * 60 + TPS.Value.Minute;
                etime = TPE.Value.Hour * 60 + TPS.Value.Minute;
            }
            numNt = Int32.Parse(TB_NT.Text);
		}
		
		private void Key_Press(object sender, KeyPressEventArgs e){
			
			if (e.KeyChar == '\b') return;
			if (e.KeyChar == 0x20){
				e.Handled = true;
				return;
			}
			
			TextBox tb = (TextBox)sender;
			string tmp = tb.Text.ToString() + e.KeyChar.ToString();
		
			try{
                Int32.Parse(tmp);
			}catch(Exception ee){
				e.Handled = true;
			}
		}



        private void CB_ALLDAY_CheckStateChanged(object sender, EventArgs e) 
        {
            if (CB_ALLDAY.CheckState == CheckState.Checked)
            {
                TPS.Enabled = false;
                TPE.Enabled = false;
            }
            else if (CB_ALLDAY.CheckState == CheckState.Unchecked)
            {
                TPS.Enabled = true;
                TPE.Enabled = true;
            }
        }
	}
}
