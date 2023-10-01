using FileReader;

internal class Program
{
    private static void Main(string[] args)
    {
        do
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Please select a file type (use the line number):");

            FileType selectedFileType = GetFileTypeFromInput();
            FileEncryption selectedEncryptionType = GetEncryptionTypeFromInput();
            bool shouldUseRoles = ShouldUseRoles();
            Role selectedRole = Role.None;
            if (shouldUseRoles)
            {
                selectedRole = GetRoleFromInput();
                Console.WriteLine($"Selected File Type: {selectedFileType}\nEncryption type: {selectedEncryptionType}\nRole-base security: {shouldUseRoles} -> {selectedRole}");
            }
            else Console.WriteLine($"Selected File Type: {selectedFileType}\nEncryption type: {selectedEncryptionType}\nRole-base security: No");

            if (ReadActualFile())
            {
                string path = GetPathToFile();
                try
                {
                    FileReader.FileReader fr = new FileReader.FileReader();
                    foreach (Role role in Enum.GetValues(typeof(Role)))
                    {
                        //Add permissions for the file to all roles because otherwise I have to add a way to add these too which would be very unwieldy in CLI
                        fr.RoleDeclarations.Add(role, new RolePermissions() { AllowedFiles = new List<string>() { path} });
                    }
                    string content = fr.ReadFile(path, selectedFileType, selectedRole, selectedEncryptionType);
                    Console.Write(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        while (true);
    }



    static FileType GetFileTypeFromInput()
    {
        while (true)
        {
            int i = 0;
            foreach (FileType fileType in Enum.GetValues(typeof(FileType)))
            {
                Console.WriteLine($" {++i} - {fileType}");
            }

            Console.Write("Choose the file type: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= Enum.GetValues(typeof(FileType)).Length + 1)
                {
                    // Convert the line number to the corresponding FileType enum
                    return (FileType)(choice - 1);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid line number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static FileEncryption GetEncryptionTypeFromInput()
    {
        while (true)
        {
            int i = 0;
            foreach (FileEncryption fileEncryption in Enum.GetValues(typeof(FileEncryption)))
            {
                Console.WriteLine($" {++i} - {fileEncryption}");
            }

            Console.Write("Choose the encryption to use: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= Enum.GetValues(typeof(FileType)).Length + 1)
                {
                    // Convert the line number to the corresponding FileType enum
                    return (FileEncryption)(choice - 1);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid line number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static bool ShouldUseRoles()
    {
        while (true)
        {
            Console.Write("Do you want to use role-based security? (y/n): ");
            string userInput = Console.ReadLine();

            if (string.Equals(userInput, "y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (string.Equals(userInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            }
        }
    }

    static Role GetRoleFromInput()
    {
        while (true)
        {
            int i = 0;
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                Console.WriteLine($" {++i} - {role}");
            }

            Console.Write("Choose your role: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= Enum.GetValues(typeof(FileType)).Length + 1)
                {
                    // Convert the line number to the corresponding FileType enum
                    return (Role)(choice - 1);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid line number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static bool ReadActualFile()
    {
        while (true)
        {
            Console.Write("Do you want to read an actual file? You need a path to a file afterwards. Permission is automaically assigned. (y/n): ");
            string userInput = Console.ReadLine();

            if (string.Equals(userInput, "y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (string.Equals(userInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            }
        }
    }
    static string GetPathToFile()
    {
        Console.WriteLine($"Provide a path to the file");
        return Console.ReadLine();
    }
}