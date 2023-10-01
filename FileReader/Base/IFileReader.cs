namespace FileReader.Base
{
    public interface IFileReader
    {

        /// <summary>
        /// Reads the contents of a file and returns it as a string
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="encryption">Encryption to use if any</param>
        /// <returns>The content of the file</returns>
        string ReadFile(string path, FileEncryption encryption = FileEncryption.None);
    }
}
