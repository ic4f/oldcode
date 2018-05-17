using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Core
{
    public class EncryptionTool
    {
        private Byte[] key = { 11, 27, 13, 24, 54, 6, 7, 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        private Byte[] iv = { 8, 10, 68, 12, 9, 4, 70, 5 };

        //remove these after converting old passwords to encrypted form
        public static Byte[] TempEncryptPassword(string password)
        {
            return new UnicodeEncoding().GetBytes(password);
        }

        public static string TempDecryptPassword(Byte[] bytes)
        {
            return new UnicodeEncoding().GetString(bytes);
        }

        public Byte[] Encrypt(string input)
        {
            MemoryStream buffer = new MemoryStream();

            //make encryptStream
            CryptoStream encryptStream = new CryptoStream(
                buffer, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write);

            //write input into buffer
            byte[] inputBytes = (new UTF8Encoding()).GetBytes(input);
            encryptStream.Write(inputBytes, 0, inputBytes.Length);
            encryptStream.FlushFinalBlock();
            buffer.Position = 0;

            //read encrypted bytes from buffer into byte array output
            int length = (int)buffer.Length;
            byte[] output = new byte[length];
            buffer.Read(output, 0, length);
            encryptStream.Close();

            return output;
        }

        public string Decrypt(byte[] inputBytes)
        {
            MemoryStream buffer = new MemoryStream();

            //make decryptStream
            CryptoStream decryptStream = new CryptoStream(
                buffer, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Write);

            //write inputBytes into buffer
            decryptStream.Write(inputBytes, 0, inputBytes.Length);
            decryptStream.FlushFinalBlock();
            buffer.Position = 0;

            //read encoded bytes from buffer into byte array output
            int length = (int)buffer.Length;
            byte[] output = new byte[length];
            buffer.Read(output, 0, length);
            decryptStream.Close();

            return (new UTF8Encoding()).GetString(output);
        }
    }
}
