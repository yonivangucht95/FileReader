using FileReader.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace FileReader.Encryption
{
    public class OffestEncryption : ITextFileEncryption
    {
        private const int SHIFT = 1;

        public string Encrypt(string text)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] + SHIFT);
            }

            return new string(chars);
        }

        public string Decrypt(string encryptedText)
        {
            char[] chars = encryptedText.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] - SHIFT);
            }
            return new string(chars);
        }
    }
}
