using FileReader.Base;
using System;
using System.IO;
using System.Text.Json;

namespace FileReader.FileReaders
{
    public class JsonFileReader : FileReaderBase
    {
        public override string ReadFile(string path, FileEncryption encryption = FileEncryption.None)
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} does not exist.");
                }

                string encryptedContent = File.ReadAllText(path);
                string decryptedContent = Decrypt(encryptedContent, encryption);

                JsonDocument jsonDocument = JsonDocument.Parse(decryptedContent);

                string jasonAsText = JsonSerializer.Serialize(jsonDocument.RootElement, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });

                return jasonAsText;
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
