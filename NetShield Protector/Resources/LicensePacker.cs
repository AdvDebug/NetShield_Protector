using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

LicensePacker
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
                    if (!File.Exists(Environment.CurrentDirectory + @"\SOS13"))
                    {
                        MessageBox.Show("Please Make a SOS13 file in the current program directory and enter the program license to it to continue.", "License Not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        IntPtr NtdllModule = GetModuleHandle("ntdll.dll");
                        IntPtr DbgUiRemoteBreakinAddress = GetProcAddress(NtdllModule, "DbgUiRemoteBreakin");
                        IntPtr DbgUiConnectToDbgAddress = GetProcAddress(NtdllModule, "DbgUiConnectToDbg");
                        byte[] Int3InvaildCode = { 0xCC };
                        WriteProcessMemory(Process.GetCurrentProcess().Handle, DbgUiRemoteBreakinAddress, Int3InvaildCode, 6, 0);
                        WriteProcessMemory(Process.GetCurrentProcess().Handle, DbgUiConnectToDbgAddress, Int3InvaildCode, 6, 0);
                        string License = File.ReadAllText(Environment.CurrentDirectory + @"\SOS13");
                        if (string.IsNullOrEmpty(License))
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            StringBuilder NewLicense = new StringBuilder();
                            for (int c = 0; c < License.Length; c++)
                                NewLicense.Append((char)((uint)License[c] ^ (uint)Convert.FromBase64String("decryptkeyencryption")[c % 4]));
                            StringBuilder ROT13Encoding = new StringBuilder();
                            Regex regex = new Regex("[A-Za-z]");
                            foreach (char KSXZ in NewLicense.ToString())
                            {
                                if (regex.IsMatch(KSXZ.ToString()))
                                {
                                    int C = ((KSXZ & 223) - 52) % 26 + (KSXZ & 32) + 65;
                                    ROT13Encoding.Append((char)C);
                                }
                            }
                            StringBuilder sb = new StringBuilder(); foreach (char c in ROT13Encoding.ToString().ToCharArray()) { sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')); }
                            var GetTextToHEX = Encoding.Unicode.GetBytes(sb.ToString());
                            var BuildHEX = new StringBuilder();
                            foreach (var FinalHEX in GetTextToHEX)
                            {
                                BuildHEX.Append(FinalHEX.ToString("X2"));
                            }
                            string HashedKey = UTF8Encoding.UTF8.GetString(MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(BuildHEX.ToString())));
                            HMACMD5 HMACMD = new HMACMD5();
                            HMACMD.Key = UTF8Encoding.UTF8.GetBytes("LXSO12");
                            string HashedKey2 = UTF8Encoding.UTF8.GetString(HMACMD.ComputeHash(UTF8Encoding.UTF8.GetBytes(HashedKey)));
                            string DecryptedProgram = TqMIJUcgsXjVgxqJ(ProgramToDecrypt, HashedKey2.ToString(), IV);
                            byte[] ProgramToRun = Convert.FromBase64String(DecryptedProgram);
                            Assembly RunInMemory = Assembly.Load(ProgramToRun);
                            RunInMemory.EntryPoint.Invoke(null, null);
                        }
                    }
                }
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Sorry, but looks like your license key are invalid.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}