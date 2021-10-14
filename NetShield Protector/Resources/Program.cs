using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Management;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

HWIDPacker
{
    static class Program
    {
        static string ProgramToDecrypt = "DecME";
        static string IV = "THISISIV";

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool IsDebuggerPresent();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern void CheckRemoteDebuggerPresent(IntPtr Handle, ref bool IsPresent);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr Handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lib);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr Module, string Function);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr ProcHandle, IntPtr BaseAddress, byte[] Buffer, uint size, int NumOfBytes);
        private static string TqMIJUcgsXjVgxqJ(string DataToDecrypt, string KeyToDecryptWith, string IVKeyToDecryptWith)
        {
            byte[] data = Convert.FromBase64String(DataToDecrypt);
            using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
            {
                byte[] keys = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(KeyToDecryptWith));
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                {
                    string initVector = IVKeyToDecryptWith;
                    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                    AES.IV = initVectorBytes;
                    ICryptoTransform transform = AES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    string Result = UTF8Encoding.UTF8.GetString(results);
                    return Result;
                }
            }
        }

        private static string HashingHardwareID(string ToHash)
        {
            byte[] KeyToHashWith = Encoding.ASCII.GetBytes("bAI!J6XwWO&A");
            HMACSHA256 SHA256Hashing = new HMACSHA256();
            SHA256Hashing.Key = KeyToHashWith;
            var TheHash = SHA256Hashing.ComputeHash(UTF8Encoding.UTF8.GetBytes(ToHash));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < TheHash.Length; i++)
            {
                builder.Append(TheHash[i].ToString("x2"));
            }
            string FinalHash = builder.ToString();
            return FinalHash;
        }

        public static string GetHardwareID()
        {
            ManagementObjectSearcher CPU = new ManagementObjectSearcher(UTF8Encoding.UTF8.GetString(Convert.FromBase64String("U0VMRUNUICogRlJPTSBXaW4zMl9Qcm9jZXNzb3I=")));
            ManagementObjectCollection GetCPU = CPU.Get();
            string CPUID = null;
            foreach (ManagementObject CPUId in GetCPU)
            {
                CPUID = CPUId[UTF8Encoding.UTF8.GetString(Convert.FromBase64String("UHJvY2Vzc29yVHlwZQ=="))].ToString() + CPUId[UTF8Encoding.UTF8.GetString(Convert.FromBase64String("UHJvY2Vzc29ySWQ="))].ToString();
            }
            ManagementObjectSearcher BIOS = new ManagementObjectSearcher(UTF8Encoding.UTF8.GetString(Convert.FromBase64String("U0VMRUNUICogRlJPTSBXaW4zMl9CSU9T")));
            ManagementObjectCollection GetBIOS = BIOS.Get();
            string GPUID = null;
            foreach (ManagementObject BIOSId in GetBIOS)
            {
                GPUID = BIOSId["Manufacturer"].ToString() + BIOSId["Version"].ToString();
            }
            return HashingHardwareID(CPUID + GPUID);
        }

        private static bool CloseHandleAntiDebug()
        {
            try
            {
                CloseHandle((IntPtr)0xDEADC0DE);
                return false;
            }
            catch (Exception ex)
            {
                if (ex.Message == "External component has thrown an exception.")
                {
                    return true;
                }
            }
            return false;
        }

        [STAThread]
        static void Main()
        {
            try
            {
                bool IsPresent = false;
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref IsPresent);
                if (Debugger.IsAttached || IsDebuggerPresent() || IsPresent || CloseHandleAntiDebug())
                {
                    Environment.Exit(0);
                }
                else
                {
                    IntPtr NtdllModule = GetModuleHandle("ntdll.dll");
                    IntPtr DbgUiRemoteBreakinAddress = GetProcAddress(NtdllModule, "DbgUiRemoteBreakin");
                    IntPtr DbgUiConnectToDbgAddress = GetProcAddress(NtdllModule, "DbgUiConnectToDbg");
                    byte[] Int3InvaildCode = { 0xCC };
                    WriteProcessMemory(Process.GetCurrentProcess().Handle, DbgUiRemoteBreakinAddress, Int3InvaildCode, 6, 0);
                    WriteProcessMemory(Process.GetCurrentProcess().Handle, DbgUiConnectToDbgAddress, Int3InvaildCode, 6, 0);
                    string HWID = GetHardwareID();
                    StringBuilder DecryptEncryptionKey = new StringBuilder();
                    for (int c = 0; c < HWID.Length; c++)
                        DecryptEncryptionKey.Append((char)((uint)HWID[c] ^ (uint)Convert.FromBase64String("SOS12")[c % 4]));
                    StringBuilder ROT13Encoding = new StringBuilder();
                    Regex regex = new Regex("[A-Za-z]");
                    foreach (char KSXZ in DecryptEncryptionKey.ToString())
                    {
                        if (regex.IsMatch(KSXZ.ToString()))
                        {
                            int C = ((KSXZ & 223) - 52) % 26 + (KSXZ & 32) + 65;
                            ROT13Encoding.Append((char)C);
                        }
                    }
                    string HashedKey = UTF8Encoding.UTF8.GetString(MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(ROT13Encoding.ToString())));
                    var GetTextToHEX = Encoding.Unicode.GetBytes(HashedKey);
                    var BuildHEX = new StringBuilder();
                    foreach (var FinalHEX in GetTextToHEX)
                    {
                        BuildHEX.Append(FinalHEX.ToString("X2"));
                    }
                    StringBuilder sb = new StringBuilder(); foreach (char c in BuildHEX.ToString().ToCharArray()) { sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')); }
                    byte[] keys = MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(sb.ToString()));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        builder.Append(keys[i].ToString("x2"));
                    }
                    string DecryptedProgram = TqMIJUcgsXjVgxqJ(ProgramToDecrypt, builder.ToString(), IV);
                    byte[] ProgramToRun = Convert.FromBase64String(DecryptedProgram);
                    Assembly RunInMemory = Assembly.Load(ProgramToRun);
                    RunInMemory.EntryPoint.Invoke(null, null);
                }
            }
            catch(CryptographicException)
            {
                MessageBox.Show("Sorry But looks like you are not authorized to use this program.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}