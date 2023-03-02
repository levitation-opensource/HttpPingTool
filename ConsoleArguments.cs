
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


using System.Collections.Generic;
using System.Diagnostics;

namespace HttpPingTool
{
    partial class Program
    {
        private const string ArgShowHelp = "-help";
        private const string ArgOutageTimeBeforeGiveUpSeconds = "-outageTimeBeforeGiveUpSeconds";
        private const string ArgOutageConditionNumPings = "-outageConditionNumPings";
        private const string ArgPassedPingIntervalMs = "-passedPingIntervalMs";
        private const string ArgFailedPingIntervalMs = "-failedPingIntervalMs";
        private const string ArgPingTimeoutMs = "-pingTimeoutMs";
        private const string ArgHost = "-url";
        //private const string ArgSourceIP = "-sourceIP";   //TODO
        private const string ArgSuccessReplyBodyRegEx = "-successReplyBodyRegEx";

        //TODO: target host IP: http://stackoverflow.com/questions/359041/request-web-page-in-c-sharp-spoofing-the-host and http://www.codeproject.com/Articles/6554/How-to-use-HttpWebRequest-and-HttpWebResponse-in-N
        //TODO: argument to print only first/last line of response
        //TODO: get/post method selection

#if DEBUG && false
        private static bool ValueShowHelp = false;
        private static int ValueOutageTimeBeforeGiveUpSeconds = 18;
        private static int ValueOutageConditionNumPings = 3;
        private static int ValuePassedPingIntervalMs = 1500;
        private static int ValueFailedPingIntervalMs = 500;
        private static int ValuePingTimeoutMs = 1000;
        private static List<string> ValueHosts = new List<string>();
        private static List<string> ValueSourceHosts = new List<string>();
#else
        private static bool ValueShowHelp = false;
        private static int ValueOutageTimeBeforeGiveUpSeconds = 180;
        private static int ValueOutageConditionNumPings = 3;
        private static int ValuePassedPingIntervalMs = 15000;
        private static int ValueFailedPingIntervalMs = 5000;
        private static int ValuePingTimeoutMs = 10000;
        private static List<string> ValueHosts = new List<string>();    //TODO: use hashset instead
        //private static List<string> ValueSourceIPs = new List<string>();    //TODO: use hashset instead
        private static List<string> ValueSuccessReplyBodyRegExes = new List<string>();
#endif

    }
}