@echo off
:s
HttpPingTool.exe -url="http://www.domain.com/uptimetest1" -url="http://www.domain.com/uptimetest2" -successReplyBodyRegEx="</html>\s+$"
net stop YourProblematicServerService
net start YourProblematicServerService
ping -n 16 127.0.0.1
REM sleep 15
goto s
