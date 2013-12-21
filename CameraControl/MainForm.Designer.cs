/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/4/22
 * Time: 21:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CameraControl
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CameraList = new System.Windows.Forms.TreeView();
            this.mToolStrip = new System.Windows.Forms.ToolStrip();
            this.ConnectButton = new System.Windows.Forms.ToolStripButton();
            this.Device = new System.Windows.Forms.ToolStripDropDownButton();
            this.openExeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurConfigSet = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateDsp = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateApp = new System.Windows.Forms.ToolStripMenuItem();
            this.EE3DefaultConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CoilSet = new System.Windows.Forms.ToolStripMenuItem();
            this.rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BroadTime = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.AboutBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.MsgShow = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.CNXMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EnsureConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.CNXMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CameraList
            // 
            this.CameraList.Location = new System.Drawing.Point(12, 28);
            this.CameraList.Name = "CameraList";
            this.CameraList.Size = new System.Drawing.Size(159, 576);
            this.CameraList.TabIndex = 0;
            this.CameraList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CameraListClick);
            // 
            // mToolStrip
            // 
            this.mToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectButton,
            this.Device,
            this.BroadTime,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2});
            this.mToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mToolStrip.Name = "mToolStrip";
            this.mToolStrip.Size = new System.Drawing.Size(1159, 25);
            this.mToolStrip.TabIndex = 1;
            this.mToolStrip.Text = "mToolStrip";
            // 
            // ConnectButton
            // 
            this.ConnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ConnectButton.Image = ((System.Drawing.Image)(resources.GetObject("ConnectButton.Image")));
            this.ConnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 22);
            this.ConnectButton.Text = "显示在线设备(&S)";
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // Device
            // 
            this.Device.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Device.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openExeToolStripMenuItem,
            this.CurConfigSet,
            this.DefaultConfig,
            this.UpdateDsp,
            this.UpdateApp,
            this.EE3DefaultConfig,
            this.pToolStripMenuItem1,
            this.CoilSet,
            this.rebootToolStripMenuItem});
            this.Device.Enabled = false;
            this.Device.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Device.Name = "Device";
            this.Device.Size = new System.Drawing.Size(84, 22);
            this.Device.Text = "设备管理(&D)";
            // 
            // openExeToolStripMenuItem
            // 
            this.openExeToolStripMenuItem.Enabled = false;
            this.openExeToolStripMenuItem.Name = "openExeToolStripMenuItem";
            this.openExeToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openExeToolStripMenuItem.Text = "启动监控";
            this.openExeToolStripMenuItem.Visible = false;
            this.openExeToolStripMenuItem.Click += new System.EventHandler(this.OpenExeButtonClick);
            // 
            // CurConfigSet
            // 
            this.CurConfigSet.Name = "CurConfigSet";
            this.CurConfigSet.Size = new System.Drawing.Size(136, 22);
            this.CurConfigSet.Text = "当前配置(&C)";
            this.CurConfigSet.Click += new System.EventHandler(this.CurConfigSet_Click);
            // 
            // DefaultConfig
            // 
            this.DefaultConfig.Name = "DefaultConfig";
            this.DefaultConfig.Size = new System.Drawing.Size(136, 22);
            this.DefaultConfig.Text = "出厂设置";
            this.DefaultConfig.Click += new System.EventHandler(this.DefaultConfig_Click);
            // 
            // UpdateDsp
            // 
            this.UpdateDsp.Name = "UpdateDsp";
            this.UpdateDsp.ShortcutKeyDisplayString = "";
            this.UpdateDsp.Size = new System.Drawing.Size(136, 22);
            this.UpdateDsp.Text = "更新DSP(&U)";
            this.UpdateDsp.Click += new System.EventHandler(this.UpdateDsp_Click);
            // 
            // UpdateApp
            // 
            this.UpdateApp.Name = "UpdateApp";
            this.UpdateApp.Size = new System.Drawing.Size(136, 22);
            this.UpdateApp.Text = "更新APP(&A)";
            this.UpdateApp.Click += new System.EventHandler(this.UpdateApp_Click);
            // 
            // EE3DefaultConfig
            // 
            this.EE3DefaultConfig.Name = "EE3DefaultConfig";
            this.EE3DefaultConfig.Size = new System.Drawing.Size(136, 22);
            this.EE3DefaultConfig.Text = "功能配置";
            this.EE3DefaultConfig.Click += new System.EventHandler(this.EE3DefaultConfig_Click);
            // 
            // pToolStripMenuItem1
            // 
            this.pToolStripMenuItem1.Name = "pToolStripMenuItem1";
            this.pToolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // CoilSet
            // 
            this.CoilSet.Name = "CoilSet";
            this.CoilSet.Size = new System.Drawing.Size(136, 22);
            this.CoilSet.Text = "线圈配置(&L)";
            this.CoilSet.Click += new System.EventHandler(this.CoilSet_Click);
            // 
            // rebootToolStripMenuItem
            // 
            this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            this.rebootToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.rebootToolStripMenuItem.Text = "重启设备(&R)";
            this.rebootToolStripMenuItem.Click += new System.EventHandler(this.RebootButtonClick);
            // 
            // BroadTime
            // 
            this.BroadTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BroadTime.Image = ((System.Drawing.Image)(resources.GetObject("BroadTime.Image")));
            this.BroadTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BroadTime.Name = "BroadTime";
            this.BroadTime.Size = new System.Drawing.Size(99, 22);
            this.BroadTime.Text = "广播系统时间(&T)";
            this.BroadTime.Click += new System.EventHandler(this.BroadTime_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutBtn});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripDropDownButton1.Text = "帮助(&H)";
            // 
            // AboutBtn
            // 
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(112, 22);
            this.AboutBtn.Text = "关于(&A)";
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // PicBox
            // 
            this.PicBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PicBox.Location = new System.Drawing.Point(186, 28);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(960, 576);
            this.PicBox.TabIndex = 2;
            this.PicBox.TabStop = false;
            this.PicBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Mouse_RightClk);
            this.PicBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Pic_DClick);
            this.PicBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pic_MouseDown);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsgShow,
            this.toolStripStatusLabel1,
            this.mainProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 612);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1159, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // MsgShow
            // 
            this.MsgShow.Name = "MsgShow";
            this.MsgShow.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.AutoSize = false;
            this.mainProgressBar.ForeColor = System.Drawing.Color.OliveDrab;
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(100, 16);
            this.mainProgressBar.Visible = false;
            // 
            // CNXMenu
            // 
            this.CNXMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnsureConfig,
            this.ResetConfig,
            this.CancelConfig});
            this.CNXMenu.Name = "CNXMenu";
            this.CNXMenu.Size = new System.Drawing.Size(119, 70);
            // 
            // EnsureConfig
            // 
            this.EnsureConfig.Enabled = false;
            this.EnsureConfig.Name = "EnsureConfig";
            this.EnsureConfig.Size = new System.Drawing.Size(118, 22);
            this.EnsureConfig.Text = "确认配置";
            this.EnsureConfig.Click += new System.EventHandler(this.EnsureConfig_Click);
            // 
            // ResetConfig
            // 
            this.ResetConfig.Enabled = false;
            this.ResetConfig.Name = "ResetConfig";
            this.ResetConfig.Size = new System.Drawing.Size(118, 22);
            this.ResetConfig.Text = "重新配置";
            this.ResetConfig.Click += new System.EventHandler(this.ResetConfig_Click);
            // 
            // CancelConfig
            // 
            this.CancelConfig.Enabled = false;
            this.CancelConfig.Name = "CancelConfig";
            this.CancelConfig.Size = new System.Drawing.Size(118, 22);
            this.CancelConfig.Text = "取消配置";
            this.CancelConfig.Click += new System.EventHandler(this.CancelConfig_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 634);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.mToolStrip);
            this.Controls.Add(this.CameraList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置工具客户端V1.2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mToolStrip.ResumeLayout(false);
            this.mToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.CNXMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		private System.Windows.Forms.ToolStripMenuItem rebootToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openExeToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton Device;
		private System.Windows.Forms.PictureBox PicBox;
		private System.Windows.Forms.ToolStripButton ConnectButton;
		private System.Windows.Forms.ToolStrip mToolStrip;
        private System.Windows.Forms.TreeView CameraList;
        private System.Windows.Forms.ToolStripMenuItem UpdateDsp;
        private System.Windows.Forms.ToolStripMenuItem UpdateApp;
        private System.Windows.Forms.ToolStripMenuItem CoilSet;
        private System.Windows.Forms.ToolStripMenuItem CurConfigSet;
        private System.Windows.Forms.ToolStripSeparator pToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton BroadTime;
        private System.Windows.Forms.ToolStripMenuItem DefaultConfig;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ContextMenuStrip CNXMenu;
        private System.Windows.Forms.ToolStripMenuItem EnsureConfig;
        private System.Windows.Forms.ToolStripMenuItem ResetConfig;
        private System.Windows.Forms.ToolStripStatusLabel MsgShow;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem AboutBtn;
        private System.Windows.Forms.ToolStripMenuItem CancelConfig;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripProgressBar mainProgressBar;
        private System.Windows.Forms.ToolStripMenuItem EE3DefaultConfig;
	}
}
