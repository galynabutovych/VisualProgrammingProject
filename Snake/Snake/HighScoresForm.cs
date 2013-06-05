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
    public partial class HighScoresForm : Form
    {
        public HighScoresForm()
        {
            InitializeComponent();
            textBoxLast.ReadOnly = true;
            textBoxHigh.ReadOnly = true;
        }

        public void setLastScore(int score)
        {
            textBoxLast.Text = score.ToString();
        }

        public void setHighScore(int score)
        {
            textBoxHigh.Text = score.ToString();
        }
    }
}
