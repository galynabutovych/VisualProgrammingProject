using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Media;
using System.Drawing;

namespace Snake
{
    public class GameSettings
    {
        #region Variables & Constructors

        string user = "";
        private int rowsCount;
        private int columsCount;
        private int score = 0;
        // what is on a field:
        private Dictionary<Point, int> foodByScore = new Dictionary<Point, int>();
        // snake shape, speed and direction:
        private Direction requestedDirection = Direction.Right;
        private Direction snakeDirection = Direction.Right;
        private List<Point> snakeBody;
        private int speed = 900;
        public GameSettings() { }
        #endregion

        #region Properties, setters and getters
        public string User
        {
            set
            {
                user = value;
            }
            get { return user; }
        }
        public int RowsCount
        {
            set
            {
                if (value > 0)
                {
                    rowsCount = value;
                }
            }
            get { return rowsCount; }
        }
        public int ColumsCount
        {
            set
            {
                if (value > 0)
                {
                    columsCount = value;
                }
            }
            get { return columsCount; }
        }
        public int Score
        {
            set
            {
                if (value >= 0)
                {
                    score = value;
                }
            }
            get { return score; }
        }
        public List<Point> SnakeBody
        {
            set
            {
                if (value.Count > 0)
                {
                    snakeBody = value;
                }
            }
            get { return snakeBody; }
        }
        public Direction SnakeDirection
        {
            set { snakeDirection = value; }
            get { return snakeDirection; }
        }
        public Direction RequestedDirection
        {
            set { requestedDirection = value; }
            get { return requestedDirection; }
        }
        public int Speed
        {
            set
            {
                if (value > 0)
                {
                    speed = value;
                }
            }
            get { return speed; }
        }            

        #endregion

        #region Default
        public static GameSettings Default
        {
            get
            {
                GameSettings defaultSettings = new GameSettings();
                defaultSettings.User = "";
                defaultSettings.Speed = 500;
                defaultSettings.Score = 0;
                defaultSettings.SnakeDirection = Direction.Right;
                defaultSettings.RequestedDirection = Direction.Right;
                defaultSettings.RowsCount = 50;
                defaultSettings.ColumsCount = 50;
                List<Point>  body = new List<Point>();
                body.Add(new Point(2, 0));
                body.Add(new Point(1, 0));
                body.Add(new Point(0, 0));
                defaultSettings.snakeBody = body;
                return defaultSettings;
            }
        }
        #endregion
    }
}
