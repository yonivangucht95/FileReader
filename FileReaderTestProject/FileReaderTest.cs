using System.Text;
using System.Xml;
using FileReader.Base;
using FileReader.Encryption;
using FileReader.FileReaders;

namespace FileReaderTestProject
{
    [TestClass]
    public class FileReaderTest
    {
        [TestMethod]
        public void TextFileReader_ReadFile_ReturnsContent()
        {
            //Arrange
            string filePath = "./testing.txt"; //for the purposes of this test, create a file in a safe directory (build)
            string expectedContents = "abcd";

            try
            {
                byte[] contentBytes = Encoding.UTF8.GetBytes(expectedContents);

                // Create and write to the file so we always have something to test with.
                using (FileStream fs = File.Create(filePath))
                {
                    fs.Write(contentBytes, 0, contentBytes.Length);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            TextFileReader reader = new TextFileReader();

            //Act
            string fileContents = reader.ReadFile(filePath);

            //Assert  
            Assert.AreEqual(expectedContents, fileContents);
        }

        [TestMethod]
        public void TextFileReader_ReadFile_ReturnsDecryptedContent()
        {
            //Arrange
            string filePath = "./encrypted.text";
            string expectedContents = "abcd";
            ITextFileEncryption encryptor = new ReversedEncryption();
            try
            {
                byte[] contentBytes = Encoding.UTF8.GetBytes(encryptor.Encrypt(expectedContents));

                // Create and write to the file so we always have something to test with.
                using (FileStream fs = File.Create(filePath))
                {
                    fs.Write(contentBytes, 0, contentBytes.Length);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            TextFileReader reader = new TextFileReader();

            //Act
            string fileContents = reader.ReadFile(filePath, FileReader.FileEncryption.Reversed);

            //Assert  
            Assert.AreEqual(expectedContents, fileContents);
        }

        [TestMethod]
        public void XmlFileReader_ReadFile_VerifyContent()
        {
            // Arrange
            string filePath = "./testing.xml";
            string expectedContents = @"<?xml version=""1.0""?>
<book id=""bk101"">
    <author>Gambardella, Matthew</author>
    <title>XML Developer's Guide</title>
    <genre>Computer</genre>
    <price>44.95</price>
    <publish_date>2000-10-01</publish_date>
    <description>An in-depth look at creating applications 
    with XML.</description>
</book>";

            try
            {
                byte[] contentBytes = Encoding.UTF8.GetBytes(expectedContents);
                using (FileStream fs = File.Create(filePath))
                {
                    fs.Write(contentBytes, 0, contentBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Act
            XmlFileReader reader = new XmlFileReader();
            string fileContents = reader.ReadFile(filePath);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileContents);

            // Assert by comparing the expected node from the output
            XmlNode? titleNode = xmlDocument.SelectSingleNode("/book/title");
            string? actualTitle = titleNode?.InnerText;
            string expectedTitle = "XML Developer's Guide";

            Assert.AreEqual(expectedTitle, actualTitle);
        }

    }
}