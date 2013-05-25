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
            this.SuspendLayout();
            // 
            // Low
            // 
            this.Low.Location = new System.Drawing.Point(37, 62);
            this.Low.Name = "Low";
            this.Low.Size = new System.Drawing.Size(59, 23);
            this.Low.TabIndex = 0;
            this.Low.Text = "Low\r\n";
            this.Low.UseVisualStyleBackColor = true;
            this.Low.Click += new System.EventHandler(this.Low_Click);
            // 
            // Medium
            // 
            this.Medium.Location = new System.Drawing.Point(105, 62);
            this.Medium.Name = "Medium";
            this.Medium.Size = new System.Drawing.Size(61, 23);
            this.Medium.TabIndex = 1;
            this.Medium.Text = "Medium";
            this.Medium.UseVisualStyleBackColor = true;
            this.Medium.Click += new System.EventHandler(this.Medium_Click);
            // 
            // High
            // 
            this.High.Location = new System.Drawing.Point(172, 62);
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
            this.label1.Location = new System.Drawing.Point(99, 25);
            this.label1.MaximumSize = new System.Drawing.Size(100, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game speed";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 119);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.High);
            this.Controls.Add(this.Medium);
            this.Controls.Add(this.Low);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Low;
        private System.Windows.Forms.Button Medium;
        private System.Windows.Forms.Button High;
        private System.Windows.Forms.Label label1;
    }
}