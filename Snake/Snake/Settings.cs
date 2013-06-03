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
        int speedSetting = 400;

        public delegate void SpeedUpdateHandler(SpeedEventArgs e);
        public event SpeedUpdateHandler OnUpdateSpeed;

        public Settings()
        {
            InitializeComponent();
            loadGeneralSettings();
        }

        void loadGeneralSettings()
        {
            playSound = GeneralSettings.Default.Sound;
            soundVolume = GeneralSettings.Default.Volume;
            Sound.Checked = playSound;
            Sound.Text = "Sound on";
            trackBar1.Value = soundVolume;
        }

        private void saveSettings()
        {
            GeneralSettings.Default.Sound = playSound;
            GeneralSettings.Default.Volume = soundVolume;

            GeneralSettings.Default.Save();
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
            loadGeneralSettings();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            //if( speedSetting == 400)
            //{
            //SpeedEventArgs args = new SpeedEventArgs(400);
            //OnUpdateSpeed(args);
            //    Hide();
            //}
            //else if (speedSetting == 200)
            //{
            //SpeedEventArgs args = new SpeedEventArgs(200);
            //OnUpdateSpeed(args);
            //    Hide();
            //}
            //else if (speedSetting == 900)
            //{
            //SpeedEventArgs args = new SpeedEventArgs(900);
            //OnUpdateSpeed(args);
            //Hide();
            //}
            SpeedEventArgs args = new SpeedEventArgs(speedSetting);
            OnUpdateSpeed(args);
            Hide();
            saveSettings();
        }

        private void Sound_CheckedChanged(object sender, EventArgs e)
        {
            playSound = Sound.Checked;
            if (Sound.Checked == true)
            {
                if (trackBar1.Value == 0)
                {
                    trackBar1.Value = 5;
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            soundVolume = trackBar1.Value;
            if (trackBar1.Value == 0)
            {
                Sound.Checked = false;
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
