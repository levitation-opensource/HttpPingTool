
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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security;

namespace HttpPingTool
{
    [SuppressUnmanagedCodeSecurity]     //SuppressUnmanagedCodeSecurity - For methods in this particular class, execution time is often critical. Security can be traded for additional speed by applying the SuppressUnmanagedCodeSecurity attribute to the method declaration. This will prevent the runtime from doing a security stack walk at runtime. - MSDN: Generally, whenever managed code calls into unmanaged code (by PInvoke or COM interop into native code), there is a demand for the UnmanagedCode permission to ensure all callers have the necessary permission to allow this. By applying this explicit attribute, developers can suppress the demand at run time. The developer must take responsibility for assuring that the transition into unmanaged code is sufficiently protected by other means. The demand for the UnmanagedCode permission will still occur at link time. For example, if function A calls function B and function B is marked with SuppressUnmanagedCodeSecurityAttribute, function A will be checked for unmanaged code permission during just-in-time compilation, but not subsequently during run time.
    public static partial class NativeMethods
    {
        public static readonly Version ServicePackVersion = GetServicePackVersion();

        public static readonly bool IsVistaOrServer2008OrNewer = Environment.OSVersion.Version.Major >= 6;
        public static readonly bool IsWin7OrNewer = (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major > 6;	//http://msdn.microsoft.com/en-us/library/ms724834(v=vs.85).aspx
        public static readonly bool IsServer2003OrVistaOrNewer = (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 2) || Environment.OSVersion.Version.Major > 5;
        public static readonly bool IsXPSP3OrServer2003OrVistaOrNewer = IsServer2003OrVistaOrNewer || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1 && ServicePackVersion.Major >= 3);

        // ############################################################################

        //http://stackoverflow.com/questions/2819934/detect-windows-7-in-net
        //http://stackoverflow.com/a/8406674/193017

        #region OSVERSIONINFOEX
        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        #endregion OSVERSIONINFOEX


        [DllImport("kernel32.dll")]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);

        public static Version GetServicePackVersion()
        {
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

            if (GetVersionEx(ref osVersionInfo))
            {
                Version result = new Version(osVersionInfo.wServicePackMajor, osVersionInfo.wServicePackMinor);
                return result;
            }
            else
            {
                return null;
            }
        }

        // ############################################################################

        private enum PROCESSINFOCLASS : int
        {
            ProcessBasicInformation = 0, // 0, q: PROCESS_BASIC_INFORMATION, PROCESS_EXTENDED_BASIC_INFORMATION
            ProcessQuotaLimits, // qs: QUOTA_LIMITS, QUOTA_LIMITS_EX
            ProcessIoCounters, // q: IO_COUNTERS
            ProcessVmCounters, // q: VM_COUNTERS, VM_COUNTERS_EX
            ProcessTimes, // q: KERNEL_USER_TIMES
            ProcessBasePriority, // s: KPRIORITY
            ProcessRaisePriority, // s: ULONG
            ProcessDebugPort, // q: HANDLE
            ProcessExceptionPort, // s: HANDLE
            ProcessAccessToken, // s: PROCESS_ACCESS_TOKEN
            ProcessLdtInformation, // 10
            ProcessLdtSize,
            ProcessDefaultHardErrorMode, // qs: ULONG
            ProcessIoPortHandlers, // (kernel-mode only)
            ProcessPooledUsageAndLimits, // q: POOLED_USAGE_AND_LIMITS
            ProcessWorkingSetWatch, // q: PROCESS_WS_WATCH_INFORMATION[]; s: void
            ProcessUserModeIOPL,
            ProcessEnableAlignmentFaultFixup, // s: BOOLEAN
            ProcessPriorityClass, // qs: PROCESS_PRIORITY_CLASS
            ProcessWx86Information,
            ProcessHandleCount, // 20, q: ULONG, PROCESS_HANDLE_INFORMATION
            ProcessAffinityMask, // s: KAFFINITY
            ProcessPriorityBoost, // qs: ULONG
            ProcessDeviceMap, // qs: PROCESS_DEVICEMAP_INFORMATION, PROCESS_DEVICEMAP_INFORMATION_EX
            ProcessSessionInformation, // q: PROCESS_SESSION_INFORMATION
            ProcessForegroundInformation, // s: PROCESS_FOREGROUND_BACKGROUND
            ProcessWow64Information, // q: ULONG_PTR
            ProcessImageFileName, // q: UNICODE_STRING
            ProcessLUIDDeviceMapsEnabled, // q: ULONG
            ProcessBreakOnTermination, // qs: ULONG
            ProcessDebugObjectHandle, // 30, q: HANDLE
            ProcessDebugFlags, // qs: ULONG
            ProcessHandleTracing, // q: PROCESS_HANDLE_TRACING_QUERY; s: size 0 disables, otherwise enables
            ProcessIoPriority, // qs: ULONG
            ProcessExecuteFlags, // qs: ULONG
            ProcessResourceManagement,
            ProcessCookie, // q: ULONG
            ProcessImageInformation, // q: SECTION_IMAGE_INFORMATION
            ProcessCycleTime, // q: PROCESS_CYCLE_TIME_INFORMATION
            ProcessPagePriority, // q: ULONG
            ProcessInstrumentationCallback, // 40
            ProcessThreadStackAllocation, // s: PROCESS_STACK_ALLOCATION_INFORMATION, PROCESS_STACK_ALLOCATION_INFORMATION_EX
            ProcessWorkingSetWatchEx, // q: PROCESS_WS_WATCH_INFORMATION_EX[]
            ProcessImageFileNameWin32, // q: UNICODE_STRING
            ProcessImageFileMapping, // q: HANDLE (input)
            ProcessAffinityUpdateMode, // qs: PROCESS_AFFINITY_UPDATE_MODE
            ProcessMemoryAllocationMode, // qs: PROCESS_MEMORY_ALLOCATION_MODE
            ProcessGroupInformation, // q: USHORT[]
            ProcessTokenVirtualizationEnabled, // s: ULONG
            ProcessConsoleHostProcess, // q: ULONG_PTR
            ProcessWindowInformation, // 50, q: PROCESS_WINDOW_INFORMATION
            ProcessHandleInformation, // q: PROCESS_HANDLE_SNAPSHOT_INFORMATION // since WIN8
            ProcessMitigationPolicy, // s: PROCESS_MITIGATION_POLICY_INFORMATION
            ProcessDynamicFunctionTableInformation,
            ProcessHandleCheckingMode,
            ProcessKeepAliveCount, // q: PROCESS_KEEPALIVE_COUNT_INFORMATION
            ProcessRevokeFileHandles, // s: PROCESS_REVOKE_FILE_HANDLES_INFORMATION
            MaxProcessInfoClass
        };

        //http://www.pinvoke.net/default.aspx/ntdll.ntqueryinformationprocess
        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtQueryInformationProcess(IntPtr processHandle,
           PROCESSINFOCLASS processInformationClass, IntPtr processInformation,
            uint processInformationLength, IntPtr returnLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtQueryInformationProcess(IntPtr processHandle,
           PROCESSINFOCLASS processInformationClass, out int processInformation,
            uint processInformationLength, IntPtr returnLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetInformationProcess(IntPtr processHandle,
           PROCESSINFOCLASS processInformationClass, IntPtr processInformation,
            uint processInformationLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetInformationProcess(IntPtr processHandle,
           PROCESSINFOCLASS processInformationClass, ref int processInformation,
            uint processInformationLength);

        public enum PROCESSIOPRIORITY : int
        {
            PROCESSIOPRIORITY_UNKNOWN = -1,

            PROCESSIOPRIORITY_VERY_LOW = 0,
            PROCESSIOPRIORITY_LOW,
            PROCESSIOPRIORITY_NORMAL,
            PROCESSIOPRIORITY_HIGH
        };

        public static bool NT_SUCCESS(int Status)
        {
            return (Status >= 0);
        }

        public static bool SetIOPriority(IntPtr processHandle, PROCESSIOPRIORITY ioPriority_in)
        {
            if (IsXPSP3OrServer2003OrVistaOrNewer)	//http://blogs.norman.com/2011/security-research/ntqueryinformationprocess-ntsetinformationprocess-cheat-sheet
            {
                int ioPriority = (int)ioPriority_in;
                int result = NtSetInformationProcess(processHandle, PROCESSINFOCLASS.ProcessIoPriority, ref ioPriority, sizeof(int));
                return NT_SUCCESS(result);
            }
            else
            {
                return false;
            }
        }

        public static PROCESSIOPRIORITY? GetIOPriority(IntPtr processHandle)
        {
            if (IsXPSP3OrServer2003OrVistaOrNewer)	//http://blogs.norman.com/2011/security-research/ntqueryinformationprocess-ntsetinformationprocess-cheat-sheet
            {
                int ioPriority;
                int result = NtQueryInformationProcess(processHandle, PROCESSINFOCLASS.ProcessIoPriority, out ioPriority, sizeof(int), IntPtr.Zero);
                if (NT_SUCCESS(result))
                    return (PROCESSIOPRIORITY)ioPriority;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public static bool SetPagePriority(IntPtr processHandle, int pagePriority_in)
        {
            if (IsVistaOrServer2008OrNewer)	//http://blogs.norman.com/2011/security-research/ntqueryinformationprocess-ntsetinformationprocess-cheat-sheet
            {
                int pagePriority = (int)pagePriority_in;
                int result = NtSetInformationProcess(processHandle, PROCESSINFOCLASS.ProcessPagePriority, ref pagePriority, sizeof(int));
                return NT_SUCCESS(result);
            }
            else
            {
                return false;
            }
        }

        public static int? GetPagePriority(IntPtr processHandle)
        {
            if (IsVistaOrServer2008OrNewer)	//http://blogs.norman.com/2011/security-research/ntqueryinformationprocess-ntsetinformationprocess-cheat-sheet
            {
                int pagePriority;
                int result = NtQueryInformationProcess(processHandle, PROCESSINFOCLASS.ProcessPagePriority, out pagePriority, sizeof(int), IntPtr.Zero);
                if (NT_SUCCESS(result))
                    return pagePriority;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        // ############################################################################

        [DllImport("kernel32.dll")]
        internal static extern bool SetProcessWorkingSetSize(IntPtr hProcess, IntPtr dwMinimumWorkingSetSize, IntPtr dwMaximumWorkingSetSize);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms682606(v=vs.85).aspx
        [/*DllImport("kernel32.dll")*/DllImport("psapi.dll")]
        public static extern bool EmptyWorkingSet(IntPtr processHandle);

        // ############################################################################
    }   //public partial class NativeMethods
}