/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/6/21
 * Time: 15:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CameraControl
{
	partial class InputForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_PX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.TB_PY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.TPS = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.TPE = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_ZQ = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TB_NT = new System.Windows.Forms.TextBox();
            this.CB_ALLDAY = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 248);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "重置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "摄像机IP";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "管理平台IP";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(30, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "摄像头分辨率";
            // 
            // TB_PX
            // 
            this.TB_PX.Enabled = false;
            this.TB_PX.Location = new System.Drawing.Point(119, 91);
            this.TB_PX.Name = "TB_PX";
            this.TB_PX.Size = new System.Drawing.Size(41, 21);
            this.TB_PX.TabIndex = 6;
            this.TB_PX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_Press);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(258, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "端口";
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(299, 54);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(40, 21);
            this.TB_Port.TabIndex = 8;
            this.TB_Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_Press);
            // 
            // TB_PY
            // 
            this.TB_PY.Enabled = false;
            this.TB_PY.Location = new System.Drawing.Point(194, 91);
            this.TB_PY.Name = "TB_PY";
            this.TB_PY.Size = new System.Drawing.Size(41, 21);
            this.TB_PY.TabIndex = 10;
            this.TB_PY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_Press);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(172, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "X";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // TPS
            // 
            this.TPS.CustomFormat = "HH:mm";
            this.TPS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TPS.Location = new System.Drawing.Point(119, 130);
            this.TPS.Name = "TPS";
            this.TPS.ShowUpDown = true;
            this.TPS.Size = new System.Drawing.Size(58, 21);
            this.TPS.TabIndex = 13;
            this.TPS.Value = new System.DateTime(2013, 1, 3, 5, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "工作时段";
            // 
            // TPE
            // 
            this.TPE.CustomFormat = "HH:mm";
            this.TPE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TPE.Location = new System.Drawing.Point(213, 128);
            this.TPE.Name = "TPE";
            this.TPE.ShowUpDown = true;
            this.TPE.Size = new System.Drawing.Size(55, 21);
            this.TPE.TabIndex = 15;
            this.TPE.Value = new System.DateTime(2013, 1, 3, 18, 30, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "——";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "上传周期";
            // 
            // TB_ZQ
            // 
            this.TB_ZQ.Location = new System.Drawing.Point(119, 200);
            this.TB_ZQ.Name = "TB_ZQ";
            this.TB_ZQ.Size = new System.Drawing.Size(100, 21);
            this.TB_ZQ.TabIndex = 18;
            this.TB_ZQ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_Press);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "夜晚车流";
            // 
            // TB_NT
            // 
            this.TB_NT.Location = new System.Drawing.Point(120, 165);
            this.TB_NT.Name = "TB_NT";
            this.TB_NT.Size = new System.Drawing.Size(100, 21);
            this.TB_NT.TabIndex = 20;
            this.TB_NT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_Press);
            // 
            // CB_ALLDAY
            // 
            this.CB_ALLDAY.AutoSize = true;
            this.CB_ALLDAY.Location = new System.Drawing.Point(274, 131);
            this.CB_ALLDAY.Name = "CB_ALLDAY";
            this.CB_ALLDAY.Size = new System.Drawing.Size(72, 16);
            this.CB_ALLDAY.TabIndex = 21;
            this.CB_ALLDAY.Text = "全天运行";
            this.CB_ALLDAY.UseVisualStyleBackColor = true;
            this.CB_ALLDAY.CheckStateChanged += new System.EventHandler(this.CB_ALLDAY_CheckStateChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(226, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "（辆/分钟）";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(226, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "（秒）";
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 328);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.CB_ALLDAY);
            this.Controls.Add(this.TB_NT);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TB_ZQ);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TPE);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TPS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_PY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_Port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_PX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "InputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "当前配置";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TB_PX;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_Port;
        private IPTextBox ITB_Cam;
        private IPTextBox ITB_Srv;
        private System.Windows.Forms.TextBox TB_PY;
        private System.Windows.Forms.Label label5;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.DateTimePicker TPS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker TPE;
        private System.Windows.Forms.TextBox TB_ZQ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TB_NT;
        private System.Windows.Forms.CheckBox CB_ALLDAY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
	}
}
