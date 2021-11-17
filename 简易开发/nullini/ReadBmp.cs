using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.IO;
using System.Drawing;
namespace lisonFunc
{
	/*
	class  BB{
		static void Main(string[] args){
			//Console.BackgroundColor = ConsoleColor.White; 
			//Console.ForegroundColor = ConsoleColor.Blue; 	
			
			Bmp b = new Bmp(@"D:\Projects\C#Projects\notepad\读取bmp\img01.bmp");
			while(true)
			{
				b.ReadAgain(@"D:\Projects\C#Projects\notepad\读取bmp\img01.bmp");
				b.ReadBmp();
				//b.PrintBmp();
				
				b.PrintColorBmp();
				
				Console.ReadLine();
				Console.Clear();
			}
		}
	}	
*/
	class Bmp{
		public byte[] bmpdata;
		public byte[] colorData;
		long  imgW;
		long  imgH;
		Color [,] pix;
		public byte[,] colorALineData;
		
		
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
		
		
		
		
		
		
		
		
		
		
		public Bmp(string fileName)
		{
			ReadAgain(fileName);
			
			
		/*
			fileName = @"D:\Projects\C#Projects\notepad\读取bmp\img01.bmp";
			FileStream fs = new FileStream(fileName,FileMode.Open);
			
			
			bmpdata=new byte[fs.Length];
			fs.Read(bmpdata,0,bmpdata.Length);
			fs.Close();
			
			
			colorData = new byte[bmpdata.Length - 0x36];
			for (int i = 0;i<colorData.Length;i++)
			{
				//Console.WriteLine(i);
				colorData[i] = bmpdata[i+0x36];
			}
			
			imgW = bmpdata[0x12];
			imgH = bmpdata[0x16];

			//Console.WriteLine("w = {0},h = {1}", imgW,imgH);


			pix = new Color[imgH,imgW];
			
			
		*/
			
			
			
			/*
			Stream stream = File.OpenRead(fileName);  // 打开位图文件
			
	
			bmpdata = new byte[stream.Length];
			stream.Read(bmpdata, 0, bmpdata.Length);
			imgW = bmpdata[0x12];
			imgH = bmpdata[0x16];
			pix = new Color[imgH,imgW];
			
			
            colorData = new byte[stream.Length - 54];  // 缓冲区，文件长度减去文件头和信息头的长度

            stream.Position = 54;  // 跳过文件头和信息头

            stream.Read(colorData, 0, colorData.Length);  // 读取位图数据，位图数据是颠倒的
			
			*/
			
			
			
			
			
		}
		public void ReadAgain(string fileName)
		{
			//fileName = @"D:\Projects\C#Projects\notepad\读取bmp\img01.bmp";
			FileStream fs = new FileStream(fileName,FileMode.Open);
			
			
			bmpdata=new byte[fs.Length];
			fs.Read(bmpdata,0,bmpdata.Length);
			fs.Close();
			
			
			colorData = new byte[bmpdata.Length - 0x36];
			for (int i = 0;i<colorData.Length;i++)
			{
				//Console.WriteLine(i);
				colorData[i] = bmpdata[i+0x36];
			}
			
			imgW = bmpdata[0x12] + bmpdata[0x13]*256 + bmpdata[0x14]*4294967296;
			imgH = bmpdata[0x16] + bmpdata[0x17]*256 + bmpdata[0x18]*4294967296;
			//bmpdata[0x17]
			//Console.WriteLine("w = {0},h = {1}", imgW,imgH);

			
			pix = new Color[imgH,imgW];
		
		}

		public void ReadBmp()
		{
			Console.WriteLine("w = {0},h = {1}", imgW,imgH);
			
			long x = 0,y = imgH-1;
			long lineNum = (long)(Math.Ceiling((3*imgW)/4.0))*4;   


			//Console.WriteLine(lineNum);
			/*for (int i = 0;i<colorData.Length;i++)
			{
				if(i%(lineNum) ==0)
					Console.WriteLine();
				if(i%(3) ==0)
					Console.Write("|");

				Console.Write ("{0,3}",colorData[i]);
			}*/
			
			
			for (long i = 0;i<colorData.Length;i+=lineNum)
			{
				x = 0;
				for (long j = i;j<i+(3*imgW);j+=3)
				{
					//Console.Write ("[{0,3}]",j);
					//Console.Write ("{0,3}",colorData[i]);
					//Console.Write ( String.Format("|{0,3},{1,3},{2,3}|", colorData[j],colorData[j+1],colorData[j+2]));
					//Console.Write ("{0,3}",".");
					pix[y,x]= Color.FromArgb(colorData[j],colorData[j+1],colorData[j+2]);

					x++;
				}

				y--;


			}

		}
		
		
		public void PrintColorBmp()
		{
			int Gray = 0;
			String a  = "M@WB08ZZ2SX7r;i:,. ";
			//a  = "@@@@@@@######MMMBBHHHAAAA&&GGhh9933XXX222255SSSiiiissssrrrrrrr;;;;;;;;:::::::,,,,,,,........        ";
			for (int y = 0; y < pix.GetLength(0); y++)//y
			{
				for (int x = 0; x < pix.GetLength(1); x++)//x
				{
					//Gray = (pix[y,x].R*299 + pix[y,x].G*587 + pix[y,x].B*114 + 500) / 1000;
					MyConsole.WriteColor("  ", ConsoleColor.Red,ClosestConsoleColor(pix[y,x].B,pix[y,x].G,pix[y,x].R));
					//Console.Write ("{0,3}",255-Gray);
					
					//Console.Write(a[(int)((Gray/255.0)*(a.Length-1))]);
					//Console.Write(a[(int)((Gray/255.0)*(a.Length-1))]);
					
					/*if(pix[y,x] == Color.FromArgb(255,255,255,255))
					{
						Console.Write(a[0]);
					}
					
					else
					{
						Console.Write("#");
					}*/
				
					
				}
				Console.WriteLine();
			}

		}
		
		public void PrintBmp()
		{
			int Gray = 0;
			String a  = "M@WB08ZZ2SX7r;i:,. ";
			//a  = "@@@@@@@######MMMBBHHHAAAA&&GGhh9933XXX222255SSSiiiissssrrrrrrr;;;;;;;;:::::::,,,,,,,........        ";
			for (int y = 0; y < pix.GetLength(0); y++)//y
			{
				for (int x = 0; x < pix.GetLength(1); x++)//x
				{
					Gray = (pix[y,x].R*299 + pix[y,x].G*587 + pix[y,x].B*114 + 500) / 1000;
					
					//Console.Write ("{0,3}",255-Gray);
					
					Console.Write(a[(int)((Gray/255.0)*(a.Length-1))]);
					Console.Write(a[(int)((Gray/255.0)*(a.Length-1))]);
					
					/*if(pix[y,x] == Color.FromArgb(255,255,255,255))
					{
						Console.Write(a[0]);
					}
					
					else
					{
						Console.Write("#");
					}*/
				
					
				}
				Console.WriteLine();
			}

		}
		
		
	}
		
		
		/*
		static void Main(string[] args){

			
			
			
			fs.Close();
			int imgW = bmpdata[0x12];
			int imgH = bmpdata[0x16];
			//Console.WriteLine( String.Format("w = {0},h = {1}", bmpdata[0x12],bmpdata[0x16]));
			byte[] colorData = new byte[bmpdata.Length - 0x36];
			Color [,] pix = new Color[imgH,imgW+1];
			
			
			Console.Write(imgW.ToString() +" "+ imgH.ToString()+"\n");
			Console.WriteLine (String.Format("{0}    {1}",bmpdata.Length,colorData.Length));
			for (int i = 0;i<54;i++)
			{
				Console.Write (bmpdata[i] + " " );
			}
			
			
			
			int x = 0,y = imgH-1;
			for (int i = 0;i<colorData.Length;i++)
			{
				//Console.WriteLine(i);
				colorData[i] = bmpdata[i+0x36];
			}
			Console.WriteLine("l = " + colorData.Length);
			
			for (int i = 0;i<colorData.Length;i++)
			{
				Console.Write (String.Format("{0,3} ",colorData[i]));
			}
			
			Console.WriteLine();
			int n = 1;
			for (int i = 0;i<colorData.Length-2;i+=3)
			{
				pix[y,x]= Color.FromArgb(colorData[i],colorData[i+1],colorData[i+2]);
				//Console.WriteLine(n + "ok"+ x +","+ y);
				Console.Write ( String.Format("|{0,3},{1,3},{2,3}|", colorData[i],colorData[i+1],colorData[i+2]));
				
				x  = (x+1)%(imgW);

				if((n) % (imgW) == 0)
				{
					//Console.WriteLine();
					y --;
					if(y<0)
						break;
				}
				n ++;
					//Console.WriteLine();	

			}
			
			Console.WriteLine("odfk");
			
			Console.WriteLine(n);
			Console.WriteLine(colorData.Length);
			
			
			//Console.Write("\n123456789|123456789|123456789|123456789|123456789|\n123456789|123456789|123456789|123456789|123456789|\n");
			for (int i = 0; i < pix.GetLength(0); i++)//y
			{
				for (int j = 0; j < pix.GetLength(1)-1; j++)//x
				{
					//Console.Write (String.Format("|{0,35}",pix[i,j].ToString()));
					
					if(pix[i,j] == Color.FromArgb(255,255,255,255))
					{
						Console.Write(".");
					}
					else
					{
						Console.Write("#");
					}
				
					
				}
				Console.WriteLine();
			}
			
			//Console.Write ( String.Format("|{0,35}",Color.Black.ToString()));
			
			
		}
	}*/
	
}