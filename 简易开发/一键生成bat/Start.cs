using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MyApplication //namespace声明命名空间,包含一个helloworld的类！
{
    /* 类名为 HelloWorld */
    class HelloWorld  
    {
        /* main函数 */
        static void Main(string[] args)//main函数是C#的接入口！
        {
			SetWindow("一键启动程序制作器  v1.0 by Jxy");
			string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//获取桌面
			//string currentPath = @"D:\Projects\C#Projects\notepad\bat生成";
			List<string> lineList = new List<string>();
			if(args.Length>0)
			{
				for (int i=0;i<args.Length;i++)
				{
					lineList.Add(args[i]);
					Console.WriteLine(lineList[i]);
				};
				CreateBat(targetPath,lineList);
				Console.WriteLine("制作完成!生成至桌面:"+Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Strat.bat");
			}
			else{
				Console.WriteLine("将多个文件一起拖拽到该文件上即可制作一键启动程序!");
			}
			Console.ReadKey();
        }
		static void CreateBat(string path,List<string> lineList)
		{
			FileStream stream = new FileStream(path+"\\Start.bat",FileMode.Create);//fileMode指定是读取还是写入
			StreamWriter writer = new StreamWriter(stream, Encoding.Default);
			
			
			for (int i = 0;i<lineList.Count;i++)
			{
				//writer.WriteLine("cd "+path.Substring(0,3));
				writer.Write("start \"\" ");
				writer.WriteLine("\""+lineList[i]+"\"");//写入一行，写完后会自动换行
				//writer.WriteLine(lineList[i]);//写入一行，写完后会自动换行
			}

			writer.Close();//释放内存
			stream.Close();//释放内存
			
			
		}
		
		static void SetWindow(string title){
		    Console.Title = title;
            // Console.WindowWidth = BUFFER_WIDTH;
            // Console.WindowHeight = BUFFER_HEIGTH;
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
		}
		static void SetWindow(string title ,int w,int h){
		    Console.Title = title;
            Console.WindowWidth = w;
            Console.WindowHeight = h;
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
		}


    }
}

