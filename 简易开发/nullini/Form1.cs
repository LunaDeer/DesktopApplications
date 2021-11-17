using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing;
using System;
using System.Threading;
namespace lisonFunc
{
    public partial class Form1 : Form
    {
		bool canSee = true;
		//IniFile ini = new IniFile();
		
        public Form1()
        {
			//没有标题
			//this.FormBorderStyle = FormBorderStyle.None;
			//任务栏不显示
			//this.ShowInTaskbar = false;

			
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.L);
			HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.F11);
			HotKey.RegisterHotKey(Handle, 999, HotKey.KeyModifiers.Ctrl, Keys.F12);
			//Console.WriteLine(GetText());
            InitializeComponent();
        }
		private void saveData()
        {
            IniFile.Write("Location", "top", this.Top.ToString(), File.GetCurrentExePath() + "data.ini");
            IniFile.Write("Location", "left", this.Left.ToString(), File.GetCurrentExePath() + "data.ini");
            IniFile.Write("Set", "size", 9.ToString(), File.GetCurrentExePath() + "data.ini");
            IniFile.Write("Set", "text", textBox1.Text, File.GetCurrentExePath() + "data.ini");
            IniFile.Write("Set", "path", File.GetCurrentExePath(), File.GetCurrentExePath() + "data.ini");
        }
		private void Form1_Load(object sender, EventArgs e)
        {
			//timer1.Start();
			//this.Location = new Point(-2000, -2000);
			
			
			
			 //如果读取失败直接写入
            if (IniFile.Read("Location", "top", "-999", File.GetCurrentExePath() + "data.ini") == "-999")
            {
                IniFile.Write("Location", "top", this.Top.ToString(), File.GetCurrentExePath() + "data.ini");
                IniFile.Write("Location", "left", this.Left.ToString(), File.GetCurrentExePath() + "data.ini");
                IniFile.Write("Set", "size", 9.ToString(), File.GetCurrentExePath() + "data.ini");
                IniFile.Write("Set", "text", textBox1.Text, File.GetCurrentExePath() + "data.ini");
                IniFile.Write("Set", "path", File.GetCurrentExePath(), File.GetCurrentExePath() + "data.ini");
            }
            else
            {
                int top, left, size;
                if (int.TryParse(IniFile.Read("Location", "top", "-999", File.GetCurrentExePath() + "data.ini"), out top))
                {
                    this.Top = top;
                }
                if (int.TryParse(IniFile.Read("Location", "left", "-999", File.GetCurrentExePath() + "data.ini"), out left))
                {
                    this.Left = left;
                }
                if (int.TryParse(IniFile.Read("Set", "size", "-999", File.GetCurrentExePath() + "data.ini"), out size))
                {
                    textBox1.Font = new Font(textBox1.Font.FontFamily, size);
                }
                textBox1.Text = IniFile.Read("Set", "text", "-999", File.GetCurrentExePath() + "data.ini");
            }
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:     //按下的是Shift+S
                            //MessageBox.Show("最后编译时间2021年11月2日 10:32:51 遛狗就完了!");
							Console.WriteLine("----------");
								//new Thread(() => {MyKey.KeyPress(Keys.A,50);}).Start();
								new Thread(() => {
									Fun.aaaa();
								}).Start();
                            break;
						case 101:     
							Console.WriteLine(MyClipboard.GetText());
							MyClipboard.SetText("ggg");
							
							//按下的是Shift+S
                            //MessageBox.Show("最后编译时间2021年11月2日 10:32:51 遛狗就完了!");
							//Console.WriteLine("----------");
								//new Thread(() => {MyKey.KeyPress(Keys.A,50);}).Start();
								//new Thread(() => {
									//Fun.EnterNumber("asdfasg");
									//Console.WriteLine(GetText());
									
							this.Location = new Point(200, 200);
								//}).Start();
                            break;
							
						case 999:

							canSee = !canSee;
							SetVisibleCore(canSee);
                            break;
	
							
                    }
                    break;
            }
            base.WndProc(ref m);
        }
		
		private void label2_Click(object sender, EventArgs e)
        {
			saveData();
            MessageBox.Show("最后编译时间2021年11月2日 10:32:51 遛狗就完了!"+ MyClipboard.GetText());
        }

		//隐藏窗口
        /*protected   override   void  SetVisibleCore( bool  value)
		{
			base.SetVisibleCore( value );
		}*/

        private void FrmSale_Leave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKey.UnregisterHotKey(Handle, 100);
			HotKey.UnregisterHotKey(Handle, 101);
			HotKey.UnregisterHotKey(Handle, 999);
        }

		private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text += "1";
			MyClipboard.SetText(">_<");
			
        }

		private void button4_Click(object sender, EventArgs e)
        {
            
            //
            
            FolderBrowserDialog dilog = new FolderBrowserDialog();            
            dilog.Description = "请选择目录";            
            DialogResult result = dilog.ShowDialog();
            //if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dilog.SelectedPath))

            if (result ==  System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)   
            {                
                MessageBox.Show(dilog.SelectedPath) ;       
            }
            
            
            /*OpenFileDialog op = new OpenFileDialog();
            op.Filter = "word Files(*.doc)|*.doc|All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(op.FileName) ;   
            }*/  
        }
		
		private void Form1_Resize(object sender, EventArgs e)
        {
			
        }
		private void button1_Click(object sender, EventArgs e)
        {
			if(!timer1.Enabled)
			{
				timer1.Start();
				button1.Text = "Stop";
			}
			else
			{
				timer1.Stop();
				button1.Text = "Start";
			}
        }
        
		/*
		System.Environment.Exit(0);
		*/
		
		
		
    }
}
