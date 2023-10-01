using System.Text;
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
    }
}