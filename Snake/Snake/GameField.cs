using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Class for visualizing game field.
    /// </summary>
    public partial class GameField : UserControl
    {
        //TODO: resize
        
        /// Members:
        private int intervalWidth = 20; /// elementary rectangular cell side width (step width too)
        private int rowsCount; /// rows count
        private int columsCount; /// colums count
        private int X = 0; /// snake (head) X position.
        private int Y = 0; /// snake (head) Y position.
        Direction direction = Direction.Right; /// in what direction snake is moving.
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

            rowsCount = rows;
            columsCount = colums;
            intervalWidth = cellWidth;

            snake = new Snake();
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void pause()
        {
            timer.Stop();
        }

        /// <summary>
        /// Resumes paused game.
        /// </summary>
        public void resume()
        {
            timer.Start();
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
            switch (direction)
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
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    snake.direction = Direction.Left;
                    break;

                case Keys.Right:
                    snake.direction = Direction.Right;
                    break;

                case Keys.Up:
                    snake.direction = Direction.Up;
                    break;

                case Keys.Down:
                    snake.direction = Direction.Down;
                    break;

                case Keys.Space:
                    if (timer.Enabled)
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

        private void GameField_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
