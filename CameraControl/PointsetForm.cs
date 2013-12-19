using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraControl
{
    public partial class PointsetForm : Form
    {
        //声明
        private double speed;
        private bool bisused;
        //全局传值
        public double Speed
        {
            set { speed = value; }
            get { return speed; }
        }
        public bool Bisused
        {
            set { bisused = value; }
            get { return bisused; }
        }
        //控件初始化
        public PointsetForm()
        {
            InitializeComponent();
            PreInit();
        }
        //变量初始化
        void PreInit()
        {
            speed = 0;
            Bisused = false;
            SpeedBox.Text = speed.ToString();

            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.OK;
        }

        private void Speed60_CheckedChanged(object sender, EventArgs e)
        {
            SpeedBox.Text = "60";
        }

        private void Speed70_CheckedChanged(object sender, EventArgs e)
        {
            SpeedBox.Text = "70";
        }

        private void SpeedSelfD_CheckedChanged(object sender, EventArgs e)
        {
            SpeedBox.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            speed = double.Parse(SpeedBox.Text) / 3.6;//转换为m/s
            bisused = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            speed = double.Parse(SpeedBox.Text) / 3.6;
            bisused = false;
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
    }
}
