using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    // TODO: move this to another file
    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
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
            gameField = new GameField(10, 20, 20);
            // TODO: set correct geometry
            //gameField.Width = this.ClientRectangle.Width;
            //gameField.Height = this.ClientRectangle.Height;
            gameField.Width = 20*20;
            gameField.Height = 10 * 20;
            
            
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
       
    }
}
