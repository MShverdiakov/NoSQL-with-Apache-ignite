
cls

Del *.exe
Del *.config

@If "%ProgramW6432%" NEQ "" goto 64-bit

@Echo "32-bit"
@SET CSC="C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" 
goto compile

:64-bit
@Echo "64-bit"
@SET CSC="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe"

:compile

%CSC% /t:exe /out:Shverdiakov.exe .\CS\Shverdiakov.cs /r:.\dll\shverdiakov\Apache.Ignite.Core.dll;C:\Windows\Microsoft.NET\assembly\GAC_MSIL\netstandard\v4.0_2.0.0.0__cc7b13ffcd2ddd51\netstandard.dll


@copy .\CS\Shverdiakov.config Shverdiakov.exe.config


@pause>nul
