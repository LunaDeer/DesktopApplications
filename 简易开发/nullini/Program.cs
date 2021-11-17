using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
namespace lisonFunc
{
	static class HelloWorld
    {
        
		//winexemod
		
		[STAThread]
		
		static void Main(string[] args)
        {
            bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                System.Windows.Forms.Application.EnableVisualStyles();   //这两行实现   XP   可视风格   
                System.Windows.Forms.Application.DoEvents();             //这两行实现   XP   可视风格   
                System.Windows.Forms.Application.Run(new Form1());
                //   Main   为你程序的主窗体，如果是控制台程序不用这句   
                mutex.ReleaseMutex();
            }
			
			
            else
            {
                //MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   提示信息，可以删除。   
                Application.Exit();//退出程序   
            }
        }


		/*
		//consolemode
        static void Main(string[] args)
        {
			
			MyConsole.SetWindow("aaa");
			MyConsole.DisbleQuickEditMode();
			MyConsole.SetCursorInfo(100,0);
			Console.WriteLine("ff");
			MyConsole.Printxy("ff\n",new Location(5,2));
			MyConsole.Write("ff\n",new Location(5,2),ConsoleColor.Blue,ConsoleColor.Black);
			MyConsole.Write("ff\n",new Location(5,3),MyConsole.ClosestConsoleColor(55,22,11) ,MyConsole.ClosestConsoleColor(48,87,63) );
			
			
			
			string filePath = File.GetCurrentCsPath()+"\\img01.bmp";
			if(args.Length>0)
			{
				filePath = args[0];
				Console.WriteLine(filePath);
			}
			
			Bmp b = new Bmp(filePath);
			while(true)
			{
				b.ReadAgain(filePath);
				b.ReadBmp();
				//b.PrintBmp();
				
				b.PrintColorBmp();
				
				Console.ReadLine();
				Console.Clear();
			}
			
			
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
		}

		*/
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
