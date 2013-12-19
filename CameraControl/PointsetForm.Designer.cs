namespace CameraControl
{
    partial class PointsetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Speed60 = new System.Windows.Forms.RadioButton();
            this.Speed70 = new System.Windows.Forms.RadioButton();
            this.SpeedSelfD = new System.Windows.Forms.RadioButton();
            this.SpeedBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置最高车速:";
            // 
            // Speed60
            // 
            this.Speed60.AutoSize = true;
            this.Speed60.Location = new System.Drawing.Point(25, 48);
            this.Speed60.Name = "Speed60";
            this.Speed60.Size = new System.Drawing.Size(89, 16);
            this.Speed60.TabIndex = 1;
            this.Speed60.TabStop = true;
            this.Speed60.Text = "60公里/小时";
            this.Speed60.UseVisualStyleBackColor = true;
            this.Speed60.CheckedChanged += new System.EventHandler(this.Speed60_CheckedChanged);
            // 
            // Speed70
            // 
            this.Speed70.AutoSize = true;
            this.Speed70.Location = new System.Drawing.Point(135, 48);
            this.Speed70.Name = "Speed70";
            this.Speed70.Size = new System.Drawing.Size(89, 16);
            this.Speed70.TabIndex = 2;
            this.Speed70.TabStop = true;
            this.Speed70.Text = "70公里/小时";
            this.Speed70.UseVisualStyleBackColor = true;
            this.Speed70.CheckedChanged += new System.EventHandler(this.Speed70_CheckedChanged);
            // 
            // SpeedSelfD
            // 
            this.SpeedSelfD.AutoSize = true;
            this.SpeedSelfD.Location = new System.Drawing.Point(25, 79);
            this.SpeedSelfD.Name = "SpeedSelfD";
            this.SpeedSelfD.Size = new System.Drawing.Size(59, 16);
            this.SpeedSelfD.TabIndex = 3;
            this.SpeedSelfD.TabStop = true;
            this.SpeedSelfD.Text = "自定义";
            this.SpeedSelfD.UseVisualStyleBackColor = true;
            this.SpeedSelfD.CheckedChanged += new System.EventHandler(this.SpeedSelfD_CheckedChanged);
            // 
            // SpeedBox
            // 
            this.SpeedBox.Location = new System.Drawing.Point(25, 116);
            this.SpeedBox.Name = "SpeedBox";
            this.SpeedBox.Size = new System.Drawing.Size(39, 21);
            this.SpeedBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "（仅供专业人员使用）";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "忽略";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PointsetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 183);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SpeedBox);
            this.Controls.Add(this.SpeedSelfD);
            this.Controls.Add(this.Speed70);
            this.Controls.Add(this.Speed60);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "PointsetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "虚拟线圈配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton Speed60;
        private System.Windows.Forms.RadioButton Speed70;
        private System.Windows.Forms.RadioButton SpeedSelfD;
        private System.Windows.Forms.TextBox SpeedBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}