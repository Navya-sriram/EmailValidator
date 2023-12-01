using System.Text.RegularExpressions;

namespace EmailValidator;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the filename:");
        string? fileName = Console.ReadLine();

        //checking if file name entered is empty
        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("Filename cannot be empty");
            return;
        }

        //calling function to check if file exist
        String? file = CheckIfFileExist(fileName);

        if (file != null)
        {
            List<String> validEmail = [];
            List<String> invalidEmail = [];

            try
            {
                string[] rows = File.ReadAllLines(file);

                foreach (string row in rows)
                {
                    string[] column = row.Split(',');
                    //condition to check if email exists in each row
                    if (column.Length > 2 && !string.IsNullOrWhiteSpace(column[2]))
                    {
                        String email = column[2];
                        //calling function to valiate email address
                        bool isValid = EmailValidator(email);
                        (isValid ? validEmail : invalidEmail).Add(email);
                    }

                }
                Console.WriteLine("Valid Emails: " + string.Join(", ", validEmail));
                Console.WriteLine("Invalid Emails: " + string.Join(", ", invalidEmail));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File not found - {file}");
            }
            catch (Exception E)
            {
                Console.WriteLine($"An error occurred: {E.Message}");
            }

        }
        else
        {
            Console.WriteLine("File not found");
        }

    }

    // Function to check if the file exists
    static String? CheckIfFileExist(String? fileName)
    {

        try
        {
            String[] csvFilesList = Directory.GetFiles("../csv/", $"{fileName}.csv");
            if (csvFilesList.Length > 0)
            {
                return csvFilesList[0];
            }
            else
                return null;

        }
        catch (Exception E)
        {
            Console.WriteLine($"An error occurred while checking if file exists: {E.Message}");
            return null;
        }
    }

    //Function to validate an email
    static Boolean EmailValidator(String? email)
    {

        try
        {

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new(emailPattern);
            if (email != null && regex.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        catch (Exception E)
        {
            Console.WriteLine($"An error occurred while validating email: {E.Message}");
            return false;
        }
    }

}
