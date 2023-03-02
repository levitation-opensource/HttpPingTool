## HTTP Ping monitor
A program to monitor a number of hosts using HTTP GET requests. The program exits when all monitored hosts have an outage.

## Use cases
If there is some server service that occasionally misbehaves and stops returning correct responses reliably then you can use Http Ping monitor tool to launch corrective actions upon detection of the occurrence of the problem. 

You can create a batch file which contains a loop and inside that loop two sets of commands: the first command starts the Http Ping monitor tool. If the first command quits then that means that the trigger situation has been detected and therefore it is appropriate time for the second set of commands to be executed. The second set of commands would contain some corrective action. For example, the second set of commands could:
	a) Trigger a restart of a related server process.
	b) Send an email to the administrator.

Example batch file content:
	@echo off
	:s
	HttpPingTool.exe -url="http://www.domain.com/uptimetest1" -url="http://www.domain.com/uptimetest2" -successReplyBodyRegEx="</html>\s+$"
	net stop YourProblematicServerService
	net start YourProblematicServerService
	sleep 15
	goto s

The above example sends GET requests to http://www.domain.com/uptimetest1 and http://www.domain.com/uptimetest2 URL addresses. Once requests to ALL of the specified target URL addresses start failing, the server service is restarted by the following commands in the batch file. The failure is specified as missing <html> tag at the end of the HTTP response. By default the trigger activates (that is, Http Ping monitor tool quits) when ALL of the target URL addresses fail to correctly respond to the request for 3 consequtive checks with 5 second intervals, and then the outage continues for another 30 seconds after that. If the monitored URL addresses respond to requests correctly during that additional time interval then the trigger is reset and Http Ping monitor tool continues running without quitting.

### State
Ready to use. Maintained.

### Program arguments and their default values
-help (Shows help text)
-outageTimeBeforeGiveUpSeconds=180 (How long outage should last before trigger is activated and HttpPingTool quits. NB! This timeout starts only after the failure count specified with -outageConditionNumPings has been exceeded.)
-outageConditionNumPings=3 (How many requests should fail before outage can be declared)
-passedPingIntervalMs=15000 (How many ms to pause after a successful request)
-failedPingIntervalMs=5000 (How many ms to pause after a failed request)
-pingTimeoutMs=10000 (Request timeout)
-url="http url" (Multiple target http urls can be specified. In this case the program exits only after ALL monitored urls have failed.)
-successReplyBodyRegEx="regular expression" (Regular expression describing expected content of successful reply. If the content does not match the expected content then the reply is considered as failure. If the parameter is not specified then any content matches when reply is of kind HTTP 200 OK.)


[![Analytics](https://ga-beacon.appspot.com/UA-351728-28/HttpPingTool/README.md?pixel)](https://github.com/igrigorik/ga-beacon)    
