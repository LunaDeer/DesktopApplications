using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
namespace lisonFunc
{
	class Fun
	{
		public static bool flag = false;
		public static int i = 0;

		public static void aaaa()
		{
			Thread.Sleep(100);
			flag = !flag;
			Console.WriteLine(flag.ToString());
			while(flag)
			{
				//MyClipboard.SetText("????");
				MyKey.KeyPress(Keys.A,500); // 按下A
				Console.WriteLine(i++.ToString());
				//System.Windows.Forms.SendKeys.Send("{TAB}");
				Thread.Sleep(500);
			}
		}
		
		public static void EnterNumber(string NumWords)
		{
			Thread.Sleep(100);
			flag = !flag;
			//Console.WriteLine(flag.ToString());
			MyKey.TypeNums(NumWords);

		}
	}
}