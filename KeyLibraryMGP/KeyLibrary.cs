using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyLibraryMGP
{
    public class KeyLibrary : CryptoLibrary
    {
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
        public string getKey(string keyName)
        {
            RegistryKey machineKeys = null;
            string result = string.Empty;
            try
            {
                machineKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MGP");
                result = machineKeys.GetValue(keyName).ToString();
                return this.Decrypt(result);
            }
            catch (Exception)
            {
                try
                {
                    machineKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\MGP");
                    result = machineKeys.GetValue(keyName).ToString();
                    return this.Decrypt(result);
                }
                catch (Exception e) { return $"Error: {e.Message}"; }
            }
            finally
            {
                machineKeys.Close();
            }
        }
    }
}
