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
        public delegate void SpeedUpdateHandler(SpeedEventArgs e);
        public event SpeedUpdateHandler OnUpdateSpeed;

        public Settings()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Low_Click(object sender, EventArgs e)
        {
            SpeedEventArgs args = new SpeedEventArgs(900);
            OnUpdateSpeed(args);
            Hide();
        }

        private void High_Click(object sender, EventArgs e)
        {
            SpeedEventArgs args = new SpeedEventArgs(200);
            OnUpdateSpeed(args);
            Hide();
        }

        private void Medium_Click(object sender, EventArgs e)
        {
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
