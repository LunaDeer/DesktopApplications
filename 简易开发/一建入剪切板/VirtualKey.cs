using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
//直接给你贴一个我自己写的类，模拟键盘输入字符
 
namespace lisonFunc
{

  
	
    static class MyKey
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]  
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
 
        /// <summary>
        /// 按一个键后等待一段时间
        /// </summary>
        /// <param name="keyCode">要按的键，如Keys.Enter</param>
        /// <param name="wait">等待的时间（毫秒）</param>
        public static void KeyPress(Keys keyCode, int wait)
        {
            keybd_event((byte)keyCode, 0, 0, 0);
            keybd_event((byte)keyCode, 0, 2, 0);
            Thread.Sleep(wait);
        }
        /// <summary>
        /// Ctrl+...组合键
        /// </summary>
        /// <param name="keyCode">要同时按下的键，如Keys.C</param>
        public static void ControlKey(Keys keyCode)
        {
            keybd_event((byte)Keys.ControlKey, 0, 0, 0);
            keybd_event((byte)keyCode, 0, 0, 0);
            keybd_event((byte)keyCode, 0, 2, 0);
            keybd_event((byte)Keys.ControlKey, 0, 2, 0);
        }
		
		
        /// <summary>
        /// 重复n次按某个键
        /// </summary>
        /// <param name="keyCode">要按的键，如Keys.Enter</param>
        /// <param name="Times">按键次数</param>
        /// <param name="wait">间隔时间（毫秒）</param>
        public static void KeyPressRep(Keys keyCode, int Times, int wait)
        {
            for (int i = 0; i < Times; i++)
            {
                KeyPress(keyCode,wait);
            }
        }
        /// <summary>
        /// 输入一串数字
        /// </summary>
        /// <param name="NumWords"></param>
        public static void TypeNums(string NumWords)
        {
            foreach (char c in NumWords.ToCharArray())
            {
                KeyPress((Keys)(48 + c), 50);
            }
        }
		
		
		
    }
	
	
	
	
	
}










