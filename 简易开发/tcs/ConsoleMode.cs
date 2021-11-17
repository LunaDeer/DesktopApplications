using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;//dllimp
using System.IO;
using System.Collections;

namespace Tcs
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



    public class ConsoleMode
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);




        public static void DisbleClosebtn()
        {
            IntPtr windowHandle = myHwnd;
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
        }
        
        
        
		public static void SetWindow(string title){
		    Console.Title = title;
			//Console.WindowWidth = BUFFER_WIDTH;
			//Console.WindowHeight = BUFFER_HEIGTH;
			//Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
		}		
		public static void SetWindow(string title,int w,int h){
		    Console.Title = title;

		    Console.WindowWidth = w; //设置窗体宽度
            Console.BufferWidth = w; //设置缓存宽度
            Console.WindowHeight = h;//设置窗体高度
            Console.BufferHeight = h;//设置缓存高度
            Console.WindowWidth = w; //重新设置窗体宽度

			//Console.WindowWidth = BUFFER_WIDTH;
			//Console.WindowHeight = BUFFER_HEIGTH;
			// Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
		}
		
		
        protected static void CloseConsole(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(0);
        }

        public static IntPtr myHwnd;

        public static void ConsoleModeInit()
        {
            
            //myHwnd = FindWindow(null, "ConsoleDemo");
            
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
            CONSOLE_CURSOR_INFO cci = new CONSOLE_CURSOR_INFO(1,0);//隐藏光标
			User32API.SetConsoleCursorInfo(handle,out cci);
            //Console.WriteLine("{0} ", );

        }


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



        public const int GWL_STYLE = -16;
        public const int WS_MAXIMIZE = 0x1000000;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_OVERLAPPED = 0;
        public const int WS_OVERLAPPEDWINDOW = 0xcf0000;
        //public const int WS_POPUP = 0x80000000;
        //public const int WS_POPUPWINDOW = 0x80880000;
        public const int WS_SIZEBOX = 0x40000;
        public const int WS_SYSMENU = 0x80000;


        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern long SetWindowLong(
            IntPtr hwnd,
            int nIndex,
            long dwNewLong

        );


        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern long GetWindowLong(
            IntPtr hwnd,
            int nIndex
        );




        public static void LockConsoleSize()
        {
            ConsoleMode.SetWindowLong(
                myHwnd,
                GWL_STYLE,
                GetWindowLong(
                    myHwnd, 
                    GWL_STYLE & ~WS_SIZEBOX & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX
                    )
                    
                );
        }





        /*
        LONG_PTR SetWindowLongPtrA(
          [in] HWND hWnd,
          [in] int nIndex,
          [in] LONG_PTR dwNewLong
        );*/

        /*
        GetConsoleWindow

        SetWindowLongPtrA(
        GetConsoleWindow(),
        GWL_STYLE,
        GetWindowLongPtrA(GetConsoleWindow(), GWL_STYLE)
        & ~WS_SIZEBOX & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX
        );
        */




        /*

         static void Main(string[] args)
        {
            Console.Title = "控制台标题";
            DisbleQuickEditMode();
            DisbleClosebtn();
        LockConsoleSize();
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseConsole);
        }


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