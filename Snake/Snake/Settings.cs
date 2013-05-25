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
        bool playSound = false;
        int soundVolume = 0;

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
            SpeedEventArgs args = new SpeedEventArgs(900);
            OnUpdateSpeed(args);
            Hide();
        }

        private void High_Click(object sender, EventArgs e)
        {
            speedSetting = 200;
            SpeedEventArgs args = new SpeedEventArgs(200);
            OnUpdateSpeed(args);
            Hide();
        }

        private void Medium_Click(object sender, EventArgs e)
        {
            speedSetting = 400;
            // TODO: emit this event only after user presses OK button
            SpeedEventArgs args = new SpeedEventArgs(400);
            OnUpdateSpeed(args);
            Hide();
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
