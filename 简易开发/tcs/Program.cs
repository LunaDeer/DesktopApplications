using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tcs
{
    //逻辑   控制  界面  三者分开写
    //一定要   !!!!高内聚 低耦合!!!!!
    //不要像以前一样 写屎
    //返回数据多 一定要新建一个类来完成
    class Program
    {
        /*static void Main1()
        {
            //Console.WindowLeft = 5;
            //Console.WindowTop = 5;SetCursorPosition 
            Console.BackgroundColor = ConsoleColor.Blue; //设置背景色
            Console.ForegroundColor = ConsoleColor.White; //设置前景色，即字体颜色
            Console.Write("  ");
            Console.ResetColor(); //将控制台的前景色和背景色设为默认值

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string value = "第三行 绿暗绿";
            Console.WriteLine(value.PadRight(Console.WindowWidth - value.Length)); //设置一整行的背景色
        }*/
        static void Main(string[] args)
        {


            int mapWidth = 17, mapHeight = 17;
            SnakeGameCore core = new SnakeGameCore(mapWidth, mapHeight);//贪吃蛇核心类
            int level = 300;
            
            
            //Console.WindowWidth = mapWidth*2;
            //Console.WindowHeight = mapHeight+3;
            //Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
            
            
            

            
            ConsoleMode.SetWindow("ConsoleDemo",mapWidth*2+1, mapHeight+3);
            ConsoleMode.ConsoleModeInit();
            ConsoleMode.DisbleQuickEditMode();
            //core.EatFood();
            

            
            while (true)
            {
                core.InitMap();//初始化地图
                while (true)
                {
                    {
                        while (Console.KeyAvailable && core.IsUodateMapComplete)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            switch (key.Key)
                            {
                                case ConsoleKey.A: if (core.Direction != MoveDirection.Right) core.Direction = MoveDirection.Left; break;
                                case ConsoleKey.D: if (core.Direction != MoveDirection.Left) core.Direction = MoveDirection.Right; break;
                                case ConsoleKey.W: if (core.Direction != MoveDirection.Down) core.Direction = MoveDirection.Up; break;
                                case ConsoleKey.S: if (core.Direction != MoveDirection.Up) core.Direction = MoveDirection.Down; break;
                            }
                            core.IsUodateMapComplete = false;
                        }
                        if (core.IsGameOver || core.IsWin) break;
                        core.UpdateMap();//更新地图
                        DrawMap(core.Map);//画更新地图 这个不应该在 游戏核心类里面  因为这个是界面相关的
                        Console.Write("得分{0}\n", core.Score);
                    }
                    if (level - 10 * (core.Score / 1) > 50)
                    {
                        System.Threading.Thread.Sleep(level - 10 * (core.Score));
                    }
                    else 
                    {
                        System.Threading.Thread.Sleep(50);
                    }
                }
                if (core.IsGameOver) 
                    Console.WriteLine("按回车重新开始!");
                if (core.IsWin) 
                    Console.WriteLine("你赢了!");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //显示部分代码
        private static void DrawMap(Array arr)
        {
            Console.SetCursorPosition(0, 0);
            object a = 0;
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                for (int c = 0; c < arr.GetLength(1); c++)
                {
                    switch ((int)arr.GetValue(r, c))
                    {
                        case (int)Snake.Null: DrawColor(ConsoleColor.Black, "  ");  break;
                        case (int)Snake.Wall: DrawColor(ConsoleColor.DarkGray, "  "); break;
                        case (int)Snake.Head: DrawColor(ConsoleColor.Magenta, "  ");  break;
                        case (int)Snake.Body: DrawColor(ConsoleColor.Blue, "  ");  break;
                        case (int)Snake.Die: DrawColor(ConsoleColor.Red, "  "); break;
                        case (int)Snake.Food: DrawColor(ConsoleColor.White, "  "); break;
                    }
                }
                Console.WriteLine();
            }
            ResetColor();
        }
        private static void DrawColor(ConsoleColor backgroundColor, ConsoleColor foregroundColor,string str) 
        {
            Console.BackgroundColor = backgroundColor; //设置背景色
            Console.ForegroundColor = foregroundColor; //设置前景色，即字体颜色
            Console.Write(str);
        }
        private static void DrawColor(ConsoleColor backgroundColor, string str)
        {
            Console.BackgroundColor = backgroundColor; //设置背景色
            Console.Write(str);
        }
        private static void ResetColor() { 
            Console.ResetColor(); //将控制台的前景色和背景色设为默认值
        }

    }
}
