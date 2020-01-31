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
        public int X { get; private set; }
        public int Y { get; private set; }

        public Parth(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void Move(Parth obj)
        {
            this.X = obj.X;
            this.Y = obj.Y;
        }
        public void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    class Snake
    {
        public List<Parth> Body = new List<Parth>();
        
        public Movement Movement { get; set; }

        public int Hp { get; set; }



        public Snake()
        {
            Hp = 100;
            Body.Add(new Parth(0, 0));
            Body.Add(new Parth(0, 1));
            Body.Add(new Parth(0, 2));
            Body.Add(new Parth(0, 3));
            Body.Add(new Parth(0, 4));
        }

        public bool Move()
        {
            int moveX = 1, moveY = 0;
            switch (Movement)
            {
                case Movement.Up:
                    moveX = 0;
                    moveY = 1;
                    break;
                case Movement.Down:
                    moveX = 0;
                    moveY = -1;
                    break;
                case Movement.Left:
                    moveX = -1;
                    moveY = 0;
                    break;
                case Movement.Right:
                    moveX = 1;
                    moveY = 0;
                    break;
            }
            List<Parth> tmp = new List<Parth>(Body);

            Body[0].Move(tmp[0].X + moveX, tmp[0].Y+ moveY);
            for(int i = 1; i < Body.Count; i++)
            {
                Body[i] = tmp[i - 1];
            }

            return true;
        }

        public void AddBody()
        {
            int previousIndex = Body.Count - 1;
            int moveX = Body[previousIndex].X - Body[previousIndex - 1].X, moveY = Body[previousIndex].Y - Body[previousIndex - 1].Y;
            
            Body.Add(new Parth(Body[previousIndex].X + moveX, Body[previousIndex].Y + moveY));
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
                
                menu.UpdateDraw(snake, ts);
                Thread.Sleep(200);
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
        public void UpdateDraw(Snake snk, TimeSpan ts)
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
        public string SnakeBodyToString(Snake snk)
        {
            char[,] output = new char[FieldLenght, FieldHeight];

            foreach(Parth obj in snk.Body)
            {
                output[FieldLenght / 2 + obj.X, FieldHeight / 2 + obj.Y] = '▉';
            }
            //Дебил, сделай нормальный интерсей, а я спать.
        }
    }
}