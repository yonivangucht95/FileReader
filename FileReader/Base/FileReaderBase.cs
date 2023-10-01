using System.Collections.Generic;
using System;
using FileReader.Encryption;

namespace FileReader.Base
{
    public abstract class FileReaderBase
    {
        /// <summary>
        /// Reads the contents of a file and returns it as a string
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="encryption">Encryption to use if any</param>
        /// <returns>The content of the file</returns>
        public abstract string ReadFile(string path, FileEncryption encryption = FileEncryption.None);

        internal virtual string Decrypt(string content, FileEncryption encryption)
        {
            ITextFileEncryption textEncryptor;
            switch (encryption)
            {
                case FileEncryption.Reversed:
                    textEncryptor = new ReversedEncryption();
                    break;
                case FileEncryption.Offset:
                    textEncryptor = new ReversedEncryption();
                    break;
                default:
                    Console.WriteLine($"{encryption} not used or supported.");
                    return content;
            }
            return textEncryptor.Decrypt(content);
        }

        /// <summary>
        /// Checks if the user with a current role has access to the provided filepath. The check is done agains a preset keyvalue list where the keys are the roles and the values the permissions
        /// </summary>
        /// <param name="userRole"></param>
        /// <param name="roleFilePermissions"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual bool RoleAllowsRead(Role userRole, Dictionary<Role, RolePermissions> roleFilePermissions, string path)
        {
            if (roleFilePermissions.ContainsKey(userRole))
            {
                RolePermissions rolePermissions = roleFilePermissions[userRole];
                if (rolePermissions.Any) return true;

                if (rolePermissions.AllowedFiles == null || rolePermissions.AllowedFiles.Count == 0) return false;

                return rolePermissions.AllowedFiles.Contains(path);
            }

            return false;
        }
    }
}
