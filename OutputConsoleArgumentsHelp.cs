
#region Copyright (c) 2012, Roland Pihlakas
/////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2012, Roland Pihlakas.
//
// Permission to copy, use, modify, sell and distribute this software
// is granted provided this copyright notice appears in all copies.
//
/////////////////////////////////////////////////////////////////////////////////////////
#endregion Copyright (c) 2012, Roland Pihlakas


using System;
using System.Diagnostics;

namespace HttpPingTool
{
    partial class Program
    {

        static void OutputConsoleArgumentsHelp()
        {
            Console.WriteLine("\n");
            Console.WriteLine("A program to monitor a number of hosts using HTTP GET requests.");
            Console.WriteLine("The program exits when all monitored hosts have an outage.");
            Console.WriteLine("Program arguments and their default values:");
            Console.WriteLine("\n");

            Console.WriteLine(string.Format("{0} ({1})", ArgShowHelp, "Shows help text"));

            OutputConsoleArgumentHelp(ArgOutageTimeBeforeGiveUpSeconds, ValueOutageTimeBeforeGiveUpSeconds, "How long outage should last before trigger is activated and HttpPingTool quits. NB! This timeout starts only after the failure count specified with " + ArgOutageConditionNumPings + " has been exceeded.");
            OutputConsoleArgumentHelp(ArgOutageConditionNumPings, ValueOutageConditionNumPings, "How many requests should fail before outage can be declared");
            OutputConsoleArgumentHelp(ArgPassedPingIntervalMs, ValuePassedPingIntervalMs, "How many ms to pause after a successful request");
            OutputConsoleArgumentHelp(ArgFailedPingIntervalMs, ValueFailedPingIntervalMs, "How many ms to pause after a failed request");
            OutputConsoleArgumentHelp(ArgPingTimeoutMs, ValuePingTimeoutMs, "Request timeout");
            OutputConsoleArgumentHelp(ArgHost, "\"http url\"", "Multiple target http urls can be specified. In this case the program exits only after all monitored urls have failed.");
            //OutputParamHelp(ArgSourceIP, "source IP", "Source IP. Multiple source IP-s can be specified. If multiple names are specified then each target host is requested through corresponding source IP (interface). If no source IP is specified for some target host then the request path is choosen by the system.");
            OutputConsoleArgumentHelp(ArgSuccessReplyBodyRegEx, "\"regular expression\"", "Regular expression describing expected content of successful reply. If the content does not match the expected content then the reply is considered as failure. If the parameter is not specified then any content matches when reply is of kind HTTP 200 OK.");

            Console.WriteLine("\n");

        }   //static void OutputHelp()

        // ############################################################################

        public static void OutputConsoleArgumentHelp<T>(string name, T defaultValue, string description) 
        {
            Console.WriteLine(string.Format("{0}={1} ({2})", name, defaultValue, description));
        }

        // ############################################################################

    }
}
