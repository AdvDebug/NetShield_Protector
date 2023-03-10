using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetShield_Protector
{
    public class Encryption
    {
        public static string AesTextEncryption(string DataToEncrypt, string KeyToEncryptWith, string IVKey)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(DataToEncrypt);
            using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
            {
                string initVector = IVKey;
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] keys = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(KeyToEncryptWith));
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                {
                    AES.IV = initVectorBytes;
                    ICryptoTransform transform = AES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    string Result = Convert.ToBase64String(results, 0, results.Length);
                    return Result;
                }
            }
        }
    }
}
