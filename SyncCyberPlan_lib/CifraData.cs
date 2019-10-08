using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;


namespace SyncCyberPlan_lib
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.IO;

    /// <summary>
    /// By Umberto baratto
    /// </summary>
    static public class CifraData
    {
        /// <summary>
        /// By Umberto baratto
        /// </summary>
        private static byte[] key = ASCIIEncoding.ASCII.GetBytes("7yhbcde5");
        private static byte[] initialVector = ASCIIEncoding.ASCII.GetBytes("8snc5dg2");

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns>The encrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be 
        /// thrown when the original string is null or empty.</exception>
        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                return null;
                //throw new ArgumentNullException("The string which needs to be encrypted can not be null or empty.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(key, initialVector), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();

            //1 return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);				
            string str_base64 = System.Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length); //NB memoryStream.Length e non memoryStream.GetBuffer().Length
            return str_base64;

            //2 System.Text.UnicodeEncoding UTF16 = new System.Text.UnicodeEncoding();
            //2 return UTF16.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);

            //3	SISTEMA VECCHIO che usa chiamata a System.Web
            //return System.Web.HttpUtility.UrlEncode(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown 
        /// when the crypted string is null or empty.</exception>
        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            //1 
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            //byte[] byte_temp2 = Convert.FromBase64String(str_base64);

            //2 System.Text.UTF32Encoding UTF32 = new System.Text.UTF32Encoding();
            //2 MemoryStream memoryStream = new MemoryStream(UTF32.GetBytes(cryptedString));

            //3 SISTEMA VECCHIO
            //3 MemoryStream memoryStream = new MemoryStream(System.Web.HttpUtility.UrlDecodeToBytes(cryptedString));


            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(key, initialVector), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
        public static System.Security.SecureString ConvertToSecureString(string ConfidentialString)
        {
            System.Security.SecureString pwd = new System.Security.SecureString();
            //char[] buffer = new char[ConfidentialString.Length ];
            char[] buffer = ConfidentialString.ToCharArray();
            for (int count = 0; count < buffer.Length; count++) pwd.AppendChar(buffer[count]);
            return (pwd);
        }       
    }
}