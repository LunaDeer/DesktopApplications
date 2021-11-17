using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace lisonFunc
{
	
	static class Txt
	{
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
	}
	
	
	
	
	
	
	
	
	
	
	
	
	static class MyClipboard{
		public static string GetText(){
			string clipText = "-1";
			if ( Clipboard.ContainsText( ) )
			{
				clipText = Clipboard.GetText();
			}
			return clipText;
		}
		public static void btnContains_Click(object sender, EventArgs e)
		{
			//判断剪切板中是否包含文本数据
			bool flag = Clipboard.ContainsText();
			Console.WriteLine(flag);
		}

		public static void SetText(string data = "")
		{
			//Clipboard.SetDataObject(data);
			Clipboard.SetText(data);
		}
	}
}