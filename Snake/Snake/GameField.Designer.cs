﻿namespace Snake
{
    partial class GameField
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerOfBonus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerOfBonus
            // 
            this.timerOfBonus.Interval = 3000;
            this.timerOfBonus.Tick += new System.EventHandler(this.timerOfBonus_Tick_1);
            // 
            // GameField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.BackgroundImage = global::Snake.Properties.Resources._9317588_grass_background_with_flowers_bees_and_butterfly1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Location = new System.Drawing.Point(0, 24);
            this.Name = "GameField";
            this.Size = new System.Drawing.Size(345, 266);
            this.Load += new System.EventHandler(this.GameField_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameField_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameField_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerOfBonus;
    }
}
