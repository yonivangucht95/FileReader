using FileReader.FileReaders;
using System;
using System.IO;

namespace FileReader
{
    public class FileReader
    {
        public FileReader()
        {
        }

        public string ReadFile(string path)
        {
            string fileContents = string.Empty;
            if (File.Exists(path))
            {
                //Select the file reader based on the extension for now,
                //ideally this can be asserted from the contents as the Windows image viewer does for images
                string fileExtension = Path.GetExtension(path);

                switch (fileExtension)
                {
                    case ".txt":
                        TextFileReader textReader = new TextFileReader();
                        fileContents = textReader.ReadFile(path);
                        break;
                    case ".xml":
                        XmlFileReader xmlReader = new XmlFileReader();
                        fileContents = xmlReader.ReadFile(path);
                        break;
                    default:
                        throw new NotSupportedException($"File type {fileExtension} is not supported.");
                }
            }
            else Console.WriteLine($"File {path} does not exist");

            return fileContents;
        }
    }
}
