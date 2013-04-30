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
    public partial class Form1 : Form
    {
        private Grid grid = null;

        public Form1()
        {
            InitializeComponent();
            grid = new Grid(500,500);
            Controls.Add(grid);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
