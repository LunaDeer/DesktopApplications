using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lisonFunc
{
	static class File
	{
				
		/*
			public static void InitSound()
			{
				List<string> l = new List<string>();
				string selectedPath = @".\Sounds";
				string suffix = ".wav|.mp3";
				l = File.FindFileAtSuffix(selectedPath, suffix);

				for (int i = 0; i < l.Count; i++)
				{
					string fileName = System.IO.Path.GetFileNameWithoutExtension(l[i]);
					a += System.IO.Path.GetFileNameWithoutExtension(l[i]) + "\n";
					mciSendString("open " + l[i] + " alias " + fileName, null, 0, IntPtr.Zero);
					soundName.Add(fileName, i.ToString());
				}
			}
		*/
		
		/*
		 *  可以遍历文件的垃圾类,能用就行 很屌嗯!
		（1）. DirectoryInfo.GetFiles()：获取目录中（不包含子目录）的文件，返回类型为FileInfo[]，支持通配符查找；   
　　　　（2）. DirectoryInfo.GetDirectories()：获取目录（不包含子目录）的子目录，返回类型为DirectoryInfo[]，支持通配符查找；   
　　　　（3）. DirectoryInfo. GetFileSystemInfos()：获取指定目录下（不包含子目录）的文件和子目录，返回类型为FileSystemInfo[]，支持通配符查找；
		*/
		public static List<string> FindAllFile(string sSourcePath)
        {
            List<String> list = new List<string>();
			List<String> targetFileList = new List<string>();
			//遍历文件夹
			DirectoryInfo theFolder = new DirectoryInfo(sSourcePath);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
			foreach (FileInfo NextFile in thefileInfo)  //遍历文件
			{
				list.Add(NextFile.FullName);
			}
				
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                //list.Add(NextFolder.ToString());
                FileInfo[] fileInfo = NextFolder.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo NextFile in fileInfo)  //遍历文件
                    list.Add(NextFile.FullName);
            }
            return list;
        }
		public static List<string> FindFileAtSuffix(string sSourcePath,string suffix)
        {
			List<String> list = new List<string>();
			if (!Directory.Exists(sSourcePath))
				return list;
			string[] suffixList;
			suffixList = suffix.Split('|');
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(sSourcePath);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
			foreach (FileInfo NextFile in thefileInfo)  //遍历文件
			{
				for (int i=0;i<suffixList.Count();i++)
				{
					if(suffixList[i] == Path.GetExtension(NextFile.FullName))
						list.Add(NextFile.FullName);
				}
			}
				
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                //list.Add(NextFolder.ToString());
                FileInfo[] fileInfo = NextFolder.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo NextFile in fileInfo)  //遍历文件
				{
					for (int i=0;i<suffixList.Count();i++)
					{
						if(suffixList[i] == Path.GetExtension(NextFile.FullName))
							list.Add(NextFile.FullName);
					}
						
					
				}
            }
            return list;
        }
		
		

		public static void FindTargetInAFile(string path,string target,out string targetFile,out string remarks)
        {
			targetFile= "";
			remarks = "";
			int line = 1;
			int index = -1;
			bool found = false;
			string sStuName = string.Empty;
            FileStream f = new FileStream(path,FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamReader reader = new StreamReader(f);
			//StreamReader reader = new StreamReader(f, UnicodeEncoding.GetEncoding("GB2312"));
			f.Position = 0;
			while ((sStuName = reader.ReadLine()) != null)
			{
				//Console.WriteLine(sStuName);
				index = sStuName.IndexOf(target);
				if(index >=0)
				{
					if (found == false)
					{
						found = true;
						targetFile = path;
						//Console.WriteLine(path);
					}
					remarks = ("第" + line.ToString() + "行,第" + index.ToString() + "位置,该行为:")+ sStuName;
					//Console.WriteLine("第"+line.ToString()+"行,第"+index.ToString()+"位置,该行为:");
					//Console.WriteLine(sStuName);
				}
				line ++;
			}	
			
			
            // for (int i = 0; i <= 20; i++)
            // {
                // Console.Write(F.ReadByte() + " ");
            // }
			
            f.Close();
        }
		
		public static string GetCurrentPath()
		{
			return System.IO.Directory.GetCurrentDirectory();  
		}
		public static string GetCurrentFilePath()
		{
			return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
		}
		
		//.exe的目录
		public static string GetCurrentExePath()
		{
			return System.AppDomain.CurrentDomain.BaseDirectory;
		}
		//获取桌面
		public static string GetDesktopPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		
	
	}
	
	static class Tools22
	{
		public static void Printf<T>(T a)
		{
			Console.WriteLine(a);
		}
		
		public static void PrintList<T>(List<T> list)
		{
			for(int i=0;i<list.Count();i++)
			{ 
				Console.WriteLine(list[i].ToString());
			}
		}
		// public static void WindowSize(int w,int h)
		// {
			// Console.WindowWidth = w;
			// Console.WindowHeight = h;
			// Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
		// }
		public static void Row(string s = "")
		{
			Console.Write(s);
			for (int i = 0;i<Console.WindowWidth-s.Length;i++)
			{
				Console.Write("-");
			}
		}
	
	}
}




/*
 * 
 * 
 *
 *
 
 博客园Logo
首页
新闻
博问
专区
闪存
班级
代码改变世界
搜索
注册
登录
返回主页
爱笑的小宇宙
博客园
首页
新随笔
联系
订阅
管理
C# 之 获取文件名及拓展名
C# 之 获取文件名及拓展名
1、用Path类的方法（最常用）

string fullPath = @"\WebSite\Default.aspx";

string filename = System.IO.Path.GetFileName(fullPath);//带拓展名的文件名  “Default.aspx”
string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath);// 不带扩展名的文件名 “Default”
string dir = System.IO.Path.GetDirectoryName(fullPath);     //返回文件所在目录“\\WebSite”

 

2、用Substring截取

string aFirstName = fullPath.Substring(fullPath.LastIndexOf("\\") + 1, (fullPath.LastIndexOf(".") - fullPath.LastIndexOf("\\") - 1));  //不带拓展名的文件名“Default”
string aLastName = fullPath.Substring(fullPath.LastIndexOf(".") + 1, (fullPath.Length - fullPath.LastIndexOf(".") - 1));   //扩展名“aspx”


string strFileName = fullPath.Substring(fullPath.LastIndexOf("\\") + 1, fullPath.Length - 1 - fullPath.LastIndexOf("\\"));//带拓展名的文件名  “Default.aspx”
string strExtensionName = fullPath.Substring(fullPath.LastIndexOf("."), fullPath.Length - fullPath.LastIndexOf("."));//扩展名 “.aspx”

 

3、用openFileDialog.SafeFileName，取到该文件的所在目录路径
string path1 = System.IO.Path.GetDirectoryName(openFileDialog1.FileName) + @"\";

string path = Path.GetFileName("C:\My Document\path\image.jpg");    //只获取文件名image.jpg

4、进程相关

//获取当前进程的完整路径，包含文件名(进程名)。获取包含清单的已加载文件的路径或 UNC 位置。
string str0 = this.GetType().Assembly.Location;
//C:\Users\yiyi\AppData\Local\Temp\Temporary ASP.NET Files\web\bd33ba98\cbcc133a\App_Web_eidyl2kf.dll
//result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)

//获取新的 System.Diagnostics.Process 组件并将其与当前活动的进程关联
//获取关联进程主模块的完整路径，包含文件名(进程名)
string str1 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
//C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\10.0\WebDev.WebServer40.exe
//result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)

//获取或设置当前工作目录（即该进程从中启动的目录）的完全限定路径。
string str2 = System.Environment.CurrentDirectory;
//C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\10.0
//result: X:\xxx\xxx (.exe文件所在的目录)

//获取当前 System.Threading.Thread 的当前应用程序域的基目录，它由程序集冲突解决程序用来探测程序集。
string str3 = System.AppDomain.CurrentDomain.BaseDirectory;
//F:\Code\得利斯20150923\web\
//result: X:\xxx\xxx\ (.exe文件所在的目录+"\")

//获取或设置包含该应用程序的目录的名称。
string str4 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
//F:\Code\得利斯20150923\web\
//result: X:\xxx\xxx\ (.exe文件所在的目录+"\")

//获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
string str5 = System.Windows.Forms.Application.StartupPath;
//C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\10.0
//result: X:\xxx\xxx (.exe文件所在的目录)

//获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
string str6 = System.Windows.Forms.Application.ExecutablePath;
//C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\10.0\WebDev.WebServer40.exe
//result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)

//获取应用程序的当前工作目录(不可靠)。
string str7 = System.IO.Directory.GetCurrentDirectory();
//C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\10.0
//result: X:\xxx\xxx (.exe文件所在的目录)
Path.GetExtension(目录) 后缀
分类: C#
好文要顶 关注我 收藏该文  
爱笑的小宇宙
关注 - 43
粉丝 - 25
+加关注
00
« 上一篇： JSON详解
» 下一篇： 大话C#之委托
posted @ 2019-02-15 11:01  爱笑的小宇宙  阅读(4558)  评论(0)  编辑  收藏  举报
刷新评论刷新页面返回顶部
登录后才能查看或发表评论，立即 登录 或者 逛逛 博客园首页
【推荐】百度智能云2021普惠上云节：新用户首购云服务器低至0.7折
【推荐】阿里云云大使特惠：新用户购ECS服务器1核2G最低价87元/年
【推荐】大型组态、工控、仿真、CAD\GIS 50万行VC++源码免费下载!
【推荐】和开发者在一起：华为开发者社区，入驻博客园科技品牌专区
【推广】园子与爱卡汽车爱宝险合作，随手就可以买一份的百万医疗保险

编辑推荐：
· 奇思妙想 CSS 3D 动画 | 仅使用 CSS 能制作出多惊艳的动画？
· 一个测试工程师的成长复盘
· 何时使用领域驱动设计
· 技术从业者的未来（三）
· .NET 6 全新指标 System.Diagnostics.Metrics 介绍
最新新闻：
· 商汤科技港股IPO招股书披露：研发投入超出营收额，亏损面正收窄（2021-08-28 11:20）
· 微软开始推送Windows 64位版OneDrive同步客户端（2021-08-28 11:10）
· Steam退款策略遭滥用 独立游戏开发者宣布退坑（2021-08-28 11:00）
· Spotify不满苹果日前对开发商政策调整 称其反竞争行为未改（2021-08-28 09:45）
· 微软Surface Go 3平板电脑似乎已被Geekbench基准测试数据库曝光（2021-08-28 09:38）
» 更多新闻...
公告
昵称： 爱笑的小宇宙
园龄： 2年9个月
粉丝： 25
关注： 43
+加关注
<	2021年8月	>
日	一	二	三	四	五	六
1	2	3	4	5	6	7
8	9	10	11	12	13	14
15	16	17	18	19	20	21
22	23	24	25	26	27	28
29	30	31	1	2	3	4
5	6	7	8	9	10	11
搜索
 
 
随笔分类
BFC(2)
C#(46)
C#单元测试(1)
CSS(32)
ES6(11)
Flex(1)
Javascript(26)
jQuery(2)
Linux(1)
python(15)
python实例(7)
sql server(3)
SVN(2)
vs code工具(10)
vue(1)
更多
随笔档案
2019年8月(18)
2019年7月(53)
2019年6月(52)
2019年5月(36)
2019年4月(57)
2019年3月(6)
2019年2月(22)
最新评论
1. Re:深入理解css中的margin属性
非常清晰的文章，感谢博主

--始不垂翅
2. Re:css中设置table中的td内容自动换行
word-wrap/break 是用来设置怎么换行。问题是换行或不换行怎么设置？

--capital2012
3. Re:C#、.Net经典面试题目及答案
--農碼一生
4. Re:js es6遍历对象的6种方法（应用中推荐前三种）
如果在回调中都用箭头函数, 那逼格更高了

--杰克-李
5. Re:js es6遍历对象的6种方法（应用中推荐前三种）
ES6比相当牛逼,可以放弃插件库了

--杰克-李
阅读排行榜
1. js es6遍历对象的6种方法（应用中推荐前三种）(162367)
2. js Map对象的用法(84656)
3. HTML+CSS实现导航栏二级下拉菜单完整代码(48196)
4. CSS + ul li 横向排列的两种方法(30943)
5. JS中map（）与forEach（）的用法(24340)
评论排行榜
1. js es6遍历对象的6种方法（应用中推荐前三种）(3)
2. js Map对象的用法(2)
3. C#、.Net经典面试题目及答案(2)
4. js实现表单提交submit()，onsubmit(1)
5. css中设置table中的td内容自动换行(1)
推荐排行榜
1. js Map对象的用法(4)
2. js es6遍历对象的6种方法（应用中推荐前三种）(4)
3. VS Code插件Vue2 代码补全工具(2)
4. 解决VS Code开发Python3语言自动补全功能(2)
5. 大白话系列之C#委托与事件讲解(一)(2)
Copyright © 2021 爱笑的小宇宙
Powered by .NET 5.0 on Kubernetes
 
 
 
 
 
 
 
 
 
 */





