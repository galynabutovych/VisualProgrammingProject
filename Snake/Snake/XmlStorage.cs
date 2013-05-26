using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Snake
{
    public class XmlStorage : Storage
    {

        public XmlStorage()
        {
        }

        public void storeGame(GameSettings lGame, String lName)
        {
            if (!lName.EndsWith(".xml"))
            {
                lName += ".xml";
            }
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlNode rootNode = doc.CreateElement("root");
            doc.AppendChild(rootNode);

            XmlAttribute attribute = doc.CreateAttribute("user");
            attribute.Value = lGame.User;
            rootNode.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("score");
            attribute.Value = lGame.Score.ToString();
            rootNode.Attributes.Append(attribute);

            // field amd snake settings
            XmlNode game = doc.CreateElement("game");

            attribute = doc.CreateAttribute("rows");
            attribute.Value = lGame.RowsCount.ToString();
            game.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("colums");
            attribute.Value = lGame.ColumsCount.ToString();
            game.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("speed");
            game.Attributes.Append(attribute);
            attribute.Value = lGame.Speed.ToString();

            attribute = doc.CreateAttribute("direction");
            attribute.Value = lGame.SnakeDirection.ToString();
            game.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("requested_direction");
            attribute.Value = lGame.RequestedDirection.ToString();
            game.Attributes.Append(attribute);

            //snake
            XmlNode snake = doc.CreateElement("snake");
            System.Drawing.PointConverter lPointConverter = new System.Drawing.PointConverter();
            foreach (System.Drawing.Point currentChunk in lGame.SnakeBody)
            {
                XmlNode chunk = doc.CreateElement("chunk");
                chunk.InnerText = lPointConverter.ConvertToString(currentChunk);
                snake.AppendChild(chunk);
            }
            game.AppendChild(snake);

            rootNode.AppendChild(game);
            doc.Save(lName);

        }

        public GameSettings loadGame(String lName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(lName);
            GameSettings rGame = new GameSettings();

            rGame.User = doc.DocumentElement.GetAttribute("user");
            rGame.Score = Convert.ToInt32(doc.DocumentElement.GetAttribute("score"));

            XmlNodeList childnodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode node in childnodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (node.Name == "game")
                    {
                        XmlAttributeCollection lAttributes = node.Attributes;
                        rGame.RowsCount = Convert.ToInt32(lAttributes.GetNamedItem("rows").Value);
                        rGame.ColumsCount = Convert.ToInt32(lAttributes.GetNamedItem("colums").Value);
                        rGame.Speed = Convert.ToInt32(lAttributes.GetNamedItem("speed").Value);
                        string direction = lAttributes.GetNamedItem("direction").Value;
                        if (direction == Direction.Left.ToString())
                        {
                            rGame.SnakeDirection = Direction.Left;
                        }
                        else if (direction == Direction.Right.ToString())
                        {
                            rGame.SnakeDirection = Direction.Right;
                        }
                        else if (direction == Direction.Up.ToString())
                        {
                            rGame.SnakeDirection = Direction.Up;
                        }
                        else if (direction == Direction.Down.ToString())
                        {
                            rGame.SnakeDirection = Direction.Down;
                        }
                        direction = lAttributes.GetNamedItem("requested_direction").Value;
                        if (direction == Direction.Left.ToString())
                        {
                            rGame.RequestedDirection = Direction.Left;
                        }
                        else if (direction == Direction.Right.ToString())
                        {
                            rGame.RequestedDirection = Direction.Right;
                        }
                        else if (direction == Direction.Up.ToString())
                        {
                            rGame.RequestedDirection = Direction.Up;
                        }
                        else if (direction == Direction.Down.ToString())
                        {
                            rGame.RequestedDirection = Direction.Down;
                        }

                        XmlNodeList gameChildnodes = node.ChildNodes;
                        foreach (XmlNode childnode in gameChildnodes)
                        {
                            if (childnode.Name == "snake")
                            {
                                List<System.Drawing.Point> body = new List<System.Drawing.Point>();
                                System.Drawing.PointConverter lPointConverter = new System.Drawing.PointConverter();
                                foreach (XmlNode snakeNode in childnode.ChildNodes)
                                {
                                    System.Drawing.Point lChunc = (System.Drawing.Point) lPointConverter.ConvertFromString(snakeNode.InnerText);
                                    body.Add(lChunc);
                                }
                                rGame.SnakeBody = body;
                            }
                        }
                    }
                }
            }

            return rGame;
        }
    }
}
