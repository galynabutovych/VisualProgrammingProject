using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        #region Members & Constructors
        public Direction direction = Direction.Right; /// snake direction
        int XConstraint;
        int YConstraint;
        bool collides = false;

        private LinkedList<Point> body; /// snake position (from head to tail, in relative coordinates)

        /// Constructor.
        public Snake(int rows, int colums, int initialX, int initialY)
        {
            body = new LinkedList<Point>();
            body.AddFirst(new Point(initialX, initialY));
            XConstraint = colums;
            YConstraint = rows;
        }

        public Snake(int rows, int colums, LinkedList<Point> initialBody)
        {
            body = new LinkedList<Point>(initialBody);
        }
        #endregion

        private void updateHead(Point newHead)
        {
            body.AddFirst(newHead);
        }

        public void cutTail()
        {
            if(body.Count > 0)
            body.RemoveLast();
        }

        public void growLeft()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X - 1, head.Y);
                if (newHead.X >= 0)
                {
                    updateHead(newHead);
                }
                else
                {
                    collides = true;
                    // wall
                }
            }
        }

        public void moveLeft()
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
                else
                {
                    collides = true;
                    // wall
                }
            }
            
        }

        public void growUp()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X, head.Y - 1);
                if (newHead.Y >= 0)
                {
                    updateHead(newHead);
                }
                else
                {
                    collides = true;
                    // wall
                }
            }
        }

        public void moveUp()
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
                else
                {
                    collides = true;
                    // wall
                }
            }
        }

        public void growRight()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X + 1, head.Y);
                if (newHead.X < XConstraint)
                {
                    updateHead(newHead);
                }
                else
                {
                    collides = true;
                    // wall
                }
            }
        }

        public void moveRight()
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
                else
                {
                    collides = true;
                    // wall
                }
            }
        }

        public void growDown()
        {
            Point head = headPosition();
            if (body.Count > 0)
            {
                Point newHead = new Point(head.X, head.Y + 1);
                if (newHead.Y < YConstraint)
                {
                    updateHead(newHead);
                }
                else
                {
                    collides = true;
                    // wall
                }
            }
        }

        public void moveDown()
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
                else
                {
                    collides = true;
                    // wall
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

        public Point tailPosition()
        {
            if (body.Count > 0)
            {
                Point point = body.Last.Value;
                return point;
            }
            else
            {
                return new Point();
            }
        }

        public bool collidesWithWall()
        {
            return collides;
        }

        public bool isSelfCollision()
        {
            bool collides = false;
            if (body.Count > 0)
            {
                collides = true;
                Point newHead = headPosition();
                switch (direction)
                {
                    case Direction.Left:
                        newHead.X--;
                        break;
                    case Direction.Up:
                        newHead.Y--;
                        break;
                    case Direction.Right:
                        newHead.X++;
                        break;
                    case Direction.Down:
                        newHead.Y++;
                        break;
                }

                foreach (Point point in body)
                {
                    if (collides)
                    {
                        // point is head 
                        collides = false;
                        continue;
                    }
                    if (point.Equals(newHead) && !point.Equals(tailPosition()))
                    {
                        // collision occured
                        collides = true;
                        break;
                    }
                }
            }
            return collides;
        }

        public List<Point> getBody()
        {
            return body.ToList<Point>();
        }

		public void grow()
        {
            if (direction == Direction.Down) growDown();
            else if (direction == Direction.Right) growRight();
            else if (direction == Direction.Left) growLeft();
            else if (direction == Direction.Up) growUp();
        }
    }
}
