cd Server
SET DOTNET=C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
SET PATH=%DOTNET%
csc.exe /win32icon:runuo.ico /r:Ultima.dll /debug /nowarn:0618 /nologo /out:..\RunUOsvnServer.exe /unsafe /recurse:*.cs
PAUSE
cd ..
title RunUO Server - RunUO 2.0 SVN 642+ML+SA - by Thilgon
echo off
cls
RunUOsvnServer.exe