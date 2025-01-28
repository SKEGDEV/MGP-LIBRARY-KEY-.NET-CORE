using System.Security.Cryptography;
using System.Text;

namespace KeyLibraryMGP
{
    public class CryptoLibrary
    {
        /// <summary>
        /// Encript text most used for systems keys as DB conection string, api keys, etc...
        /// </summary>
        /// <param name="TextPlain">text that can encrypt</param>
        /// <returns>text plain encripted based in the entry text plain</returns>
        /// <exception cref="Exception">Thrown if something of the process is wrong</exception>
        /// <example>
        /// <code>
        /// CryptoKey crypto = new CryptoKey();
        /// string encryptedKey = crypto.Encrypt("hello world");
        /// Console.WriteLine(encryptedKey); // Output: "qHRYdTQf4rxaRHydExeNhw=="
        /// </code>
        /// </example>
        public string Encript(string TextPlain)
        { 
            try
            {
                string result = string.Empty;
                string secretKey = "santhosh";
                string publicKey = "engineer";
                byte[] secretByte = { };
                secretByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
                byte[] publicByte = { };
                publicByte = System.Text.Encoding.UTF8.GetBytes(publicKey);
                byte[] inputByte = System.Text.Encoding.UTF8.GetBytes(TextPlain);
                MemoryStream ms = null;
                CryptoStream cs = null;
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publicByte, secretByte), CryptoStreamMode.Write);
                    cs.Write(inputByte, 0, inputByte.Length);
                    cs.FlushFinalBlock();
                    result = Convert.ToBase64String(ms.ToArray());
                }
                return result;
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
        /// <summary>
        /// Encript text most used for systems keys as DB conection string, api keys, etc...
        /// </summary>
        /// <param name="EncriptedText">text that can decrypt</param>
        /// <returns>text plain based in encripted text entry</returns>
        /// <exception cref="Exception">Thrown if something of the process is wrong</exception>
        /// <example>
        /// <code>
        /// CryptoKey crypto = new CryptoKey();
        /// string DencryptedKey = crypto.Decrypt("qHRYdTQf4rxaRHydExeNhw==");
        /// Console.WriteLine(DencryptedKey); // Output: "hello world"
        /// </code>
        /// </example>
        public string Decrypt(string EncriptedText)
        {
            try
            {
                string result = string.Empty;
                string secretKey = "santhosh";
                string publicKey = "engineer";
                byte[] secretByte = { };
                secretByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
                byte[] publicByte = { };
                publicByte = System.Text.Encoding.UTF8.GetBytes(publicKey);
                byte[] inputByte = new byte[EncriptedText.Replace(" ", "+").Length];
                inputByte = Convert.FromBase64String(EncriptedText.Replace(" ", "+"));
                MemoryStream ms = null;
                CryptoStream cs = null;
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publicByte, secretByte), CryptoStreamMode.Write);
                    cs.Write(inputByte, 0, inputByte.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    result = encoding.GetString(ms.ToArray());
                }
                return result;
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
    }
}
