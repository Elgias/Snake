using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeSpace
{
    enum Movement
    {
        Up,
        Left,
        Right,
        Down
    }
    class Parth
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Move(Parth obj)
        {
            this.X = obj.X;
            this.Y = obj.Y;
        }
    }

    class Snake
    {
        public LinkedList<Parth> body;

        public Movement Movement { get; set; }

        public int Hp { get; set; }

        public Snake()
        {
            Hp = 100;
        }

        public bool Move()
        {
            return true;
        }

        public void AddBody()
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            Interface menu = new Interface(48,24);
            Snake snake = new Snake();
            while (true)
            {
                TimeSpan ts = stopWatch.Elapsed;
                menu.Draw(snake, ts);
                Console.Clear();
            }
        }
    }

    class Interface
    {
        public Dictionary<string, bool> parameters;

        int FieldLenght { get; set; }
        int FieldHeight { get; set; }

        public Interface(int fieldLenght, int fieldHeight)
        {
            parameters = new Dictionary<string, bool>();
            parameters.Add("Hp", true);
            parameters.Add("LifeTime", true);
            

            this.FieldHeight = fieldHeight;
            this.FieldLenght = fieldLenght;
        }
        public void Draw(Snake snk, TimeSpan ts)
        {
            string Out = "";
            
            foreach (KeyValuePair<string, bool> obj in parameters)
            {
                switch (obj.Key)
                {
                    case "Hp":
                        Out += "Hp:" + HpToString(snk.Hp);
                        break;
                    case "LifeTime":
                        Out += new string(' ', FieldLenght + 2 - 8 - 5) + TimeToString(ts);
                        break;
                    
                }
            }

            //draw upper line
            Out += "\n╔" + new string('═', FieldLenght) + "╗";

            //draw field
            for (int i = 1; i <= FieldHeight; i++)
            {
                Out += "\n║" + new string(' ', FieldLenght) + "║";
            }

            //draw bottom line
            Out += "\n╚" + new string('═', FieldLenght) + "╝";

            Console.WriteLine(Out);
        }
        
        public string HpToString(int hp)
        {
            string s_hp = new string('█', (hp / 20));
            int shade = (int)(((float)hp / 20 - hp / 20) * 10);

            if (shade >= 9)
                s_hp += '█';
            else if (shade >= 6)
                s_hp += "▓";
            else if (shade >= 3)
                s_hp += "▒";
            else if (shade < 3 && shade != 0)
                s_hp += "░";

            return s_hp;
        }
        public string TimeToString(TimeSpan ts)
        {
            return new  string (String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds));
        }
    }
}
