using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcs
{
    struct Location
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RIndex{get;set;}
        /// <summary>
        /// 列索引
        /// </summary>
        public int CIndex{get;set;}

        /// <summary>
        /// 创建一个新位置
        /// </summary>
        /// <param name="rIndex"></param>
        /// <param name="cIndex"></param>
        public Location(int rIndex,int cIndex):this()
        {
            this.RIndex = rIndex;
            this.CIndex = cIndex;
        }
    }


    enum Snake : int
    {
        Wall = 0,
        Null = 1,
        Head = 2,
        Body = 3,
        Die = 4,
        Food =5

    }
    enum MoveDirection : int
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        Stop = 4
    }
    class SnakeGameCore
    {
        //字段
        private Snake[,] map;
        private List<Location> snakeList;
        private List<Location> nullMapList;
        private Random random;
        private MoveDirection direction;
        private bool isGameOver;
        private bool isWin;
        private bool produceFood;

        //属性
        public bool IsUodateMapComplete { get; set; }
        public MoveDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }
        public bool IsWin//只读
        {
            get { return this.isWin; }
        }
        public bool IsGameOver//只读
        {
            get { return this.isGameOver; }
        }
        public int Score//只读
        {
            get { return this.snakeList.Count; }
        }
        public Snake[,] Map//只读用于显示
        {
            get { return this.map; }
        }

        //构造函数
        public SnakeGameCore(int r = 10, int c = 10)
        {
            direction = MoveDirection.Up;
            isGameOver = false;
            produceFood = true;
            map = new Snake[r, c];
            snakeList = new List<Location>(r * c);
            nullMapList = new List<Location>(r * c);
            random = new Random();
        }
        //初始化地图相关的一些狗东西
        public void InitMap()
        {
            isGameOver = false;
            isWin = false;
            produceFood = true;
            direction = MoveDirection.Up;
            snakeList.Clear();
            nullMapList.Clear();
            


            //初始化地图
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (r == 0 || c == 0 || c == map.GetLength(1) - 1 || r == map.GetLength(0) - 1)
                    {
                        map[r, c] = Snake.Wall;
                    }
                    else
                    {
                        map[r, c] = Snake.Null;
                    }
                }
            }

            //产生蛇头 和两节蛇尾身子
            snakeList.Add(new Location(map.GetLength(0) / 2, map.GetLength(1) / 2));
            snakeList.Add(new Location(map.GetLength(0) / 2 + 1, map.GetLength(1) / 2));
            snakeList.Add(new Location(map.GetLength(0) / 2 + 2, map.GetLength(1) / 2));
            //= map.GetLength(0)
            //CreateFood();
        }
        //统计空白地区列表
        private void CalculateEmpty()
        {
            //每次统计新位置先清空列表
            nullMapList.Clear();
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (map[r, c] == Snake.Null)
                    {
                        nullMapList.Add(new Location(r, c));
                        //记录r c 一个空位置
                    }
                }
            }
        }
        //产生食物
        public void CreateFood()
        {
            CalculateEmpty();
            if (nullMapList.Count > 0)
            {
                int randomIndex = random.Next(0, nullMapList.Count);
                Location loc = nullMapList[randomIndex];
                map[loc.RIndex, loc.CIndex] = Snake.Food;
                //map[loc.RIndex, loc.CIndex] = random.Next(0, 10) < 1 ? 4 : 2;
            }
        }
        //吃食物 在尾巴 新产生一个 身体
        public void EatFood()
        {
            snakeList.Add(new Location(snakeList[snakeList.Count - 1].RIndex, snakeList[snakeList.Count - 1].CIndex));
        }
        //更新地图
        public void UpdateMap()
        {

            //开始进入更新地图  更新地图未完成
            IsUodateMapComplete = false;
            //判断方向
            switch (direction)
            {
                case MoveDirection.Up:
                    map[snakeList[snakeList.Count - 1].RIndex, snakeList[snakeList.Count - 1].CIndex] = Snake.Null;
                    if (map[snakeList[0].RIndex - 1, snakeList[0].CIndex] == Snake.Food)
                    {
                        EatFood();
                        produceFood = true;
                    }
                    else if (map[snakeList[0].RIndex - 1, snakeList[0].CIndex] == Snake.Wall || map[snakeList[0].RIndex - 1, snakeList[0].CIndex] == Snake.Body)
                    {
                        map[snakeList[0].RIndex, snakeList[0].CIndex] = Snake.Die;
                        isGameOver = true;
                    }
                    snakeList.Insert(0, new Location(snakeList[0].RIndex - 1, snakeList[0].CIndex));
                    snakeList.RemoveAt(snakeList.Count - 1);
                    break;
                case MoveDirection.Down:
                    map[snakeList[snakeList.Count - 1].RIndex, snakeList[snakeList.Count - 1].CIndex] = Snake.Null;
                    if (map[snakeList[0].RIndex + 1, snakeList[0].CIndex] == Snake.Food)
                    {
                        EatFood();
                        produceFood = true;
                    }
                    else if (map[snakeList[0].RIndex + 1, snakeList[0].CIndex] == Snake.Wall || map[snakeList[0].RIndex + 1, snakeList[0].CIndex] == Snake.Body)
                    {
                        map[snakeList[0].RIndex, snakeList[0].CIndex] = Snake.Die;
                        isGameOver = true;
                    }
                    snakeList.Insert(0, new Location(snakeList[0].RIndex + 1, snakeList[0].CIndex));
                    snakeList.RemoveAt(snakeList.Count - 1);

                    break;
                case MoveDirection.Left:
                    map[snakeList[snakeList.Count - 1].RIndex, snakeList[snakeList.Count - 1].CIndex] = Snake.Null;
                    if (map[snakeList[0].RIndex, snakeList[0].CIndex - 1] == Snake.Food)
                    {
                        EatFood();
                        produceFood = true;
                    }
                    else if (map[snakeList[0].RIndex, snakeList[0].CIndex - 1] == Snake.Wall || map[snakeList[0].RIndex, snakeList[0].CIndex - 1] == Snake.Body)
                    {
                        map[snakeList[0].RIndex, snakeList[0].CIndex] = Snake.Die;
                        isGameOver = true;
                    }
                    snakeList.Insert(0, new Location(snakeList[0].RIndex, snakeList[0].CIndex - 1));
                    snakeList.RemoveAt(snakeList.Count - 1);

                    break;
                case MoveDirection.Right:
                    map[snakeList[snakeList.Count - 1].RIndex, snakeList[snakeList.Count - 1].CIndex] = Snake.Null;
                    if (map[snakeList[0].RIndex, snakeList[0].CIndex + 1] == Snake.Food)
                    {
                        EatFood();
                        produceFood = true;
                    }
                    else if (map[snakeList[0].RIndex, snakeList[0].CIndex + 1] == Snake.Wall || map[snakeList[0].RIndex, snakeList[0].CIndex + 1] == Snake.Body)
                    {
                        map[snakeList[0].RIndex, snakeList[0].CIndex] = Snake.Die;
                        isGameOver = true;
                    }
                    snakeList.Insert(0, new Location(snakeList[0].RIndex, snakeList[0].CIndex + 1));
                    snakeList.RemoveAt(snakeList.Count - 1);
                    break;
            }
            //更新 蛇身子各个坐标
            if (!isGameOver)
            {
                for (int i = 0; i < snakeList.Count; i++)
                {
                    if(i==0)
                        map[snakeList[i].RIndex, snakeList[i].CIndex] = Snake.Head;
                    else 
                        map[snakeList[i].RIndex, snakeList[i].CIndex] = Snake.Body;
                }
            }

            //更新完成 以便外面可以接受按键
            IsUodateMapComplete = true;
            if (produceFood)
            {
                CreateFood();
                produceFood = false;
            }
            if (snakeList.Count >= (map.GetLength(0) - 2) * (map.GetLength(1) - 2))
            {
                isWin = true;
            }
        }
    }
}
