@echo off
::C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe *.cs /win32icon:icon.ico 
::C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /t:exe  /out:a.exe  *.cs   /win32icon:icon.ico 



C:\windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /t:exe  /out:main.exe  *.cs   /win32icon:icon.ico 


echo +-------------------------------------------------------+
echo -                                                       -
echo - ����,���û�д���,���ļ�����Ӧ�������� Program.exe !  -
echo -                         ����c#������   �޹���èС��   -
echo +-------------------------------------------------------+

pause
cls
main.exe
