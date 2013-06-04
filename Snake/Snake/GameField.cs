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
using System.Media;

namespace Snake
{
    /// <summary>
    /// Class for visualizing game field.
    /// </summary>
    public partial class GameField : UserControl
    {
        //TODO: resize

        #region Members & Constructors
        private int squareWidth = 20; /// elementary rectangular cell side width (step width too)
        private int rowsCount;
        private int columsCount;
        Snake snake = null;
        Random random = new Random();
        Dictionary<Point, int> foodByScore = new Dictionary<Point, int>();
        Dictionary<Point, int> bonusByType = new Dictionary<Point, int>();
        List<Point> barriers = new List<Point>();
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

            snake = new Snake(rowsCount, columsCount, 0, 0);
            // REMOVEME:
            snake.growRight();
            snake.growRight();
            snake.growRight();
            ResizeRedraw = false;
        }
        #endregion

        #region Event Handlers
        private void timer_Tick(object sender, EventArgs e)
        {
            snake.direction = requestedDirection;
            moveSnake();
        }
        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (isRunning() && snake.direction != Direction.Right)
                    {
                        requestedDirection = Direction.Left;
                    }
                    break;

                case Keys.Right:
                    if (isRunning() && snake.direction != Direction.Left)
                    {
                        requestedDirection = Direction.Right;
                    }
                    break;

                case Keys.Up:
                    if (isRunning() && snake.direction != Direction.Down)
                    {
                        requestedDirection = Direction.Up;
                    }
                    break;

                case Keys.Down:
                    if (isRunning() && snake.direction != Direction.Up)
                    {
                        requestedDirection = Direction.Down;
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
            createBarriers();

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
        #endregion

        #region Painting & Sound


        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            paintFood(g);
            paintBonus(g);
            paintBarriers(g);
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
                    Bitmap bodyBody = global::Snake.Resources.snake.body;
                    g.DrawImage(bodyBody, rect);
                }
            }

            //head
            Point lefTopHead = leftTopRectPosition(snake.headPosition());
            Rectangle rectHead = new Rectangle(lefTopHead.X, lefTopHead.Y, squareWidth, squareWidth);
            Bitmap headImage = global::Snake.Resources.snake.head;
            g.DrawImage(headImage, rectHead);
            switch (snake.direction)
            {
                case Direction.Left:
                    headImage = global::Snake.Resources.snake.left;
                    g.DrawImage(headImage, rectHead);
                    break;

                case Direction.Up:
                    headImage = global::Snake.Resources.snake.up;
                    g.DrawImage(headImage, rectHead);
                    break;

                case Direction.Right:
                    headImage = global::Snake.Resources.snake.head;
                    g.DrawImage(headImage, rectHead);
                    break;

                case Direction.Down:
                    headImage = global::Snake.Resources.snake.down;
                    g.DrawImage(headImage, rectHead);
                    break;
            }



            //tail
            Point lefTopTail = leftTopRectPosition(snake.tailPosition());
            Rectangle rectTail = new Rectangle(lefTopTail.X, lefTopTail.Y, squareWidth, squareWidth);
            Bitmap tailImage = global::Snake.Resources.snake.tail;
            g.DrawImage(tailImage, rectTail);
        }

        private void paintFood(Graphics g)
        {
            foreach (Point currentLink in foodByScore.Keys)
            {
                Point lefTop = leftTopRectPosition(currentLink);
                Rectangle rect = new Rectangle(lefTop.X, lefTop.Y, squareWidth, squareWidth);
                Bitmap cookieImage = global::Snake.Properties.Resources.cookie;
                g.DrawImage(cookieImage, rect);
            }
        }

        private void paintBonus(Graphics g)
        {
            foreach (Point currentLink in bonusByType.Keys)
            {
                Point lefTop = leftTopRectPosition(currentLink);
                Rectangle rect = new Rectangle(lefTop.X, lefTop.Y, squareWidth, squareWidth);
                Bitmap foodImage = global::Snake.Properties.Resources.cookie;
                switch (bonusByType[currentLink])
                {
                    case 1:
                        //lBrush = new LinearGradientBrush(rect, Color.Red, Color.Red, LinearGradientMode.BackwardDiagonal);
                        foodImage = global::Snake.Properties.Resources.podarok1;
                        g.DrawImage(foodImage, rect);
                        break;
                    case 2:

                        foodImage = global::Snake.Properties.Resources.podarok1;
                        g.DrawImage(foodImage, rect);
                        break;
                    case 3:

                        foodImage = global::Snake.Resources.Resource1.skull;
                        g.DrawImage(foodImage, rect);
                        break;
                    case 4:

                        foodImage = global::Snake.Properties.Resources.m611;
                        g.DrawImage(foodImage, rect);
                        break;
                }


                //    Bitmap fastfoodImage = global::Snake.Resources.snake.fastfood;
                //    g.DrawImage(fastfoodImage, rectfastfood);
            }

        }

        void paintBarriers(Graphics g)
        {
            foreach (Point currentLink in barriers)
            {
                Point lefTop = leftTopRectPosition(currentLink);
                Rectangle rect = new Rectangle(lefTop.X, lefTop.Y, squareWidth, squareWidth);
                LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Black, Color.Red, LinearGradientMode.BackwardDiagonal);
                g.FillRectangle(lBrush, rect);
            }
        }


        private Point leftTopRectPosition(Point point)
        {
            Point mappedPoint = new Point(point.X * squareWidth, point.Y * squareWidth);
            return mappedPoint;
        }

        //private void Form1_Click(object sender, EventArgs e)
        //{
        //    Form1 f = new Form1();
        //    f.Show();

        //    MediaPlayer mp = new MediaPlayer();
        //    mp.Open(Path.Combine(Application.StartupPath, "Лето 2010.mp3"));
        //    mp.Play(true);
        //}
        #endregion

        #region Methods
        public void StartGame(GameSettings pGameSettings)
        {
            isGameOver = false;
            loadSettings(pGameSettings);
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
                foreach (Point currentLink in barriers)
                {
                    if (foodX == currentLink.X && foodY == currentLink.Y)
                    {
                        accept = false;
                    }
                }
            }
            Point foodPos = new Point(foodX, foodY);
            foodByScore.Add(foodPos, 4);

        }

        public void createBonus()
        {

            if (bonusByType.Count != 1)
            {
                timerOfBonus.Start();
                bool accept = false;
                int bonusX = 0;
                int bonusY = 0;
                while (!accept)
                {
                    bonusX = random.Next(0, columsCount);
                    bonusY = random.Next(0, rowsCount);
                    accept = true;
                    List<Point> snakeBody = snake.getBody();
                    foreach (Point currentLink in snakeBody)
                    {
                        if (bonusX == currentLink.X && bonusY == currentLink.Y)
                        {
                            accept = false;
                        }
                    }
                    foreach (Point currentLink in barriers)
                    {
                        if (bonusX == currentLink.X && bonusY == currentLink.Y)
                        {
                            accept = false;
                        }
                    }
                }

                Point BonusPos = new Point(bonusX, bonusY);
                int type = random.Next(1, 4);
                bonusByType.Add(BonusPos, type);
            }
        }

        void createBarriers()
        {
            int barrierX = random.Next(1, columsCount-1);
            int barrierY = random.Next(1, rowsCount-1);
            List<Point> snakeBody = snake.getBody();
            foreach (Point currentLink in snakeBody)
            {
                if (barrierX == currentLink.X && barrierY == currentLink.Y)
                {
                    createBarriers();
                    return;
                }
            }
            Point lNew = new Point(barrierX, barrierY);
            barriers.Add(lNew);
            //Point lNew1 = new Point(barrierX + 1, barrierY);
            //barriers.Add(lNew1);
            //Point lNew2 = new Point(barrierX + 2, barrierY);
            //barriers.Add(lNew2);
            //Point lNew3 = new Point(barrierX + 3, barrierY);
            //barriers.Add(lNew3);
        }

        public bool barriersCrash()
        {           
            foreach (Point currentLink in barriers)
            {
                if (snake.nextHeadPosition() == currentLink)
                {
                    return true;
                }
            }
            return false;
        }

        public bool eat()
        {
            foreach (Point currentLink in foodByScore.Keys)
            {
                if (snake.headPosition() == currentLink)
                {
                    addScore(foodByScore[currentLink]);
                    foodByScore.Remove(currentLink);
                    snake.grow();
                    createFood();
                    Invalidate();
                    return true;
                }
            }
            return false;
        }

        public bool eatBonus()
        {

            foreach (Point currentLink in bonusByType.Keys)
            {
                if (snake.headPosition() == currentLink)
                {
                    //timerOfBonus.Stop();
                    switch (bonusByType[currentLink])
                    {
                        case 1:
                            addScore(100);
                            break;
                        case 2:
                            addScore(-score());
                            break;
                        case 3:
                            gameOver();
                            break;
                        case 4:
                            snake.cutTail(5);
                            break;
                            
                    }

                    bonusByType.Remove(currentLink);
                    Invalidate();
                    return true;
                }
            }
            return false;
        }

        private void addScore(int pScore)
        {
            Score += pScore;
            ScoreEventArgs args = new ScoreEventArgs(Score);
            OnUpdateScore(args);
            if ((Score % 16) == 0 && Score != 0)
                createBonus();
        }

        void moveSnake()
        {
            if (snake.isSelfCollision())
            {
                gameOver();
                Invalidate();
                return;
            }
            if (snake.collidesWithWall())
            {
                gameOver();
                Invalidate();
                return;
            }
            if (barriersCrash())
            {

                gameOver();
                Invalidate();
                return;
            }
            if (!eat() && !eatBonus())
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
        }

        /// <summary>
        /// Sets time interval for 1 movement of snake (speed).
        /// </summary>
        /// <param name="msec">Miliseconds betweeen 2 updates.</param>
        public void setTimerInterval(int msec)
        {
            timer.Interval = msec;
            switch (timer.Interval)
            {
                case SpeedDefs.LowSpeed:
                    timerOfBonus.Interval = 30000;
                    break;

                case SpeedDefs.MediumSpeed:
                    timerOfBonus.Interval = 19500;
                    break;

                case SpeedDefs.HighSpeed:
                    timerOfBonus.Interval = 12000;
                    break;
            }
        }

        private void loadSettings(GameSettings pSettings)
        {
            rowsCount = pSettings.RowsCount;
            columsCount = pSettings.ColumsCount;
            requestedDirection = pSettings.RequestedDirection;
            snake = new Snake(rowsCount, columsCount, new LinkedList<Point>(pSettings.SnakeBody));
            snake.direction = pSettings.SnakeDirection;
            setTimerInterval(pSettings.Speed);
            barriers.Clear();
            if (pSettings.Barriers.Count > 0)
            {
                barriers = pSettings.Barriers;
            }
            else
            {
                createBarriers();
            }
            Score = pSettings.Score;
            addScore(0);
        }

        public GameSettings getSettings()
        {
            GameSettings rSettings = new GameSettings();
            rSettings.RowsCount = rowsCount;
            rSettings.ColumsCount = columsCount;
            rSettings.Score = Score;
            rSettings.Speed = timer.Interval;
            rSettings.SnakeDirection = snake.direction;
            rSettings.RequestedDirection = requestedDirection;
            rSettings.SnakeBody = snake.getBody();
            rSettings.Barriers = barriers;
            return rSettings;
        }

        public int score()
        {
            return Score;
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
            if (!isGameOver)
            {
                timer.Start();
                Invalidate();
            }
        }

        public bool isRunning()
        {
            return timer.Enabled;
        }

        #endregion

        private void timerOfBonus_Tick_1(object sender, EventArgs e)
        {
            timerOfBonus.Stop();
            bonusByType.Clear();
            Invalidate();
        }

    }

    /***********************************************************************************************************************************************/
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