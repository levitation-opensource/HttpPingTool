@echo off
:s
HttpPingTool.exe -url="http://www.domain.com/uptimetest1" -url="http://www.domain.com/uptimetest2" -successReplyBodyRegEx="</html>\s+$"
net stop YourProblematicServerService
net start YourProblematicServerService
sleep 15
goto s
