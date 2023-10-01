using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Base
{
    internal interface IFileReader
    {
        /// <summary>
        /// Reads the contents of a file and returns it as a string
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The content of the file</returns>
        string ReadFile(string path);
    }
}
