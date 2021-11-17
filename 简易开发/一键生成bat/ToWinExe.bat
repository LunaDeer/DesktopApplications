@echo off
::C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe *.cs /win32icon:icon.ico 
::C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /t:exe  /out:a.exe  *.cs   /win32icon:icon.ico 



::C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /t:exe  /out:main.exe  *.cs   /win32icon:icon.ico 
C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /t:winexe  /out:main.exe  *.cs   /win32icon:icon.ico 


main.exe

echo +-------------------------------------------------------+
echo -                                                       -
echo - 结束,如果没有错误,该文件夹下应该生成了 Program.exe !  -
echo -                         简易c#编译器   遛狗的猫小萌   -
echo +-------------------------------------------------------+
pause