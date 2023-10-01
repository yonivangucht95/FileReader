using FileReader.Base;
using System;
using System.IO;
using System.Xml;

namespace FileReader.FileReaders
{
    public class XmlFileReader : FileReaderBase
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

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(decryptedContent);

                return ExtractXmlNodesAsText(xmlDoc);
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

        /// <summary>
        /// Helper function to retrieve an xml document as a readable text file
        /// </summary>
        /// <param name="xmlDocument">The xml file</param>
        /// <returns>Readable text</returns>
        private string ExtractXmlNodesAsText(XmlDocument xmlDocument)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    NewLineChars = "\r\n",
                };

                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlDocument.Save(xmlWriter);
                }

                return stringWriter.ToString();
            }
        }
    }
}
