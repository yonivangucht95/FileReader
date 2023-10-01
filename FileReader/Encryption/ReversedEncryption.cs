using FileReader.Base;
using System;

namespace FileReader.Encryption
{
    public class ReversedEncryption : ITextFileEncryption
    {
        public string Encrypt(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public string Decrypt(string encryptedText)
        {
            return Encrypt(encryptedText);
        }
    }
}
