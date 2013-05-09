using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    /// <summary>
    /// Class for visualizing game field.
    /// </summary>
    public partial class GameField : UserControl
    {
        //TODO: resize
        
        /// Members:
        private int squareWidth = 20; /// elementary rectangular cell side width (step width too)
        private int rowsCount; /// rows count
        private int columsCount; /// colums count
        private int X = 0; /// snake (head) X position.
        private int Y = 0; /// snake (head) Y position.
        Snake snake = null;

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="rows">Rows count.</param>
        /// <param name="colums">Colums count.</param>
        /// <param name="cellWidth">Elementary rectangular cell side width.</param>
        public GameField(int rows, int colums, int cellWidth)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            rowsCount = rows;
            columsCount = colums;
            squareWidth = cellWidth;            

            snake = new Snake(rowsCount, columsCount, 0, 0);
            snake.growRight();
            snake.growRight();
            snake.growRight();
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void pause()
        {
            
            timer.Stop();
            Invalidate();
        }

        /// <summary>
        /// Resumes paused game.
        /// </summary>
        public void resume()
        {
            timer.Start();
            Invalidate();
        }

        public bool isRunning()
        {
            return timer.Enabled;
        }

        /// <summary>
        /// Sets time interval for 1 movement of snake (speed).
        /// </summary>
        /// <param name="msec">Miliseconds betweeen 2 updates.</param>
        public void setTimerInterval(int msec)
        {
            timer.Interval = msec;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (snake.direction)
            {
                case Direction.Left:
                    snake.moveLeft();
                    break;
                case Direction.Up:
                    snake.moveUp();
                    break;
                case Direction.Right:
                    snake.moveRight();
                    break;
                case Direction.Down:
                    snake.moveDown();
                    break;
            }
            Invalidate();
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (isRunning() && snake.direction != Direction.Right)
                    {
                        snake.direction = Direction.Left;
                    }
                    break;

                case Keys.Right:
                    if (isRunning() && snake.direction != Direction.Left)
                    {
                        snake.direction = Direction.Right;
                    }
                    break;

                case Keys.Up:
                    if (isRunning() && snake.direction != Direction.Down)
                    {
                        snake.direction = Direction.Up;
                    }
                    break;

                case Keys.Down:
                    if (isRunning() && snake.direction != Direction.Up)
                    {
                        snake.direction = Direction.Down;
                    }
                    break;

                case Keys.Space:
                    if (isRunning())
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

        private void GameField_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
        protected override bool IsInputKey(Keys keyData)
        {
            //return true if the specified key is a regular input key; otherwise return false. 
            if (keyData == Keys.Left ||
                keyData == Keys.Up ||
                keyData == Keys.Right ||
                keyData == Keys.Down ||
                keyData == Keys.Space
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            paintSnake(g);
            if (!isRunning())
            {
                paintPause(g);
            }
        }


        // Visual notification that game is paused

        private void paintPause(Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;        // horizontal alignment
            sf.LineAlignment = StringAlignment.Center;    // vertical alignment
            Rectangle rect = new Rectangle(Width / 4, Height / 4, Width / 2, Height / 2);
            Color color = Color.FromArgb(100, Color.FromKnownColor(KnownColor.Yellow));
            SolidBrush lBrush = new SolidBrush(color);
            g.FillRectangle(lBrush, rect);
            Font font1 = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Point);
            g.DrawString("Paused", font1, Brushes.Blue, rect, sf);
        }

        private void paintSnake(Graphics g)
        {
            List<Point> snakeBody = snake.getBody();
            foreach (Point currentLink in snakeBody)
            {
                Point lefyTop = leftTopRectPosition(currentLink);
                Rectangle rect = new Rectangle(lefyTop.X, lefyTop.Y, squareWidth, squareWidth);
                LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Red, Color.Yellow, LinearGradientMode.BackwardDiagonal);
                g.FillRectangle(lBrush, rect);
            }
        }

        private Point leftTopRectPosition(Point point)
        {
            Point mappedPoint = new Point(point.X * squareWidth, point.Y * squareWidth);
            return mappedPoint;
        }
    }
}
