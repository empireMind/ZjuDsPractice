using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_6
{
    class Graph_62
    {
        private static Player jamesBond;
        private static List<Node> crocs = new List<Node>();

        public static void gameReady(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] data = sr.ReadLine().Split(new char[] { ' ' });
                    int total = int.Parse(data[0]);
                    jamesBond = new Player(int.Parse(data[1]));
                    for (int i = 0; i < total; i++)
                    {
                        string[] position = sr.ReadLine().Split(new char[] { ' ' });
                        crocs.Add(new Node(int.Parse(position[0]), int.Parse(position[1])));
                    }
                }
            }
            
            bool isAlive = liveOrDead(jamesBond);
            if (isAlive)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
        }

        public static bool liveOrDead(Player jamesBond)
        {
            bool ret = false;
            Node start = new Node(jamesBond.X, jamesBond.Y);
            foreach(Node croc in crocs)
            {
                if(croc.isVisited == false && jamesBond.canReach(croc))
                {
                    jamesBond.JumpTo(croc);
                    croc.isVisited = true;
                    if (jamesBond.isSafe())
                    {
                        ret = true;
                        break;
                    }
                    else
                    {
                        if(liveOrDead(jamesBond))
                        {
                            ret = true;
                            break;
                        }
                        else
                            jamesBond.JumpTo(start);
                    } 
                }
            }               
            return ret;
        }
    }

    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool isVisited { get; set; }

        public Node() { }
        public Node(int x, int y)
        {
            X = x;
            Y = y;
            isVisited = false;
        }                    
    }

    class Player : Node
    {
        private int step;
        //安全岛的直径为15
        private double radius = 15 / 2.0;

        public Player(int step)
        {
            //安全岛原点为(0,0)
            X = Y = 0;
            this.step = step;
        }

        public void JumpTo(Node node)
        {
            X = node.X;
            Y = node.Y;
        }

        public bool canReach(Node node)
        {
            if( X==0 && Y==0 )
            {
                if (Math.Pow(node.X, 2) + Math.Pow(node.Y, 2) <= Math.Pow(step + radius, 2))
                    return true;
                else
                    return false;
            }
            else
            {
                if (Math.Pow(node.X - X, 2) + Math.Pow(node.Y - Y, 2) <= step * step)
                    return true;
                else
                    return false;
            }
        }

        public bool isSafe()
        {
            if (canEscape(X) || canEscape(Y))
                return true;
            else
                return false;
        }

        private bool canEscape(int pos)
        {
            bool ret = false;
            if (pos > 0)
            {
                if (50 - pos <= step)
                    ret = true;
            }
            else if (pos <= 0)
            {
                if (pos + 50 <= step)
                    ret = true;
            }
            return ret;
        }
    }
}
