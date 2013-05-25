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

        private int rowsCount;
        private int columsCount;
        private int score = 0;
        // what is on a field:
        private Dictionary<Point, int> foodByScore = new Dictionary<Point, int>();
        // snake shape, speed and direction:
        private Direction requestedDirection = Direction.Right;
        private Direction snakeDirection = Direction.Right;
        private LinkedList<Point> snakeBody;
        private int speed = 900;
        public GameSettings() { }
        #endregion

        #region Properties, setters and getters
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
        public LinkedList<Point> SnakeBody
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
    }
}
