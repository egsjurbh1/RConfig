/*
 * Created by LQ.
 * Brief: EagleEye3系列相机功能配置 
 * Date: 2013/12/27
 * Version: 0.1.0
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CameraControl
{
    public partial class EE3DefaultConfig : Form
    {
        //变量定义
        private string cameraIP;
        private string serverIP;
        private int serverPort;
        private int resolution;
        private int runMode;
        private int temperature;
        private int roadnum1;
        private int roadnum2;
        private int uploadtime;
        private int nighttrafficstream;
        private string version;
        private int isreadflash;
        private bool isException;
        //全局传值
        public string CameraIP
        {
            set { cameraIP = value; }
            get { return cameraIP; }
        }
        public string ServerIP
        {
            set { serverIP = value; }
            get { return serverIP; }
        }
        public int ServerPort
        {
            set { serverPort = value; }
            get { return serverPort; }
        }
        public int Resolution
        {
            set { resolution = value; }
            get { return resolution; }
        }
        public int RunMode
        {
            set { runMode = value; }
            get { return runMode; }
        }
        public int Temperature
        {
            set { temperature = value; }
            get { return temperature; }
        }
        public int Roadnum1
        {
            set { roadnum1 = value; }
            get { return roadnum1; }
        }
        public int Roadnum2
        {
            set { roadnum2 = value; }
            get { return roadnum2; }
        }
        public int Uploadtime
        {
            set { uploadtime = value; }
            get { return uploadtime; }
        }
        public int Nighttrafficstream
        {
            set { nighttrafficstream = value; }
            get { return nighttrafficstream; }
        }
        public string Version
        {
            set { version = value; }
            get { return version; }
        }
        public int IsReadFlash
        {
            set { isreadflash = value; }
            get { return isreadflash; }
        }
        public bool IsException
        {
            set { isException = value; }
            get { return isException; }
        }
        //控件初始化
        public EE3DefaultConfig()
        {
            InitializeComponent();
            PreInit();
            Init();
        }
        //变量初始化
        void PreInit()
        {
            cameraIP = "0.0.0.0";
            resolution = 0;
            runMode = 110;//不可设为0
            serverIP = "0.0.0.0";
            serverPort = 4545;
            roadnum1 = 0;
            roadnum2 = 0;
            uploadtime = 60;
            version = "None";
            nighttrafficstream = 10;
            temperature = 0;
            isreadflash = 0;
            isException = false;

            Mode1_confirm.DialogResult = DialogResult.OK;
            Mode2_confirm.DialogResult = DialogResult.OK;
        }
        //控件初始化
        public void Init()
        {
            ipTextBox1.Text = cameraIP;
            ipTextBox2.Text = serverIP;
            Server_port.Text = serverPort.ToString();
            EE3versionid.Text = version;
            TofEE31.Text = temperature.ToString();
            Mode1_roadnum1.Text = roadnum1.ToString();
            Mode1_roadnum2.Text = roadnum2.ToString();
            Mode2_nightTstream.Text = nighttrafficstream.ToString();
            Mode2_uploadtime.Text = uploadtime.ToString();
            //groupbox显示控制
            EE3DCgroupBox1.Visible = true;
            EE3DCgbmode1.Visible = false;
            EE3DCgbmode2.Visible = false;
            //“当前功能模式”高亮显示
            switch (runMode)
            {
                case (int)EE3RunMode.Mode1:
                    EE3mode1.Checked = true;
                    EE3mode1.ForeColor = Color.ForestGreen;
                    break;
                case (int)EE3RunMode.Mode2:
                    EE3mode2.Checked = true;
                    EE3mode2.ForeColor = Color.ForestGreen;
                    break;
                case (int)EE3RunMode.Mode0:
                    EE3mode0.Checked = true;
                    EE3mode0.ForeColor = Color.ForestGreen;
                    break;
                default:
                    break;
            }
            //“分辨率”显示
            switch (resolution)
            {
                case 1:
                    resolutionEE3.Text = "960 * 544";
                    break;
                case 2:
                    resolutionEE3.Text = "1920 * 1088";
                    break;
                default:
                    break;
            }
            //"状态栏信息"
            StringBuilder sb = new StringBuilder();
            if (isException)
            {
                sb.Append("当前相机的配置信息有误，请重新配置。");
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                switch (isreadflash)
                {
                    case 0:
                        sb.Append("当前从相机Flash中读取的配置信息可能有误，请重新配置。");
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        break;
                    case 1:
                        sb.Append("读取配置成功。");
                        toolStripStatusLabel1.ForeColor = Color.ForestGreen;
                        break;
                    default:
                        sb.Append("出错了");
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        break;
                }
            }
            
            toolStripStatusLabel1.Text = sb.ToString();

        }
        //Mode1选择
        private void EE3mode1_CheckedChanged(object sender, EventArgs e)
        {
            runMode = (int)EE3RunMode.Mode1;
        }
        //Mode2选择
        private void EE3mode2_CheckedChanged(object sender, EventArgs e)
        {
            runMode = (int)EE3RunMode.Mode2;
        }
        //Mode0选择
        private void EE3mode0_CheckedChanged(object sender, EventArgs e)
        {
            runMode = (int)EE3RunMode.Mode0;
        }

        //“下一步”
        private void EE3BCnextStep_Click(object sender, EventArgs e)
        {
            switch (runMode)
            {

                case (int)EE3RunMode.Mode0:  //暂时共用
                    EE3DCgbmode1.Visible = true;
                    EE3DCgbmode2.Visible = false;
                    break;
                case (int)EE3RunMode.Mode1:
                    EE3DCgbmode1.Visible = true;
                    EE3DCgbmode2.Visible = false;
                    break;
                case (int)EE3RunMode.Mode2:
                    EE3DCgbmode1.Visible = false;
                    EE3DCgbmode2.Visible = true;
                    break;
                default:
                    MessageBox.Show("请选择相机的工作模式！");
                    break;
            }
            //当前路道高亮显示
            switch (roadnum1)
            {
                case 0:
                    Mode1_east.Checked = true;
                    Mode1_east.ForeColor = Color.ForestGreen;
                    break;
                case 3:
                    Mode1_south.Checked = true;
                    Mode1_south.ForeColor = Color.ForestGreen;
                    break;
                case 6:
                    Mode1_west.Checked = true;
                    Mode1_west.ForeColor = Color.ForestGreen;
                    break;
                case 9:
                    Mode1_north.Checked = true;
                    Mode1_north.ForeColor = Color.ForestGreen;
                    break;
                default:
                    Mode1_selfdefine.Checked = true;
                    Mode1_selfdefine.ForeColor = Color.ForestGreen;
                    break;
            }

        }
        //“上一步”
        private void Mode1_lastStep_Click(object sender, EventArgs e)
        {
            EE3DCgroupBox1.Visible = true;
            EE3DCgbmode1.Visible = false;
        }

        private void Mode2_lastStep_Click(object sender, EventArgs e)
        {
            EE3DCgroupBox1.Visible = true;
            EE3DCgbmode2.Visible = false;
        }

        //“东”
        private void Mode1_east_CheckedChanged(object sender, EventArgs e)
        {
            Mode1_roadnum1.Text = "0";
            Mode1_roadnum2.Text = "1";
        }
        //“南”
        private void Mode1_south_CheckedChanged(object sender, EventArgs e)
        {
            Mode1_roadnum1.Text = "3";
            Mode1_roadnum2.Text = "4";
        }
        //“西”
        private void Mode1_west_CheckedChanged(object sender, EventArgs e)
        {
            Mode1_roadnum1.Text = "6";
            Mode1_roadnum2.Text = "7";
        }
        //“北”
        private void Mode1_north_CheckedChanged(object sender, EventArgs e)
        {
            Mode1_roadnum1.Text = "9";
            Mode1_roadnum2.Text = "10";
        }
        //“自定义”
        private void Mode1_selfdefine_CheckedChanged(object sender, EventArgs e)
        {
            Mode1_roadnum1.Text = "0";
            Mode1_roadnum2.Text = "0";
        }
        //Mode1确认配置
        private void Mode1_confirm_Click(object sender, EventArgs e)
        {
            cameraIP = ipTextBox1.Text;
            serverIP = ipTextBox2.Text;
            serverPort = Int32.Parse(Server_port.Text);
            roadnum1 = Int32.Parse(Mode1_roadnum1.Text);
            roadnum2 = Int32.Parse(Mode1_roadnum2.Text);

            if (Server_port.Text == "") Server_port.Text = "0";
            if (Mode1_roadnum1.Text == "") Mode1_roadnum1.Text = "0";
            if (Mode1_roadnum2.Text == "") Mode1_roadnum2.Text = "0";
          
        }
        //Mode2确认配置
        private void Mode2_confirm_Click(object sender, EventArgs e)
        {
            cameraIP = ipTextBox1.Text;
            serverIP = ipTextBox2.Text;
            serverPort = Int32.Parse(Server_port.Text);
            uploadtime = Int32.Parse(Mode2_uploadtime.Text);
            nighttrafficstream = Int32.Parse(Mode2_nightTstream.Text);

            if (Server_port.Text == "") Server_port.Text = "0";
            if (Mode2_uploadtime.Text == "") Mode2_uploadtime.Text = "0";
            if (Mode2_nightTstream.Text == "") Mode2_nightTstream.Text = "0";

        }
        
        //其它
        private void Key_Press(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\b') return;
            if (e.KeyChar == 0x20)
            {
                e.Handled = true;
                return;
            }

            TextBox tb = (TextBox)sender;
            string tmp = tb.Text.ToString() + e.KeyChar.ToString();

            try
            {
                Int32.Parse(tmp);
            }
            catch (Exception ee)
            {
                e.Handled = true;
            }
        }
        //动态显示车道改变
        private void Mode1_roadnum1_TextChanged(object sender, EventArgs e)
        {
            switch (Mode1_roadnum1.Text)
            {
                case "0":
                    road1label.Text = "东左";
                    break;
                case "1":
                    road1label.Text = "东直";
                    break;
                case "2":
                    road1label.Text = "东右";
                    break;
                case "3":
                    road1label.Text = "南左";
                    break;
                case "4":
                    road1label.Text = "南直";
                    break;
                case "5":
                    road1label.Text = "南右";
                    break;
                case "6":
                    road1label.Text = "西左";
                    break;
                case "7":
                    road1label.Text = "西直";
                    break;
                case "8":
                    road1label.Text = "西右";
                    break;
                case "9":
                    road1label.Text = "北左";
                    break;
                case "10":
                    road1label.Text = "北直";
                    break;
                case "11":
                    road1label.Text = "北右";
                    break;
                default:
                    road1label.Text = "Error";
                    break;
            }
        }

        private void Mode1_roadnum2_TextChanged(object sender, EventArgs e)
        {
            switch (Mode1_roadnum2.Text)
            {
                case "0":
                    road2label.Text = "东左";
                    break;
                case "1":
                    road2label.Text = "东直";
                    break;
                case "2":
                    road2label.Text = "东右";
                    break;
                case "3":
                    road2label.Text = "南左";
                    break;
                case "4":
                    road2label.Text = "南直";
                    break;
                case "5":
                    road2label.Text = "南右";
                    break;
                case "6":
                    road2label.Text = "西左";
                    break;
                case "7":
                    road2label.Text = "西直";
                    break;
                case "8":
                    road2label.Text = "西右";
                    break;
                case "9":
                    road2label.Text = "北左";
                    break;
                case "10":
                    road2label.Text = "北直";
                    break;
                case "11":
                    road2label.Text = "北右";
                    break;
                default:
                    road2label.Text = "Error";
                    break;
            }
        }

        private int res;
        //关闭窗口时，断开本次连接
        private void EE3DefaultConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            res = SingletonSocket.Instance.SendCommand(CommonData.CANCELCMD, 0);
        }

    }
}
