using FileReader.Base;
using FileReader.Encryption;
using System;
using System.IO;

namespace FileReader.FileReaders
{

    public class TextFileReader : IFileReader
    {
        public string ReadFile(string path, FileEncryption encryption = FileEncryption.None)
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} does not exist!");
                }

                if(encryption != FileEncryption.None) {
                    ITextFileEncryption textEncryptor;
                    switch(encryption)
                    {
                        case FileEncryption.Reversed:
                            textEncryptor = new ReversedEncryption();
                            break;
                        case FileEncryption.Offset:
                            textEncryptor = new ReversedEncryption();
                            break;
                        default: throw new ArgumentException();
                    }
                    return textEncryptor.Decrypt(File.ReadAllText(path));
                }

                return File.ReadAllText(path);
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
