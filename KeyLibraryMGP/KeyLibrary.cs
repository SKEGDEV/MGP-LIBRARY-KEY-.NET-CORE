using Microsoft.Win32;

namespace KeyLibraryMGP
{
    public class KeyLibrary
    {
        private CryptoLibrary crypto = null;
        public KeyLibrary() {
            crypto = new CryptoLibrary(getKey("publicKey", false), getKey("secretKey", false));
        }

        /// <summary>
        /// get keys from registry system key of Windows
        /// </summary>
        /// <param name="KeyName">The name in registry of the key</param>
        /// <param name="decryptKey">Indicates if the key should or not decrypted</param>
        /// <returns>the key from registry in plain text</returns>
        /// <exception cref="Exception">Thrown if the key can't find in the routes of MGP that's can be on software or software/wow6432Node</exception>
        /// <example>
        /// <code>
        /// KeyLibrary keys = new KeyLibrary();
        /// string key = keys.getKey("test", true);
        /// Console.WriteLine(key); // Output: "hello world"
        /// </code>
        /// </example>
        public string getKey(string keyName, bool decryptKey=true)
        {
            RegistryKey machineKeys = null;
            string result = string.Empty;
            try
            {
                machineKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MGP");
                result = machineKeys.GetValue(keyName).ToString();
                return decryptKey ? crypto.Decrypt(result) : result;
            }
            catch (Exception)
            {
                try
                {
                    machineKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\MGP");
                    result = machineKeys.GetValue(keyName).ToString();
                    return decryptKey ? crypto.Decrypt(result) : result;
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
