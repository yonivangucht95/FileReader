using System.Text;
using System.Xml;
using FileReader.Base;
using FileReader.Encryption;
using FileReader.FileReaders;

namespace FileReaderTestProject
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void ReversedEncryption_Decrypt_ReturnsDecrypted()
        {
            //Arrange
            ITextFileEncryption encryptor = new ReversedEncryption();
            string input = "123456789";
            string expected = "987654321";

            //Act
            string output = encryptor.Decrypt(input);

            //Assert
            Assert.AreEqual(expected, output);
        }
        [TestMethod]
        public void OffsetEncryption_Decrypt_ReturnsDecrypted()
        {
            //Arrange
            ITextFileEncryption encryptor = new OffestEncryption();
            string input = "123456789";
            string expected = "012345678";

            //Act
            string output = encryptor.Decrypt(input);

            //Assert
            Assert.AreEqual(expected, output);
        }
    }
}