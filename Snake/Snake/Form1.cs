using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
    public partial class Form1 : Form
    {
        //TODO: remove snake painting to another class

        /// <summary>
        /// Game field.
        /// </summary>
        GameField gameField = null;

        /// <summary>
        /// Settings control.
        /// </summary>
        Settings settings = null;
        
        int sideWidth = 100;

        int X = 100;
        int Y = 100;

        Direction direction = Direction.Right;

        int step = 2;

        public Form1()
        {
            InitializeComponent();

            gameField = new GameField(50, 50, 20);
            gameField.Width = (this.ClientRectangle.Width * 4) / 5;
            gameField.Height = this.ClientRectangle.Height;
            gameField.Location = new Point(0, 0);
            this.Controls.Add(gameField);

            settings = new Settings();
            this.Controls.Add(settings);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(X, Y, sideWidth, sideWidth);
            LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Red, Color.Yellow, LinearGradientMode.BackwardDiagonal);
            g.FillRectangle(lBrush, rect);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    direction = Direction.Left;
                    break;

                case Keys.Right:
                    direction = Direction.Right;
                    break;

                case Keys.Up:
                    direction = Direction.Up;
                    break;

                case Keys.Down:
                    direction = Direction.Down;
                    break;

                case Keys.Space:
                    if (timer1.Enabled)
                    {
                        pause();
                    }
                    else
                    {
                        resume();
                    }
                    break;

                default:
                    return;
            }
        }

        private void moveLeft(int x)
        {
            int newX = X - x;
            if (newX >= 0)
            {
                X = newX;
                Invalidate();
            }
        }

        private void pause()
        {
            timer1.Stop();
        }

        private void resume()
        {
            timer1.Start();
        }

        private void moveUp(int x)
        {
            Y = Y - step;
            Invalidate();
        }

        private void moveRight(int x)
        {
            X = X + step;
            Invalidate();
        }

        private void moveDown(int x)
        {
            Y = Y + step;
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (direction) 
            {
                case Direction.Left:
                    moveLeft(step);
                    break;
                case Direction.Up:
                    moveUp(step);
                    break;
                case Direction.Right:
                    moveRight(step);
                    break;
                case Direction.Down:
                    moveDown(step);
                    break;
            }
        }
    }
}
