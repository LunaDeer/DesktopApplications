using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;//dllimp
using System.IO;
using System.Collections;

namespace lisonFunc
{
	public struct CONSOLE_CURSOR_INFO {
		public uint dwSize;    //光标的高度，控制台一行字符的高度为100，光标的高度为1到100
		public uint bVisible;      //是否显示光标，TRUE为显示，FALSE为不显示
		public CONSOLE_CURSOR_INFO(uint dwSize,uint bVisible):this()
        {
            this.dwSize = dwSize;
            this.bVisible = bVisible;
        }
	};
	
	
	public struct Location
    {
        public double x{get;set;}
        public double y{get;set;}
        public Location(double x,double y):this()
        {
            this.x = x;
            this.y = y;
        }
    };
	
    public class MyConsole
    {
		
		
		
#region 设置控制台标题 禁用关闭按钮
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		public static void DisbleClosebtn()
        {
            IntPtr windowHandle = FindWindow(null, "控制台标题");
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
        }

		public static void SetWindow(string title){
		    Console.Title = title;
			//Console.WindowWidth = BUFFER_WIDTH;
			//Console.WindowHeight = BUFFER_HEIGTH;
			// Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
		}
		
	
        protected static void CloseConsole(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(0);
        }




#endregion



        const int STD_INPUT_HANDLE = -10;
        const int STD_OUTPUT_HANDLE = -11;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const uint ENABLE_INSERT_MODE = 0x0020;
        const uint ENABLE_MOUSE_INPUT = 0x0008;


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);

		[DllImport("Kernel32.dll")]
		public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, uint wAttributes);






        public static void DisbleQuickEditMode()
        {
            IntPtr hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;//移除快速编辑模式
            mode &= ~ENABLE_INSERT_MODE;      //移除插入模式
            mode &= ~ENABLE_MOUSE_INPUT;
			
			
		
            SetConsoleMode(hStdin, mode);
        }
		
		//设置光标
		public static void SetCursorInfo(uint dwSize,uint bVisible)
        {
			//光标的高度，控制台一行字符的高度为100，光标的高度为1到100
			//是否显示光标，TRUE为显示，FALSE为不显示
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
            CONSOLE_CURSOR_INFO cci = new CONSOLE_CURSOR_INFO(dwSize,bVisible);//隐藏光标
			User32API.SetConsoleCursorInfo(handle,out cci);
			
			
			SetConsoleTextAttribute(handle, 16 | 32 | 64);
        }



		public static void WriteColor(string str, ConsoleColor colorF,ConsoleColor colorB)
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = colorF;
            Console.BackgroundColor = colorB;
            Console.Write(str);
            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;
        }
		
		
		
		
        public  static void ResetColor() { 
            Console.ResetColor(); 
        }





		//坐标输出
		public static void Printxy<T>(T c,Location a)
		{
			if(a.x>=0 && a.y >=0&& a.x<Console.WindowWidth&& a.y<Console.WindowHeight)
			{
				Console.SetCursorPosition((int)a.x,(int)a.y);
				Console.Write(c);
			}
		}
		
		/*
		public static void Write<T>(T str,Location a, ConsoleColor colorB,ConsoleColor colorF)
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = colorF;
            Console.BackgroundColor = colorB;
            Printxy(str,a);
            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;
        }
		*/

		public static void Write<T>(T str,Location a, ConsoleColor colorF = ConsoleColor.Gray,ConsoleColor colorB = ConsoleColor.Black)
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = colorF;
            Console.BackgroundColor = colorB;
            Printxy(str,a);
            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;
        }
		
		public static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b) 
		{
		 ConsoleColor ret = 0; 
		 double rr = r, gg = g, bb = b, delta = double.MaxValue; 

		 foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor))) 
		 { 
		  var n = Enum.GetName(typeof(ConsoleColor), cc); 
		  var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix 
		  var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0); 
		  if (t == 0.0) 
		   return cc; 
		  if (t < delta) 
		  { 
		   delta = t; 
		   ret = cc; 
		  } 
		 } 
		 return ret; 
		}
		
		
/*
Black  #000000 
DarkBlue  #00008B 
DarkGreen #006400 
DarkCyan  #008B8B 
DarkRed  #8B0000 
DarkMagenta #8B008B 
DarkYellow #000000 <-- see comments 
Gray   #808080 
DarkGray  #A9A9A9 
Blue   #0000FF 
Green  #008000 
Cyan   #00FFFF 
Red   #FF0000 
Magenta  #FF00FF 
Yellow  #FFFF00 
White  #FFFFFF 

*/




    }






    public class User32API
    {
        private static Hashtable processWnd = null;

        public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);

        static User32API()
        {
            if (processWnd == null)
            {
                processWnd = new Hashtable();
            }
        }

		[DllImport("Kernel32.dll")]
		public static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, out CONSOLE_CURSOR_INFO c);
		[DllImport("msvcrt.dll", SetLastError = false, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private extern static void system(string command);

        [DllImport("user32.dll", EntryPoint = "EnumWindows", SetLastError = true)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

        [DllImport("user32.dll", EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(uint dwErrCode);

        public static IntPtr GetCurrentWindowHandle()
        {
            IntPtr ptrWnd = IntPtr.Zero;
            uint uiPid = (uint)System.Diagnostics.Process.GetCurrentProcess().Id;  // 当前进程 ID
            object objWnd = processWnd[uiPid];

            if (objWnd != null)
            {
                ptrWnd = (IntPtr)objWnd;
                if (ptrWnd != IntPtr.Zero && IsWindow(ptrWnd))  // 从缓存中获取句柄
                {
                    return ptrWnd;
                }
                else
                {
                    ptrWnd = IntPtr.Zero;
                }
            }

            bool bResult = EnumWindows(new WNDENUMPROC(EnumWindowsProc), uiPid);
            // 枚举窗口返回 false 并且没有错误号时表明获取成功
            if (!bResult && Marshal.GetLastWin32Error() == 0)
            {
                objWnd = processWnd[uiPid];
                if (objWnd != null)
                {
                    ptrWnd = (IntPtr)objWnd;
                }
            }

            return ptrWnd;
        }

        private static bool EnumWindowsProc(IntPtr hwnd, uint lParam)
        {
            uint uiPid = 0;

            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref uiPid);
                if (uiPid == lParam)    // 找到进程对应的主窗口句柄
                {
                    processWnd[uiPid] = hwnd;   // 把句柄缓存起来
                    SetLastError(0);    // 设置无错误
                    return false;   // 返回 false 以终止枚举窗口
                }
            }

            return true;
        }

    }






}