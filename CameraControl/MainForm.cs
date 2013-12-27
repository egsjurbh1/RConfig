/*
 * Created by SharpDevelop.
 * User: Wragon,LQ
 * Date: 2012/4/22
 * Update: 2013/12/26
 * Version:0.1.0
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

enum STSTUS
{

}

namespace CameraControl
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private String choseIP = null;
        private int deviceID = 0;

        public MainForm()
        {
            //

            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();//初始化

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            SingletonSocket.Instance.SetForm(this);
            CommonFunc.InitConvertTable();
            Init();

        }
        //初始化
        private void Init()
        {
            CommonData.Bmps[0] = null;
            CommonData.Bmps[1] = null;
            CommonData.Bmps[2] = null;
            CommonData.curPos = 0;
            PicBox.Image = null;
            choseIP = null;
            isRec = false;
            isVideoing = false;
        }

        #region 广播时间
        private void BroadTime_Click(object sender, EventArgs e)
        {
            SingletonSocket.Instance.BroadCastTime();
        }
        #endregion

        #region 查询显示在线设备
        private Thread ThFindClients = null;
        //显示在线设备
        void ConnectButtonClick(object sender, EventArgs e)
        {
            mainProgressBar.Visible = true;
            mainProgressBar.Value = 0;
            CameraList.Nodes.Clear();

            if (ThFindClients == null)
            {
                ThreadStart ts = new ThreadStart(SingletonSocket.Instance.FindClients);
                ThFindClients = new Thread(ts);
                ThFindClients.Start();

            }
            //UDP广播查询在线设备
            SingletonSocket.Instance.SayHello();

        }

        //更新IP和设备信息
        public void UpdateCameraInfo(CameraInfo info)
        {
            string ip = info.IP;
            //相机IP加入节点
            if (!CameraList.Nodes.ContainsKey(ip))
            {
                CameraList.Nodes.Add(ip, ip);

                foreach (String cam in info.camera)
                {
                    CameraList.Nodes[ip].Nodes.Add(cam, cam);
                }
            }
            CameraList.Sort();//列表排序
            ClassifyDevice();//设备分类
            mainProgressBar.Value = 100;
        }

        //分类整理
        private void ClassifyDevice()
        {

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            foreach (TreeNode nd in CameraList.Nodes)
            {
                if (nd.FirstNode.Name == "EagleEye3智能相机")
                {
                    nd.ForeColor = Color.Blue;//蓝
                    ++count2;
                }
                else if (nd.FirstNode.Name == "EagleEye3E智能相机")
                {
                    nd.ForeColor = Color.BlueViolet;//紫
                    ++count3;
                }
                else if (nd.FirstNode.Name == "海达SM-X-I型")
                {
                    nd.ForeColor = Color.SeaGreen;//绿
                    ++count1;
                }


            }
            //设备统计信息状态栏显示
            StringBuilder sb = new StringBuilder();
            sb.Append("当前在线设备：");
            if (count1 > 0)
            {
                sb.Append("海达SM-X-I型 ");
                sb.Append(count1.ToString());
                sb.Append("台；");
            }
            if (count2 > 0)
            {
                sb.Append("EagleEye3智能相机 ");
                sb.Append(count2.ToString());
                sb.Append("台；");
            }
            if (count3 > 0)
            {
                sb.Append("EagleEye3E智能相机 ");
                sb.Append(count3.ToString());
                sb.Append("台；");
            }
            toolStripStatusLabel1.Text = sb.ToString();

        }


        //点击IP节点
        void CameraListClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            mainProgressBar.Visible = false;
            //树的第一级
            if (e.Node.Level == 0)
            {
                Clear();
                //获取IP地址
                choseIP = e.Node.Name.ToString();
                Device.Enabled = true;
                //按设备类型限制可用按钮
                if (e.Node.FirstNode.Name.Contains("EagleEye3"))
                {
                    CurConfigSet.Visible = false;
                    DefaultConfig.Visible = false;               
                    UpdateDsp.Visible = false;
                    UpdateApp.Visible = false;
                    rebootToolStripMenuItem.Visible = true;
                    CoilSet.Enabled = true;
                    EE3DefaultConfig.Visible = true; 
                    deviceID = CommonData.DEVICETYPEOFEE3;
                }
                else if (e.Node.FirstNode.Name == "海达SM-X-I型")
                {
                    CurConfigSet.Visible = true;
                    DefaultConfig.Visible = true;
                    CoilSet.Visible = true;
                    UpdateDsp.Visible = true;
                    UpdateApp.Visible = true;
                    rebootToolStripMenuItem.Visible = true;
                    EE3DefaultConfig.Visible = false;
                    deviceID = 1;
                }

            }
            
        }
        #endregion

        #region 线圈配置

        private Thread ThImageRecv = null;//声明线程
        private bool isVideoing = false;
        private bool isSpeedUsed = false;
        private double areaofCoil = 0;
        private double carspeed = 0;//车速
        private const double carlongN = 5;//车长5米
        private const double expC1 = 0.007013;//EE3型参数一
        private const double expC2 = 14.22;//EE3型参数二

        private void CoilSet_Click(object sender, EventArgs e)
        {
            curChs = -1;
            isRec = false;
            isVideoing = true;
            if (choseIP == null)
            {
                MessageBox.Show("请选择设备!");
                return;
            }

            //初始化连接
            if (!SingletonSocket.Instance.InitComSock(choseIP))
            {
                MessageBox.Show("连接失败!");
                return;
            }
            //连接检测
            if (!SingletonSocket.Instance.CheckConnection())
            {
                MessageBox.Show("连接失败!");
                return;
            }

            PointsetForm ifm = new PointsetForm();

            if (ifm.ShowDialog() == DialogResult.OK)
            {
                isSpeedUsed = ifm.Bisused;
                carspeed = ifm.Speed;
            }

            if (!SingletonSocket.Instance.CheckConnection())
            {
                if (choseIP != null)
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }
            }


            if (SingletonSocket.Instance.CheckConnection())
            {
                object lck = new object();
                lock (lck)
                {
                    CommonData.Flag = true;
                }
                //海达SM-X-I
                if (deviceID == 1)
                {
                    //创建线程
                    ThreadStart ts = new ThreadStart(SingletonSocket.Instance.RecvImagesYUV420);
                    ThImageRecv = new Thread(ts);
                    ThImageRecv.Start();
                    isSpeedUsed = false;//默认不开启最大线框模式
                }
                //EagleEye3型
                else if (deviceID == CommonData.DEVICETYPEOFEE3)
                {
                    ThreadStart ts = new ThreadStart(SingletonSocket.Instance.RecvImagesYUV422);
                    ThImageRecv = new Thread(ts);
                    ThImageRecv.Start();
                    //此处添加计算线框大小公式
                    areaofCoil = (carlongN / carspeed * 1000 - expC2) / expC1;

                }
                //EagleEye3E型
                else if (deviceID == CommonData.DEVICETYPEOFEE3E)
                {
                    ThreadStart ts = new ThreadStart(SingletonSocket.Instance.RecvImagesYUV422);
                    ThImageRecv = new Thread(ts);
                    ThImageRecv.Start();
                    //此处添加计算线框大小公式
                    areaofCoil = (carlongN / carspeed * 1000 - expC2) / expC1;
                }

                Thread.Sleep(50);
                int res = SingletonSocket.Instance.SendCommand(CommonData.STARTVIDEO, 1);
                if (res == 1)
                {
                    MessageBox.Show("开始传输视频, 双击画面，截取当前图片。");
                    MsgShow.ForeColor = Color.ForestGreen;
                    MsgShow.Text = "双击画面，截取当前图片。";
                }
            }

            SingletonSocket.Instance.CloseComSock();
        }

        //视频流更新
        public void UpdateVideo()
        {
            curChs = CommonData.curPos;
            //设备分类
            if (deviceID == 1)
            {
                CommonData.Bmps[curChs] = CommonFunc.ConvertYUV420(CommonData.Buffer1, CommonData.Width1, CommonData.Height1);
            }
            else if (deviceID == CommonData.DEVICETYPEOFEE3 || deviceID == CommonData.DEVICETYPEOFEE3E)
            {
                CommonData.Bmps[curChs] = CommonFunc.ConvertYUV422(CommonData.Buffer2, CommonData.Width2, CommonData.Height2);
            }
            //控件显示图像
            Image img = Image.FromHbitmap(CommonData.Bmps[curChs].GetHbitmap());
            PicBox.Image = img;

            switch (curChs)
            {
                case 0:
                    //PicOne.Image = img;
                    break;

                case 1:
                    //PicTwo.Image = img;
                    break;

                case 2:
                    //PicTrd.Image = img;
                    break;
            }
            //设备分类
            if (deviceID == 1)
            {
                Array.Copy(CommonData.Buffer1, 0, CommonData.HisBuf1, curChs * CommonData.FrmSize1, CommonData.FrmSize1);
            }
            else if (deviceID == CommonData.DEVICETYPEOFEE3 || deviceID == CommonData.DEVICETYPEOFEE3E)
            {
                Array.Copy(CommonData.Buffer2, 0, CommonData.HisBuf2, curChs * CommonData.FrmSize2, CommonData.FrmSize2);
            }
            CommonData.curPos = (CommonData.curPos + 1) % 3;
        }

        private void Pic_DClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (curChs >= 0) isRec = true;
            isVideoing = false;

            MsgShow.Text = "";

            if (!SingletonSocket.Instance.CheckConnection())
            {
                if (choseIP != null)
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }
            }

            if (SingletonSocket.Instance.CheckConnection())
            {
                int res = SingletonSocket.Instance.SendCommand(CommonData.ENDVIDEO, 1);
                if (res == 1)
                {
                    MessageBox.Show("停止传输视频");
                    object lck = new object();
                    lock (lck)
                    {
                        CommonData.Flag = false;
                    }
                    CancelConfig.Enabled = true;
                    if (isSpeedUsed)
                    {
                        MsgShow.Text = "用鼠标左键画出两点，确定监控区域的宽度。";
                    }
                    else
                    {
                        MsgShow.Text = "用鼠标左键画出一个或两个四边形，确定监控区域。";
                    }
                }
            }

            SingletonSocket.Instance.CloseComSock();
        }


        private void Clear()
        {
            if (isVideoing && SingletonSocket.Instance.CheckConnection())
            {
                SingletonSocket.Instance.SendCommand(CommonData.ENDVIDEO, 0);
                Init();
            }
        }

        private int curChs = -1;

        void PicClick(object sender, EventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
        }

        private List<Point> rec = new List<Point>();
        private List<Point> reca = new List<Point>();
        private bool isRec = false;
        private int Zonelong = 0;
        private int ZoneWide = 0;
        private int theY = 0;
        private int theX = 0;
        private int PointX = 0;
        private int PointY = 0;

        //画点，鼠标点击事件触发
        void Pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (!isRec) return;
            int lengtha = reca.Count;
            int length = rec.Count;

            switch (isSpeedUsed)
            {
                //最大框模式
                case true:

                    if (lengtha < 6)
                    {
                        reca.Add(new Point(e.X, e.Y));
                    }
                    else
                    {
                        return;
                    }
                    if (lengtha >= 0)
                    {
                        Graphics g = PicBox.CreateGraphics();
                        //第一个框
                        while (reca.Count == 2)
                        {
                            Zonelong = Math.Abs(reca[0].X - reca[1].X);
                            //此处加入Zonelong == 0时的判断
                            if (Zonelong == 0)
                            {
                                MessageBox.Show("超出设置范围!请重新配置");
                                rec.Remove(reca[0]);
                                //rec.Remove(rec[1]);
                            }
                            else
                            {
                                g.DrawLine(Pens.Green, reca[0], reca[1]);
                                Thread.Sleep(1000);//延迟1秒
                                ZoneWide = (int)(areaofCoil / Zonelong);
                                theY = reca[0].Y > reca[1].Y ? reca[1].Y : reca[0].Y;//取Y轴坐标较小者
                                theX = reca[0].X > reca[1].X ? reca[1].X : reca[0].X;//取X轴坐标较小者                   
                                g.DrawRectangle(Pens.Red, theX, theY, Zonelong, ZoneWide);
                                MsgShow.Text = "在红色矩形框内，用鼠标左键画出一点，确定第一个线框大小。";
                                break;
                            }
                        }
                        while (reca.Count == 3)
                        {
                            if ((reca[2].X > theX) && (reca[2].X <= Zonelong + theX) && (reca[2].Y > theY) && (reca[2].Y <= ZoneWide + theY))
                            {
                                PointX = reca[2].X;
                                PointY = reca[2].Y;
                                //4点转存
                                rec.Add(new Point(theX, theY));
                                rec.Add(new Point(PointX, theY));
                                rec.Add(new Point(PointX, PointY));
                                rec.Add(new Point(theX, PointY));
                                g.DrawRectangle(Pens.Yellow, theX, theY, PointX - theX, PointY - theY);
                                MsgShow.Text = "第一个虚拟线框配置成功。可继续配置或单击鼠标右键选择功能。";
                                break;

                            }
                            else
                            {
                                MessageBox.Show("请勿超出线框的最大配置范围!");
                                reca.Remove(reca[2]);
                            }
                        }
                        //第二个框
                        while(reca.Count == 5)
                        {
                            
                            Zonelong = Math.Abs(reca[3].X - reca[4].X);
                            if (Zonelong == 0)
                            {
                                MessageBox.Show("超出设置范围!请重新配置");
                                reca.Remove(reca[3]);
                            }
                            else
                            {
                                g.DrawLine(Pens.Green, reca[3], reca[4]);
                                Thread.Sleep(1000);
                                ZoneWide = (int)(areaofCoil / Zonelong);
                                theY = reca[3].Y > reca[4].Y ? reca[4].Y : reca[3].Y;//取Y轴坐标较小者
                                theX = reca[3].X > reca[4].X ? reca[4].X : reca[3].X;//取X轴坐标较小者                   
                                g.DrawRectangle(Pens.Red, theX, theY, Zonelong, ZoneWide);
                                MsgShow.Text = "在红色矩形框内，用鼠标左键画出一点，确定第二个线框大小。";
                                break;
                            }
                        }
                        while (reca.Count == 6)
                        {
                            if ((reca[5].X > theX) && (reca[5].X <= Zonelong + theX) && (reca[5].Y > theY) && (reca[5].Y <= ZoneWide + theY))
                            {
                                PointX = reca[5].X;
                                PointY = reca[5].Y;
                                //4点转存
                                rec.Add(new Point(theX, theY));
                                rec.Add(new Point(PointX, theY));
                                rec.Add(new Point(PointX, PointY));
                                rec.Add(new Point(theX, PointY));
                                g.DrawRectangle(Pens.Yellow, theX, theY, PointX - theX, PointY - theY);
                                MsgShow.Text = "第二个虚拟线框配置成功。单击鼠标右键可以选择确认配置。";
                                break;

                            }
                            else
                            {
                                MessageBox.Show("请勿超出线框的最大配置范围!");
                                reca.Remove(reca[5]);
                            }
                        }

                    }

                    if (rec.Count == 4 || rec.Count == 8)
                    {
                        EnsureConfig.Enabled = true;
                    }
                    else
                    {
                        EnsureConfig.Enabled = false;
                    }

                    if (reca.Count > 0)
                    {
                        ResetConfig.Enabled = true;
                    }
                    else
                    {
                        EnsureConfig.Enabled = false;
                    }
                    break;

                //直接画点模式
                case false:

                    if (length < 8)
                    {
                        rec.Add(new Point(e.X, e.Y));
                    }
                    else
                    {
                        return;
                    }
                    if (length > 0)
                    {
                        Graphics g = PicBox.CreateGraphics();
                        if (length != 4) g.DrawLine(Pens.Red, rec[length - 1], rec[length]);
                        if (length == 3) g.DrawLine(Pens.Red, rec[length], rec[0]);
                        if (length == 7) g.DrawLine(Pens.Red, rec[length], rec[4]);
                    }

                    if (rec.Count == 4 || rec.Count == 8)
                    {
                        EnsureConfig.Enabled = true;
                    }
                    else
                    {
                        EnsureConfig.Enabled = false;
                    }

                    if (rec.Count > 0)
                    {
                        ResetConfig.Enabled = true;
                    }
                    else
                    {
                        EnsureConfig.Enabled = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private void Mouse_RightClk(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Right) return;
            CNXMenu.Show((Control)sender, e.Location);
        }
        //确认配置
        private void EnsureConfig_Click(object sender, EventArgs e)
        {
            MsgShow.Text = "";
            if (CameraList.SelectedNode == null || choseIP == null)
            {
                MessageBox.Show("请确定配置的设备!");
                return;
            }
            if (rec.Count != 4 && rec.Count != 8)
            {
                MessageBox.Show("虚拟线圈设置错误!");
                return;
            }

            if (rec.Count == 4)
            {
                for (int i = 0; i < 4; i++) rec.Add(new Point(0, 0));
            }

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            Point temp1 = rec[2]; rec[2] = rec[3]; rec[3] = temp1;
            Point temp2 = rec[6]; rec[6] = rec[7]; rec[7] = temp2;
            foreach (Point p in rec)
            {
                bw.Write(Encoding.ASCII.GetBytes(string.Format("{0} {1}\r\n", p.X, p.Y)));
            }

            if (MessageBox.Show("设置线圈") == DialogResult.OK)
            {
                if (!SingletonSocket.Instance.CheckConnection())
                {
                    if (choseIP != null)
                    {
                        SingletonSocket.Instance.InitComSock(choseIP);
                    }
                }

                int res = SingletonSocket.Instance.SendCommand(CommonData.GETCONFIGFILE, (int)ms.Length, ms.ToArray());


                if (res == 1)
                {
                    if (MessageBox.Show("设置成功，是否重启!", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                        if (res == 1)
                        {
                            MessageBox.Show("设备重启中...");
                            Init();
                        }
                        SingletonSocket.Instance.CloseComSock();
                    }
                }

            }
            ms.Close();

            SingletonSocket.Instance.CloseComSock();
            //连接已关闭，“设备配置”按钮不可用
            Device.Enabled = false;
        }
        //重新配置
        private void ResetConfig_Click(object sender, EventArgs e)
        {
            rec.Clear();
            reca.Clear();
            PicBox.Refresh();
            EnsureConfig.Enabled = false;
            ResetConfig.Enabled = false;
            MsgShow.Text = "";
            if (isSpeedUsed)
            {
                MsgShow.Text = "用鼠标左键画出两点，确定监控区域的宽度。";
            }
            else
            {
                MsgShow.Text = "用鼠标左键画出一个或两个四边形，确定监控区域。";
            }
        }
        //取消配置
        private void CancelConfig_Click(object sender, EventArgs e)
        {
            if (!SingletonSocket.Instance.CheckConnection())
            {
                if (choseIP != null)
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }
            }
            //关闭连接
            SingletonSocket.Instance.SendCommand(CommonData.CANCELCMD, 0);
            MessageBox.Show("取消了配置");
            rec.Clear();
            PicBox.Refresh();
            EnsureConfig.Enabled = false;
            ResetConfig.Enabled = false;
            isRec = true;
            Init();
            MsgShow.Text = "";  
            //连接已关闭，“设备配置”按钮不可用
            Device.Enabled = false;
        }

        #endregion

        #region 重启设备
        void RebootButtonClick(object sender, EventArgs e)
        {
            int res = 0;
            int ress = 0;
            if (choseIP == null)
            {
                MessageBox.Show("请选择设备!");
                return;
            }

            //初始化连接
            if (!SingletonSocket.Instance.InitComSock(choseIP))
            {
                MessageBox.Show("连接失败!");
                return;
            }
            //连接检测
            if (!SingletonSocket.Instance.CheckConnection())
            {
                MessageBox.Show("连接失败!");
                return;
            }
            if (!SingletonSocket.Instance.CheckConnection())
            {

                if (choseIP != null)
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }
            }

            if (SingletonSocket.Instance.CheckConnection())
            {
                if (MessageBox.Show("确认要重启设备吗？", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                    ress = 0;
                }
                else
                {
                    res = SingletonSocket.Instance.SendCommand(CommonData.CANCELCMD, 0);
                    ress = 1;
                }
                if (ress == 0 && res == 1)
                {
                    MessageBox.Show("设备重启中...");
                    Init();
                }
                else if (ress == 1 && res == 1)
                {
                    MessageBox.Show("取消重启");
                }
                else
                {
                    MessageBox.Show("重启失败！");
                }

                SingletonSocket.Instance.CloseComSock();
                //连接已关闭，“设备配置”按钮不可用
                Device.Enabled = false;
            }
        }
        //启动监控
        void OpenExeButtonClick(object sender, EventArgs e)
        {
            if (!SingletonSocket.Instance.CheckConnection())
            {
                if (choseIP != null)
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }
            }
            if (SingletonSocket.Instance.CheckConnection())
            {
                int res = SingletonSocket.Instance.SendCommand(CommonData.OPENEXE, 0);
                if (res == 1)
                {
                    MessageBox.Show("启动摄像头监控");
                }
                SingletonSocket.Instance.CloseComSock();
            }
            SingletonSocket.Instance.CloseComSock();
        }

        #endregion

        #region 海达SM-X-I出厂设置
        private void DefaultConfig_Click(object sender, EventArgs e)
        {

            mainProgressBar.Visible = true;
            mainProgressBar.Value = 0;//进度条
            
            if (choseIP == null)
            {
                MessageBox.Show("请选择设备!");
                return;
            }

            //初始化连接
            if (!SingletonSocket.Instance.InitComSock(choseIP))
            {
                MessageBox.Show("连接失败!");
                return;
            }
            //连接检测
            if (!SingletonSocket.Instance.CheckConnection())
            {
                MessageBox.Show("连接失败!");
                return;
            }

            //获取当前配置信息
            MemoryStream ms = SingletonSocket.Instance.RecvCfgData(CommonData.GETDAFAULTCFG, 0);

            InitForm ifm = new InitForm();
            if (ms.Length == 0)
            {
                MessageBox.Show("还未进行车道编号设置!");
            }
            else
            {
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                mainProgressBar.Value = 20;
                ifm.CamIP = sr.ReadLine();
                ifm.SrvIP = sr.ReadLine();
                ifm.SrvPort = Convert.ToInt32(sr.ReadLine());
                ifm.CamPX = Convert.ToInt32(sr.ReadLine());
                ifm.CamPY = Convert.ToInt32(sr.ReadLine());
                mainProgressBar.Value = 60;
                ifm.Number = Convert.ToInt32(sr.ReadLine());
                ifm.SendZq = Convert.ToInt32(sr.ReadLine());
                ifm.Stime = Convert.ToInt32(sr.ReadLine());
                ifm.Etime = Convert.ToInt32(sr.ReadLine());
                ifm.NumNt = Convert.ToInt32(sr.ReadLine());
                sr.Close();

                ifm.Init();
            }
            mainProgressBar.Value = 100;


            if (ifm.ShowDialog() == DialogResult.OK)
            {
                MemoryStream mms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(mms);
                bw.Write(Encoding.ASCII.GetBytes(ifm.CamIP + "\r\n"));
                bw.Write(Encoding.ASCII.GetBytes(ifm.SrvIP + "\r\n"));
                bw.Write(Encoding.ASCII.GetBytes(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", ifm.SrvPort, ifm.CamPX, ifm.CamPY, ifm.Number)));
                bw.Write(Encoding.ASCII.GetBytes(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", ifm.SendZq, ifm.Stime, ifm.Etime, ifm.NumNt)));

                if (MessageBox.Show("确定要修改处理板出厂设置!", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!SingletonSocket.Instance.CheckConnection())
                    {
                        if (choseIP != null)
                        {
                            SingletonSocket.Instance.InitComSock(choseIP);
                        }
                    }

                    int res = SingletonSocket.Instance.SendCommand(CommonData.DEFAULTCONFIG, (int)mms.Length, mms.ToArray());

                    if (res == 1)
                    {

                        if (MessageBox.Show("配置成功，是否重启!", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                            if (res == 1)
                            {
                                MessageBox.Show("设备重启中...");
                                Init();
                            }
                            else
                            {
                                MessageBox.Show("重启失败!");
                            }
                            SingletonSocket.Instance.CloseComSock();
                        }
                    }

                    SingletonSocket.Instance.CloseComSock();
                }

                bw.Close();
                mms.Close();

            }
            SingletonSocket.Instance.CloseComSock();
            mainProgressBar.Visible = false;
        }
        #endregion

        #region 海达SM-X-I当前配置
        private void CurConfigSet_Click(object sender, EventArgs e)
        {

            mainProgressBar.Visible = true;
            mainProgressBar.Value = 0;//进度条
            
            if (choseIP == null)
            {
                MessageBox.Show("请选择设备!");
                return;
            }

            //初始化连接
            if (!SingletonSocket.Instance.InitComSock(choseIP))
            {
                MessageBox.Show("连接失败!");
                return;
            }
            //连接检测
            if (!SingletonSocket.Instance.CheckConnection())
            {
                MessageBox.Show("连接失败!");
                return;
            }

            MemoryStream ms = SingletonSocket.Instance.RecvCfgData(CommonData.GETCONFIG, 0);

            if (ms.Length == 0)
            {
                MessageBox.Show("没有当前配置，请先配置出厂设置!");
            }
            else
            {
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                mainProgressBar.Value = 30;//进度条
                string mIP = sr.ReadLine();
                string sIP = sr.ReadLine();
                string sPort = sr.ReadLine();
                string mWidth = sr.ReadLine();
                string mHeight = sr.ReadLine();
                string XQ = sr.ReadLine();
                string zq = sr.ReadLine();
                string srtime = sr.ReadLine();
                mainProgressBar.Value = 60;//进度条
                string edtime = sr.ReadLine();
                string nt = sr.ReadLine();
                sr.Close();
                ms.Close();

                InputForm ifm = new InputForm();
                ifm.CamIP = mIP;
                ifm.CamPX = Convert.ToInt32(mWidth);
                ifm.CamPY = Convert.ToInt32(mHeight);
                ifm.SrvIP = sIP;
                ifm.SrvPort = Convert.ToInt32(sPort);
                ifm.SendZq = Convert.ToInt32(zq);
                ifm.Stime = Convert.ToInt32(srtime);
                ifm.Etime = Convert.ToInt32(edtime);
                ifm.NumNt = Convert.ToInt32(nt);

                ifm.Init();
                mainProgressBar.Value = 100;//进度条


                if (ifm.ShowDialog() == DialogResult.OK)
                {
                    MemoryStream mms = new MemoryStream();
                    BinaryWriter bw = new BinaryWriter(mms);
                    bw.Write(Encoding.ASCII.GetBytes(ifm.CamIP + "\r\n"));
                    bw.Write(Encoding.ASCII.GetBytes(ifm.SrvIP + "\r\n"));
                    bw.Write(Encoding.ASCII.GetBytes(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", ifm.SrvPort, ifm.CamPX, ifm.CamPY, XQ)));
                    bw.Write(Encoding.ASCII.GetBytes(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", ifm.SendZq, ifm.Stime, ifm.Etime, ifm.NumNt)));

                    int res = SingletonSocket.Instance.SendCommand(CommonData.SETCONFIG, (int)mms.Length, mms.ToArray());
                    if (res == 1)
                    {
                        if (MessageBox.Show("配置成功，是否重启!", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                            if (res == 1)
                            {
                                MessageBox.Show("设备重启中...");
                                Init();
                            }
                            else
                            {
                                MessageBox.Show("重启失败!");
                            }
                            SingletonSocket.Instance.CloseComSock();
                        }
                    }
                    else
                    {
                        MessageBox.Show("设置失败！");
                    }

                    bw.Close();
                    mms.Close();
                }
            }
            SingletonSocket.Instance.CloseComSock();
            mainProgressBar.Visible = false;

        }
        #endregion

        #region EagleEye3功能配置
        private void EE3DefaultConfig_Click(object sender, EventArgs e)
        {
            int res = 0;
            int ress = 0;
            int ee3cameraIP = 0;
            int ee3serverIP = 0;

            if (choseIP == null)
            {
                MessageBox.Show("请选择设备!");
                return;
            }
            //初始化连接
            if (!SingletonSocket.Instance.InitComSock(choseIP))
            {
                MessageBox.Show("连接失败!");
                return;
            }
            //连接检测
            if (!SingletonSocket.Instance.CheckConnection())
            {
                MessageBox.Show("连接失败!");
                return;
            }
            //获取当前配置信息
            MemoryStream ms = SingletonSocket.Instance.RecvCfgData(CommonData.EE3_GETCONFIG, 0);
            EE3DefaultConfig ifm = new EE3DefaultConfig();

            if (ms.Length == 0)
            {
                MessageBox.Show("还未参数设置!");
            }
            else
            {
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                mainProgressBar.Value = 20;
                //按行读取配置信息
                try
                {
                    ifm.RunMode = Convert.ToInt32(sr.ReadLine());//1 runmode
                    ee3cameraIP = Convert.ToInt32(sr.ReadLine());//2 cameraip
                    //将int型IP地址转换为string点分式
                    byte[] bs1 = BitConverter.GetBytes(ee3cameraIP);
                    ifm.CameraIP = string.Format("{0}.{1}.{2}.{3}", bs1[0], bs1[1], bs1[2], bs1[3]);
                    ifm.Resolution = Convert.ToInt32(sr.ReadLine());//3 resolution
                    ifm.Temperature = Convert.ToInt32(sr.ReadLine());//4 temperature
                    ee3serverIP = Convert.ToInt32(sr.ReadLine());//5 serverip
                    //将int型IP地址转换为string点分式
                    byte[] bs2 = BitConverter.GetBytes(ee3serverIP);
                    ifm.ServerIP = string.Format("{0}.{1}.{2}.{3}", bs2[0], bs2[1], bs2[2], bs2[3]);
                    ifm.ServerPort = Convert.ToInt32(sr.ReadLine());//6 serverport
                    mainProgressBar.Value = 60;
                    ifm.Roadnum1 = Convert.ToInt32(sr.ReadLine());//7 roadnum
                    ifm.Roadnum2 = Convert.ToInt32(sr.ReadLine());
                    ifm.Uploadtime = Convert.ToInt32(sr.ReadLine());//8 uploadtime
                    ifm.Nighttrafficstream = Convert.ToInt32(sr.ReadLine());//9 nightTS
                    ifm.Version = sr.ReadLine();//10 version
                    ifm.IsReadFlash = Convert.ToInt32(sr.ReadLine());//11 isreadflash
                }
                catch (Exception ex)
                {
                    ifm.IsException = true;
                }

                sr.Close();
                mainProgressBar.Value = 100;
                ifm.Init();

                mainProgressBar.Visible = false;
            }

            if (ifm.ShowDialog() == DialogResult.OK)
            {
                MemoryStream mms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(mms);
                //数据打包
                switch (ifm.RunMode)
                {
                    case 0:
                        bw.Write(Encoding.ASCII.GetBytes(ifm.RunMode + "\n"));//1 
                        bw.Write(Encoding.ASCII.GetBytes(ifm.CameraIP + "\r\n"));//2
                        bw.Write(Encoding.ASCII.GetBytes(ifm.ServerIP + "\r\n"));//5
                        bw.Write(Encoding.ASCII.GetBytes(ifm.ServerPort + "\r\f\n\v"));//6 分类标记\f
                        break;
                    case 1:
                        bw.Write(Encoding.ASCII.GetBytes(ifm.RunMode + "\n"));//1 
                        bw.Write(Encoding.ASCII.GetBytes(ifm.CameraIP + "\r\n"));//2
                        bw.Write(Encoding.ASCII.GetBytes(ifm.ServerIP + "\r\n"));//5
                        bw.Write(Encoding.ASCII.GetBytes(ifm.ServerPort + "\r\f\n"));//6 分类标记\f         
                        bw.Write(Encoding.ASCII.GetBytes(ifm.Roadnum1 + "\r\n"));//7
                        bw.Write(Encoding.ASCII.GetBytes(ifm.Roadnum2 + "\r\v"));  //结束标记\v
                        break;
                    case 2:
                        bw.Write(Encoding.ASCII.GetBytes(ifm.RunMode + "\n"));//1
                        bw.Write(Encoding.ASCII.GetBytes(ifm.CameraIP + "\r\n"));//2
                        bw.Write(Encoding.ASCII.GetBytes(ifm.ServerIP + "\r\n"));//5
                        bw.Write(Encoding.ASCII.GetBytes(ifm.ServerPort + "\r\f\n"));//6 分类标记\f        
                        bw.Write(Encoding.ASCII.GetBytes(ifm.Uploadtime + "\r\n"));//8
                        bw.Write(Encoding.ASCII.GetBytes(ifm.Nighttrafficstream + "\r\v"));//9 结束标记\v
                        break;
                    default:
                        break;
                }
                if (!SingletonSocket.Instance.CheckConnection())
                {
                    if (choseIP != null)
                    {
                        SingletonSocket.Instance.InitComSock(choseIP);
                    }
                }

                if (ifm.Roadnum1 >= 0 && ifm.Roadnum1 <= 99 && ifm.Roadnum2 >= 0 && ifm.Roadnum2 <= 99)
                {
                    if (MessageBox.Show("确定要修改相机的配置吗？", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        res = SingletonSocket.Instance.SendCommand(CommonData.EE3_SETCONFIG, (int)mms.Length, mms.ToArray());
                        ress = 0;
                    }
                    else
                    {
                        res = SingletonSocket.Instance.SendCommand(CommonData.CANCELCMD, 0);
                        MessageBox.Show("已取消配置");
                        ress = 1;
                        SingletonSocket.Instance.CloseComSock();
                    }
                }
                else
                {
                    MessageBox.Show("错误的车道编号，请重新配置！");
                }

                if (res == 1 && ress == 0)
                {
                    if (MessageBox.Show("设置成功，是否重启!", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                        if (res == 1)
                        {
                            MessageBox.Show("设备重启中...");
                            Init();
                        }
                        SingletonSocket.Instance.CloseComSock();
                    }

                }

                bw.Close();
                mms.Close();

            }


            SingletonSocket.Instance.CloseComSock();
            //连接已关闭，“设备配置”按钮不可用
            Device.Enabled = false;
        }

        #endregion

        #region 海达SM-X-I程序更新
        private void UpdateDsp_Click(object sender, EventArgs e)
        {
            if (CameraInfo.DeviceType == 1)
            {
                if (choseIP == null)
                {
                    MessageBox.Show("先选择设备！");
                    return;
                }
                if (!SingletonSocket.Instance.CheckConnection())
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int size = (int)fs.Length;
                    byte[] buffer = br.ReadBytes(size);

                    int res = SingletonSocket.Instance.SendCommand(CommonData.UPDATEEXE, buffer.Length, buffer);

                    br.Close();
                    fs.Close();
                    if (res == 1)
                    {
                        if (MessageBox.Show("DSP更新成功，是否重启!", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                            if (res == 1)
                            {
                                MessageBox.Show("设备重启中...");
                                Init();
                            }
                            else
                            {
                                MessageBox.Show("重启失败!");
                            }
                            SingletonSocket.Instance.CloseComSock();
                        }
                    }
                }
                SingletonSocket.Instance.CloseComSock();
            }
            else
            {
                MessageBox.Show("该配置功能不适用于当前类型的设备！", "注意");
            }
        }

        private void UpdateApp_Click(object sender, EventArgs e)
        {
            if (CameraInfo.DeviceType == 1)
            {
                if (choseIP == null)
                {
                    MessageBox.Show("先选择设备！");
                    return;
                }
                if (!SingletonSocket.Instance.CheckConnection())
                {
                    SingletonSocket.Instance.InitComSock(choseIP);
                }

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int size = (int)fs.Length;
                    byte[] buffer = br.ReadBytes(size);

                    int res = SingletonSocket.Instance.SendCommand(CommonData.UPDATEC64, buffer.Length, buffer);

                    br.Close();
                    fs.Close();
                    if (res == 1)
                    {
                        if (MessageBox.Show("APP更新成功，是否重启!", "注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            res = SingletonSocket.Instance.SendCommand(CommonData.REBOOT, 0);
                            if (res == 1)
                            {
                                MessageBox.Show("设备重启中...");
                                Init();
                            }
                            else
                            {
                                MessageBox.Show("重启失败!");
                            }
                            SingletonSocket.Instance.CloseComSock();
                        }
                    }
                }
                SingletonSocket.Instance.CloseComSock();
            }
            else
            {
                MessageBox.Show("该配置功能不适用于当前类型的设备！", "注意");
            }
        }
        #endregion


        void PhaseToolStripMenuItemClick(object sender, EventArgs e)
        {
            PhaseForm pfm = new PhaseForm();
            if (pfm.ShowDialog() == DialogResult.OK)
            {
            }
        }

        void ExpTableToolStripMenuItemClick(object sender, EventArgs e)
        {
            ExpFrom efm = new ExpFrom();
            efm.ShowDialog();
        }

        void UpdatePhaseToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                int size = (int)fs.Length;
                byte[] buffer = br.ReadBytes(size);
                int res = SingletonSocket.Instance.SendCommand(CommonData.GETPHASECFG, buffer.Length, buffer);
                br.Close();
                fs.Close();
                if (res == 1)
                {
                    MessageBox.Show("Success!");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ThFindClients != null && ThFindClients.IsAlive)
            {
                SingletonSocket.Instance.CloseFindClients();
                Thread.Sleep(200);
            }
            if (ThImageRecv != null && ThImageRecv.IsAlive)
            {
                object lck = new object();
                lock (lck)
                {
                    CommonData.Flag = false;
                }
                Thread.Sleep(200);
            }
        }

        #region 关于
        private void AboutBtn_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }
        #endregion


































    }

    public class CameraInfo
    {
        public string IP;
        public List<string> camera = new List<string>();
        public static int DeviceType;
    }


    delegate void InfoDelegate(CameraInfo cameraInfo);
    delegate void VideoDelegate();

}
