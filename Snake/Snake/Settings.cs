using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class Settings : Form
    {
        //General settings:
        bool playSound = true;
        int soundVolume = 100;

        // Per game settings:
        int speedSetting = 0;

        public delegate void SpeedUpdateHandler(SpeedEventArgs e);
        public event SpeedUpdateHandler OnUpdateSpeed;

        public Settings()
        {
            InitializeComponent();
        }

        void loadGeneralSettings()
        {
            playSound = GeneralSettings.Default.Sound;
            soundVolume = GeneralSettings.Default.Volume;
        }

        private void saveSettings()
        {
            // TODO: call this function in the OK button press event handler

            // save general settings:
            GeneralSettings.Default.Sound = playSound;
            GeneralSettings.Default.Volume = soundVolume;

            // save game settings:
        }

        private void Low_Click(object sender, EventArgs e)
        {
            speedSetting = 900;
            //SpeedEventArgs args = new SpeedEventArgs(900);
            //OnUpdateSpeed(args);
            
         }

        private void High_Click(object sender, EventArgs e)
        {
            speedSetting = 200;
            //SpeedEventArgs args = new SpeedEventArgs(200);
            //OnUpdateSpeed(args);
           
        }

        private void Medium_Click(object sender, EventArgs e)
        {
            speedSetting = 400;
            // TODO: emit this event only after user presses OK button
            //SpeedEventArgs args = new SpeedEventArgs(400);
            //OnUpdateSpeed(args);
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if( speedSetting == 400)
            {
            SpeedEventArgs args = new SpeedEventArgs(400);
            OnUpdateSpeed(args);
                Hide();
            }
            else if (speedSetting == 200)
            {
            SpeedEventArgs args = new SpeedEventArgs(200);
            OnUpdateSpeed(args);
                Hide();
            }
            else if (speedSetting == 900)
            {
            SpeedEventArgs args = new SpeedEventArgs(900);
            OnUpdateSpeed(args);
            Hide();
            }
                    
            Hide();
        }

        private void Sound_CheckedChanged(object sender, EventArgs e)
        {
            Sound.Text = "Sound on";
            
            if (Sound.Checked == true)
            {
                Sound.Text = "Sound off";
                playSound = false;
                trackBar1.Value = 0;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            if (trackBar1.Value == 1)
            {
                soundVolume = 50;
                Sound.Checked = false;
            }

            else if (trackBar1.Value == 0)
            {
                Sound.Checked = true;

            }


        }


    }
        
public class SpeedEventArgs : EventArgs
{
    public int Speed { get; private set; }

    public SpeedEventArgs(int speed)
    {
        if(speed > 0)
        Speed = speed;
    }
}

}
