using FileReader.Base;
using FileReader.Encryption;
using System;
using System.IO;

namespace FileReader.FileReaders
{

    public class TextFileReader : FileReaderBase
    {
        public override string ReadFile(string path, FileEncryption encryption = FileEncryption.None)
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} does not exist!");
                }

                return Decrypt(File.ReadAllText(path), encryption);
            }
            catch (FileNotFoundException ex)
            {
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
