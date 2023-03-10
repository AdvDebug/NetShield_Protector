using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetShield_Protector
{
    internal class Classes
    {
        internal class AntiDebug
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool IsDebuggerPresent();

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool CloseHandle(IntPtr Handle);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool CheckRemoteDebuggerPresent(IntPtr Handle, ref bool IsPresent);
            public static void AntiDebugCheck()
            {
                bool IsPresent = false;
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref IsPresent);
                if (IsDebuggerPresent() || Debugger.IsAttached || IsPresent)
                {
                    Environment.Exit(0);
                }
                try
                {
                    CloseHandle((IntPtr)0x1231);
                }
                catch
                {
                    Environment.Exit(0);
                }
            }
        }

        internal class AntiVM
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr GetModuleHandle(string lib);

            public static bool IsEmulated()
            {
                long Tick = Environment.TickCount;
                Thread.Sleep(500);
                long Tick2 = Environment.TickCount;
                if (((Tick2 - Tick) < 500L))
                {
                    return true;
                }
                return false;
            }

            public static bool IsModulePresent(string lib)
            {
                if (GetModuleHandle(lib) != IntPtr.Zero )
                    return true;
                return false;
            }

            public static bool CheckForVMwareAndVirtualBox()
            {
                using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
                {
                    using (ManagementObjectCollection ObjectItems = ObjectSearcher.Get())
                    {
                        foreach (ManagementBaseObject Item in ObjectItems)
                        {
                            string ManufacturerString = Item["Manufacturer"].ToString().ToLower();
                            string ModelName = Item["Model"].ToString();
                            if ((ManufacturerString == "microsoft corporation" && ModelName.ToUpperInvariant().Contains("VIRTUAL") || ManufacturerString.Contains("vmware")))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            public static void AntiVMCheck()
            {
                string[] BlacklistModules = { "SbieDll.dll", "cmdvrt32.dll", "SxIn.dll", "cuckoomon.dll" };
                for (int i = 0; i < BlacklistModules.Length; i++)
                {
                    if (IsModulePresent(BlacklistModules[i]))
                    {
                        Environment.Exit(0);
                    }
                }
                if (CheckForVMwareAndVirtualBox() || IsEmulated())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
