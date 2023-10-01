using FileReader.FileReaders;
using System;
using System.Collections.Generic;
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

    public enum Role
    {
        None,
        User,
        Admin,
        SuperAdmin
    }

    public class RolePermissions
    {
        /// <summary>
        /// Override to allow any file
        /// </summary>
        public bool Any { get; set; }

        /// <summary>
        /// List of allowed files to check when the Any property is false (null = no rights)
        /// </summary>
        public List<string>? AllowedFiles { get; set; }
    }

    public class RoleAccessException : Exception
    {
        public RoleAccessException() : base("Custom file access exception")
        {
        }

        public RoleAccessException(string message) : base(message)
        {
        }
    }

    public class FileReader
    {
        //Roles mapped to accessible files
        public Dictionary<Role, RolePermissions> RoleDeclarations { get; private set; }

        /// <summary>
        /// Generic constructor for when roles are unavailable or not used
        /// </summary>
        public FileReader()
        {
            RoleDeclarations = new Dictionary<Role, RolePermissions>();
        }

        /// <summary>
        /// Role based constructor
        /// </summary>
        /// <param name="roleFilePermissions">Dictionary containing Role keys and a list of role permissions eg. files</param>
        public FileReader(Dictionary<Role, RolePermissions> roleFilePermissions)
        {
            RoleDeclarations = roleFilePermissions;
        }

        /// <summary>
        /// Reads a file of a specified type with a specified encryption
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <param name="fileType">The file type of the file to read from</param>
        /// <param name="role">The role of the user reading the file, ignored for non-xml files</param>
        /// <param name="encryption">The encryption to use, default is none</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">When an unsupported FileType is used this error is thrown</exception>
        /// <exception cref="FileNotFoundException">When the file is not present or otherwise inaccessible</exception>
        /// <exception cref="RoleAccessException">The user's rights do not align with reading the requested file</exception>
        public string ReadFile(string path, FileType fileType, Role role = Role.None, FileEncryption encryption = FileEncryption.None)
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
                        if (xmlReader.RoleAllowsRead(role, RoleDeclarations, path))
                            fileContents = xmlReader.ReadFile(path, encryption);
                        else throw new RoleAccessException($"The role {role} does not have access to the file at {path}");
                        break;
                    default:
                        throw new NotSupportedException($"File type {fileType} is not supported.");
                }
            }
            else
            {
                Console.WriteLine($"File {path} does not exist");
                throw new FileNotFoundException($"File {path} was not found or inaccessible.");
            }

            return fileContents;
        }
    }
}