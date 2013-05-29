namespace Snake
{
    partial class Settings
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
            this.Low = new System.Windows.Forms.Button();
            this.Medium = new System.Windows.Forms.Button();
            this.High = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.Volume = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.Sound = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Low
            // 
            this.Low.Location = new System.Drawing.Point(40, 45);
            this.Low.Name = "Low";
            this.Low.Size = new System.Drawing.Size(59, 23);
            this.Low.TabIndex = 0;
            this.Low.Text = "Low\r\n";
            this.Low.UseVisualStyleBackColor = true;
            this.Low.Click += new System.EventHandler(this.Low_Click);
            // 
            // Medium
            // 
            this.Medium.Location = new System.Drawing.Point(105, 45);
            this.Medium.Name = "Medium";
            this.Medium.Size = new System.Drawing.Size(61, 23);
            this.Medium.TabIndex = 1;
            this.Medium.Text = "Medium";
            this.Medium.UseVisualStyleBackColor = true;
            this.Medium.Click += new System.EventHandler(this.Medium_Click);
            // 
            // High
            // 
            this.High.Location = new System.Drawing.Point(172, 45);
            this.High.Name = "High";
            this.High.Size = new System.Drawing.Size(55, 23);
            this.High.TabIndex = 2;
            this.High.Text = "High";
            this.High.UseVisualStyleBackColor = true;
            this.High.Click += new System.EventHandler(this.High_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 9);
            this.label1.MaximumSize = new System.Drawing.Size(100, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game speed";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(214, 182);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(55, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(144, 182);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(55, 23);
            this.Ok.TabIndex = 5;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Volume
            // 
            this.Volume.AutoSize = true;
            this.Volume.Location = new System.Drawing.Point(113, 103);
            this.Volume.MaximumSize = new System.Drawing.Size(100, 20);
            this.Volume.Name = "Volume";
            this.Volume.Size = new System.Drawing.Size(42, 13);
            this.Volume.TabIndex = 6;
            this.Volume.Text = "Volume";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(145, 131);
            this.trackBar1.Maximum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(124, 45);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Sound
            // 
            this.Sound.AutoSize = true;
            this.Sound.Location = new System.Drawing.Point(42, 131);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(72, 17);
            this.Sound.TabIndex = 8;
            this.Sound.Text = "Sound off";
            this.Sound.UseVisualStyleBackColor = true;
            this.Sound.CheckedChanged += new System.EventHandler(this.Sound_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 219);
            this.ControlBox = false;
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.Volume);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.High);
            this.Controls.Add(this.Medium);
            this.Controls.Add(this.Low);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Low;
        private System.Windows.Forms.Button Medium;
        private System.Windows.Forms.Button High;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label Volume;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox Sound;
    }
}