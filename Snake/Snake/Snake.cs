using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake
{
    class Snake
    {
         /// Members:
        public Direction direction = Direction.Right; /// snake direction
        int XConstraint;
        int YConstraint;

        private LinkedList<Point> body = new LinkedList<Point>(); /// snake position (from head to tail, in relative coordinates)

        /// Constructor.
        public Snake(int rows, int colums, int initialX, int initialY)
        {
            body.AddFirst(new Point(initialX, initialY));
            XConstraint = colums;
            YConstraint = rows;
        }

        private void updateHead(Point newHead)
        {
            body.AddFirst(newHead);
        }

        public void cutTail()
        {
            body.RemoveLast();
        }

        public void move()
        {
            switch (direction)
            {
                case Direction.Left:
                    moveLeft();
                    break;
                case Direction.Up:
                    moveUp();
                    break;
                case Direction.Right:
                    moveRight();
                    break;
                case Direction.Down:
                    moveDown();
                    break;
            }
        }

        private void moveLeft()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X - 1, head.Y);
                if (newHead.X >= 0)
                {
                    updateHead(newHead);
                    cutTail();
                }
            }            
        }

        private void moveUp()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X, head.Y - 1);
                if (newHead.Y >= 0)
                {
                    updateHead(newHead);
                    cutTail();
                }
            }
        }

        private void moveRight()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X + 1, head.Y);
                if (newHead.X < XConstraint)
                {
                    updateHead(newHead);
                    cutTail();
                }
            }
        }

        private void moveDown()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X, head.Y + 1);
                if (newHead.Y < YConstraint)
                {
                    updateHead(newHead);
                    cutTail();
                }
            }
        }

        public Point headPosition()
        {
            if (body.Count > 0)
            {
                Point point = body.First.Value;
                return point;
            }
            else
            {
                return new Point();
            }
        }

        public List<Point> getBody()
        {
            return body.ToList<Point>();
        }

    }
}
