using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Game field.
        /// </summary>
        GameField gameField = null;

        /// <summary>
        /// Settings control.
        /// </summary>
        Settings settings = null;

        public Form1()
        {
            InitializeComponent();
            gameField = new GameField(20, 20, 20);
            // TODO: set correct geometry
            //gameField.Width = this.ClientRectangle.Width;
            //gameField.Height = this.ClientRectangle.Height;
            gameField.Width = 20*20;
            gameField.Height = 20 * 20;
            gameField.Location = new Point(500, 500);
            
            //this.DoubleBuffered = true;
           
            
            gameField.Location = new Point(0, 0);
            this.Controls.Add(gameField);

            settings = new Settings();
            this.Controls.Add(settings);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }


        // exit

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameField.pause();

           DialogResult result = MessageBox.Show("Are you sure you want to exit?","Exit", MessageBoxButtons.YesNo);

           if (result == DialogResult.Yes)
           {

               DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
               if (resultsave == DialogResult.Yes)
               {
                   // save game
               }
               else Close();
           }
           else gameField.resume();
            
                       
        }

    
       
    }
}
