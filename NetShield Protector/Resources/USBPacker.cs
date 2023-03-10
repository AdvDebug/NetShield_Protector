using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

USBPacker
{
    internal static class Program
    {
        private static string ProgramToDecrypt = "DecME";
        private static string IV = "THISISIV";

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
            byte[] inputBuffer = Convert.FromBase64String(DataToDecrypt);
            using (SHA256CryptoServiceProvider cryptoServiceProvider1 = new SHA256CryptoServiceProvider())
            {
                byte[] hash = cryptoServiceProvider1.ComputeHash(Encoding.UTF8.GetBytes(KeyToDecryptWith));
                AesCryptoServiceProvider cryptoServiceProvider2 = new AesCryptoServiceProvider();
                cryptoServiceProvider2.Key = hash;
                cryptoServiceProvider2.Mode = CipherMode.CBC;
                cryptoServiceProvider2.Padding = PaddingMode.PKCS7;
                using (AesCryptoServiceProvider cryptoServiceProvider3 = cryptoServiceProvider2)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(IVKeyToDecryptWith);
                    cryptoServiceProvider3.IV = bytes;
                    return Encoding.UTF8.GetString(cryptoServiceProvider3.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
                }
            }
        }

        private static string HashingHardwareID(string ToHash)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("bAI!J6XwWO&A");
            HMACSHA256 hmacshA256 = new HMACSHA256();
            hmacshA256.Key = bytes;
            byte[] hash = hmacshA256.ComputeHash(Encoding.UTF8.GetBytes(ToHash));
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x2"));
            return stringBuilder.ToString();
        }

        private static string GetUSBHardwareID()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    foreach (ManagementObject managementObject in new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive").Get())
                    {
                        if (managementObject["MediaType"].ToString() == "Removable Media")
                            return Program.HashingHardwareID(drive.TotalSize.ToString() + managementObject["SerialNumber"].ToString() + managementObject["PNPDeviceID"].ToString());
                    }
                }
            }
            return (string)null;
        }

        private static bool CloseHandleAntiDebug()
        {
            try
            {
                Program.CloseHandle((IntPtr)3735929054L);
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
        private static void Main()
        {
            try
            {
                bool IsPresent = false;
                Program.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref IsPresent);
                if (Debugger.IsAttached || Program.IsDebuggerPresent() || IsPresent || Program.CloseHandleAntiDebug())
                {
                    Environment.Exit(0);
                }
                else
                {
                    IntPtr moduleHandle = Program.GetModuleHandle("ntdll.dll");
                    IntPtr procAddress1 = Program.GetProcAddress(moduleHandle, "DbgUiRemoteBreakin");
                    IntPtr procAddress2 = Program.GetProcAddress(moduleHandle, "DbgUiConnectToDbg");
                    byte[] Buffer = new byte[1] { (byte)204 };
                    Program.WriteProcessMemory(Process.GetCurrentProcess().Handle, procAddress1, Buffer, 6U, 0);
                    Program.WriteProcessMemory(Process.GetCurrentProcess().Handle, procAddress2, Buffer, 6U, 0);
                    string USBHWID = GetUSBHardwareID();
                    StringBuilder DecryptEncryptionKey = new StringBuilder();
                    for (int c = 0; c < USBHWID.ToString().Length; c++)
                        DecryptEncryptionKey.Append((char)((uint)USBHWID[c] ^ (uint)Convert.FromBase64String("SOS12")[c % 4]));
                    Assembly.Load(Convert.FromBase64String(Program.TqMIJUcgsXjVgxqJ(Program.ProgramToDecrypt, Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(DecryptEncryptionKey.ToString())), Program.IV))).EntryPoint.Invoke((object)null, (object[])null);
                }
            }
            catch
            {
                MessageBox.Show("Sorry But looks like you are not authorized to use this program.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
            }
        }
    }
}