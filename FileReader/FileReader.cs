using FileReader.Encryption;
using FileReader.FileReaders;
using System;
using System.IO;

namespace FileReader
{
    public enum FileType
    {
        Text,
        Xml
    }

    public enum FileEncryption
    {
        None,
        Offset,
        Reversed
    }

    public class FileReader
    {
        public FileReader()
        {
        }

        /// <summary>
        /// Reads a file of a specified type with a specified encryption
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <param name="fileType">The file type of the file to read from</param>
        /// <param name="encryption">The encryption to use, default is none</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">When an unsupported FileType is used this error is thrown</exception>
        public string ReadFile(string path, FileType fileType, FileEncryption encryption = FileEncryption.None)
        {
            string fileContents = string.Empty;
            if (File.Exists(path))
            {
                switch (fileType)
                {
                    case FileType.Text:
                        TextFileReader textReader = new TextFileReader();
                        fileContents = textReader.ReadFile(path, encryption);
                        break;
                    case FileType.Xml:
                        XmlFileReader xmlReader = new XmlFileReader();
                        fileContents = xmlReader.ReadFile(path);
                        break;
                    default:
                        throw new NotSupportedException($"File type {fileType} is not supported.");
                }
            }
            else
            {
                Console.WriteLine($"File {path} does not exist");
            }

            return fileContents;
        }
    }
}