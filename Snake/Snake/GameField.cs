using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Snake.Properties;
using System.Resources;
using Snake.Resources;

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
       // private int X = 0; /// snake (head) X position.
       // private int Y = 0; /// snake (head) Y position.
        Snake snake = null;
        Point oldSnakeHead = new Point();
        Random random = new Random();
        private List<PictureBox> foodList;
        Dictionary<Point, int> foodByScore = new Dictionary<Point, int>();
        int Score = 0;
        bool isGameOver = false;
        public delegate void ScoreUpdateHandler(ScoreEventArgs e);
        public event ScoreUpdateHandler OnUpdateScore;
        public Direction requestedDirection = Direction.Right;
        ResourceManager resourceManager = new ResourceManager
               ("snake", typeof(GameField).Assembly);
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
            //foodList = new List<PictureBox>();

            snake = new Snake(rowsCount, columsCount, 0, 0);
            snake.growRight();
            snake.growRight();
            snake.growRight();
                      
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>

        public int score()
        {
            return Score;
        }

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
            snake.direction = requestedDirection;
            moveSnake();
        }

        void moveSnake()
        {
            oldSnakeHead = snake.headPosition();
            if (snake.isSelfCollision())
            {
                gameOver();
                Invalidate();
                return;
            }
            if(!eat())
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

        void gameOver()
        {
            isGameOver = true;
            timer.Stop();
            Invalidate();
            Score = 0;
            addScore(0);
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (isRunning() && snake.direction != Direction.Right)
                    {
                        requestedDirection = Direction.Left;
                        //snake.direction = Direction.Left;
                        //timer.Enabled = false;
                        //moveSnake();
                        //timer.Enabled = true;
                    }
                    break;

                case Keys.Right:
                    if (isRunning() && snake.direction != Direction.Left)
                    {
                        requestedDirection = Direction.Right;
                        //snake.direction = Direction.Right;
                        //timer.Enabled = false;
                        //moveSnake();
                        //timer.Enabled = true;
                    }
                    break;

                case Keys.Up:
                    if (isRunning() && snake.direction != Direction.Down)
                    {
                        requestedDirection = Direction.Up;
                        //snake.direction = Direction.Up;
                        //timer.Enabled = false;
                        //moveSnake();
                        //timer.Enabled = true;
                    }
                    break;

                case Keys.Down:
                    if (isRunning() && snake.direction != Direction.Up)
                    {
                        requestedDirection = Direction.Down;
                        //snake.direction = Direction.Down;
                        //timer.Enabled = false;
                        //moveSnake();
                        //timer.Enabled = true;
                    }
                    break;

               default:
                    return;
            }
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            timer.Start();
            createFood();

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
            paintFood(g);
            paintSnake(g);
            if (isGameOver)
            {
                paintGameOver(g);
            }
            else if (!isRunning())
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

        // Visual notification that game is over
        private void paintGameOver(Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;        // horizontal alignment
            sf.LineAlignment = StringAlignment.Center;    // vertical alignment
            Rectangle rect = new Rectangle(Width / 4, Height / 4, Width / 2, Height / 2);
            Color color = Color.FromArgb(100, Color.FromKnownColor(KnownColor.Red));
            SolidBrush lBrush = new SolidBrush(color);
            g.FillRectangle(lBrush, rect);
            Font font1 = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Point);
            g.DrawString("Game Over", font1, Brushes.Red, rect, sf);
        }

        private void paintSnake(Graphics g)
        {
            List<Point> snakeBody = snake.getBody();
            // body
            foreach (Point currentLink in snakeBody)
            {
                if (currentLink != snake.headPosition() && currentLink != snake.tailPosition())
                {
                    Point lefTop = leftTopRectPosition(currentLink);
                    Rectangle rect = new Rectangle(lefTop.X, lefTop.Y, squareWidth, squareWidth);
                    //LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Green, Color.Yellow, LinearGradientMode.BackwardDiagonal);
                    //g.FillRectangle(lBrush, rect);
                    Bitmap bodyImage = global::Snake.Resources.snake.body;
                    g.DrawImage(bodyImage, rect);
                }
            }

            //head
            Point lefTopHead = leftTopRectPosition(snake.headPosition());
            Rectangle rectHead = new Rectangle(lefTopHead.X, lefTopHead.Y, squareWidth, squareWidth);
            LinearGradientBrush lBrushHead = new LinearGradientBrush(rectHead, Color.Red, Color.Yellow, LinearGradientMode.BackwardDiagonal);
            g.FillRectangle(lBrushHead, rectHead);
           // Bitmap headImage = (Bitmap)resourceManager.GetObject("head");
            //g.DrawImage(headImage, lefTopHead);
            //Bitmap headImage = global::Snake.Properties.Resources.picture_1506;
            Bitmap headImage = global::Snake.Resources.snake.head;
            g.DrawImage(headImage, rectHead);


            //tail
            Point lefTopTail = leftTopRectPosition(snake.tailPosition());
            Rectangle rectTail = new Rectangle(lefTopTail.X, lefTopTail.Y, squareWidth, squareWidth);
            LinearGradientBrush lBrushTail = new LinearGradientBrush(rectTail, Color.Beige, Color.Yellow, LinearGradientMode.BackwardDiagonal);
            g.FillRectangle(lBrushTail, rectTail);
            Bitmap tailImage = global::Snake.Resources.snake.tail;
            g.DrawImage(tailImage, rectTail);
        }

        private void paintFood(Graphics g)
        {
            foreach (Point currentLink in foodByScore.Keys)
            {
                Point lefTop = leftTopRectPosition(currentLink);
                Rectangle rect = new Rectangle(lefTop.X, lefTop.Y, squareWidth, squareWidth);
                LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Pink, Color.BlueViolet, LinearGradientMode.BackwardDiagonal);
                g.FillRectangle(lBrush, rect);

            }
        }
        
        private Point leftTopRectPosition(Point point)
        {
            Point mappedPoint = new Point(point.X * squareWidth, point.Y * squareWidth);
            return mappedPoint;
        }

        public void StartGame()
        {
            isGameOver = false;
            snake = new Snake(rowsCount, columsCount, 0, 0);
            requestedDirection = snake.direction;
            snake.growRight();
            snake.growRight();
            snake.growRight();
            timer.Start();
            Invalidate();
        }
         
        public void createFood()
        {
            bool accept = false;
            int foodX = 0;
            int foodY = 0;
            while (!accept)
            {
                foodX = random.Next(0, columsCount);
                foodY = random.Next(0, rowsCount);
                accept = true;
                List<Point> snakeBody = snake.getBody();
                foreach (Point currentLink in snakeBody)
                {
                    if (foodX == currentLink.X && foodY == currentLink.Y)
                    {
                        accept = false;
                    }
                }
            }
            //PictureBox food = new System.Windows.Forms.PictureBox();
            Point foodPos = new Point(foodX, foodY);
            //foodList.Add(food);
            foodByScore.Add(foodPos, 4);
            //food.Location = leftTopRectPosition(foodPos);
            //food.Width = squareWidth;
            //food.Height = squareWidth;
            //Controls.Add(food);
        }

        public bool eat()
        {
            //Point lItemToRemove = null;
            foreach (Point currentLink in foodByScore.Keys)
            {
                if (snake.headPosition() == currentLink)
                {
                    //lItemToRemove = currentLink;
                    //Controls.Remove(lItemToRemove);
                    //foodList.Remove(lItemToRemove);
                    addScore(foodByScore[currentLink]);
                    foodByScore.Remove(currentLink);
                    snake.grow();
                    createFood();                    
                    Invalidate();
                    return true;                    
                }
            }
            //if (lItemToRemove != null)
            //{
            //    Controls.Remove(lItemToRemove);
            //    foodList.Remove(lItemToRemove);
            //    Invalidate();
            //    snake.grow();
            //    createFood();
            //    addScore(5);
                
            //    return true;
            //}
            return false;
        }

        private void addScore(int pScore)
        {
            Score += pScore;
            ScoreEventArgs args = new ScoreEventArgs(Score);
            OnUpdateScore(args);
        }
    }
        public class ScoreEventArgs : EventArgs
        {
            public int Score { get; private set; }

            public ScoreEventArgs(int score)
            {
                if (score > 0)
                    Score = score;
            }
        }


      
    
}
