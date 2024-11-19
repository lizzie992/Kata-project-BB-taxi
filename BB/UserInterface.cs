using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class UserInterface
    {

        public static string MainMenuSelection(string answer)
        {
            Console.WriteLine("Welcome to Baxi! Please, select from the following options: ");
            Console.WriteLine("Press R to Register a new user");
            Console.WriteLine("Press L for Login");
            answer = Console.ReadLine().ToUpper();
            return answer;
        }


        public static void ClearScreen()
        {
            Console.WriteLine("Please, press a button to move on!");
            Console.ReadKey();
            Console.Clear();
        }

        public static void WriteNewUserMessage()
        {
            Console.WriteLine($"Thank you for signing up for our {Constants.COMPANYNAME} Taxi!");
            Console.WriteLine("Please, give us the following data to prepare your profile: ");
        }

        public static string getEmail()
        {
            Console.WriteLine("Email address (please, do not forget to use your company email address!): ");
            string emailAddress = Console.ReadLine();
            return emailAddress;
        }

        public static string getFirstName()
        {
            Console.WriteLine("First name: ");
            string firstName = Console.ReadLine();
            return firstName;
        }

        public static string getLastName()
        {
            Console.WriteLine("Last name: ");
            string lastName = Console.ReadLine();
            return lastName;
        }

        public static int getDepartmentName()
        {
            Console.WriteLine("Number of department from the list: ");
            int department = Convert.ToInt32(Console.ReadLine());
            return department;
        }

        public static int getPreferrefLanguage()
        {
            Console.WriteLine("Number of preferred language from the list: ");
            int preferredLanguage = Convert.ToInt32(Console.ReadLine());
            return preferredLanguage;
        }

        public static string getContact()
        {
            Console.WriteLine("Contact: ");
            string contact = Console.ReadLine();
            return contact;
        }

        
        public static void printList(Type myEnumType)
        {
            foreach (var value in Enum.GetValues(myEnumType))
            {
                Console.WriteLine(value.ToString());
            }
        }

    }
}
