/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/7/9
 * Time: 11:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CameraControl
{
	partial class ExpFrom
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
			this.Save = new System.Windows.Forms.Button();
			this.cbPan = new System.Windows.Forms.Panel();
			this.Lbx = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Save
			// 
			this.Save.Location = new System.Drawing.Point(176, 206);
			this.Save.Name = "Save";
			this.Save.Size = new System.Drawing.Size(75, 23);
			this.Save.TabIndex = 0;
			this.Save.Text = "保存";
			this.Save.UseVisualStyleBackColor = true;
			this.Save.Click += new System.EventHandler(this.SaveClick);
			// 
			// cbPan
			// 
			this.cbPan.Location = new System.Drawing.Point(72, 41);
			this.cbPan.Name = "cbPan";
			this.cbPan.Size = new System.Drawing.Size(300, 150);
			this.cbPan.TabIndex = 1;
			// 
			// Lbx
			// 
			this.Lbx.Location = new System.Drawing.Point(62, 24);
			this.Lbx.Name = "Lbx";
			this.Lbx.Size = new System.Drawing.Size(296, 14);
			this.Lbx.TabIndex = 2;
			this.Lbx.Text = "  很少      少       中等       多      很多";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "很少";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(28, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "少";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(28, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 18);
			this.label3.TabIndex = 5;
			this.label3.Text = "中等";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 133);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 18);
			this.label4.TabIndex = 4;
			this.label4.Text = "多";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 162);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 18);
			this.label5.TabIndex = 6;
			this.label5.Text = "很多";
			// 
			// ExpFrom
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430, 246);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Lbx);
			this.Controls.Add(this.cbPan);
			this.Controls.Add(this.Save);
			this.Name = "ExpFrom";
			this.Text = "ExpFrom";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label Lbx;
		private System.Windows.Forms.Panel cbPan;
		private System.Windows.Forms.Button Save;
	}
}
