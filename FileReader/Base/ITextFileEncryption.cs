namespace FileReader.Base
{
    public interface ITextFileEncryption
    {
        /// <summary>
        /// Encrypts text
        /// </summary>
        /// <param name="text">The text to encrypt</param>
        /// <returns>The encrypted string</returns>
        string Encrypt(string text);

        /// <summary>
        /// Decrypts text
        /// </summary>
        /// <param name="encryptedText">The text to decrypt</param>
        /// <returns>The decrypted string</returns>
        string Decrypt(string encryptedText);
    }
}
