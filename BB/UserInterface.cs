using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BB
{
    public class UserInterface
    {

        public static string MainMenuSelection(string answer)
        {
            Console.WriteLine("Welcome to Baxi! Please, select from the following options: ");
            Console.WriteLine($"Press {Constants.SELECT_REGISTER} to Register a new user");
            Console.WriteLine($"Press {Constants.SELECT_LOGIN} for Login");
            Console.WriteLine($"Press {Constants.SELECT_CREATE_AD} for Ad Creation");
            Console.WriteLine($"Press {Constants.SELECT_CHECK_ADS} to check out the ads posted");
            Console.WriteLine($"Press {Constants.SELECT_EXIT} to close the site");
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
            Console.WriteLine($"Thank you for signing up for our {Constants.COMPANY_NAME} Taxi!");
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
            Console.WriteLine("Contact information (phone number / email) // not mandatory: ");
            string contact = Console.ReadLine();
            return contact;
        }

        
        public static void printListofEnums(Type myEnumType)
        {
            int i = 1;
            foreach (var value in Enum.GetValues(myEnumType))
            {
                Console.WriteLine($"{i}: {value.ToString()}");
                i++;
            }
        }

        public static int getAdType()
        {
            Console.WriteLine("Please select if you are a driver or passenger: ");
            int adType = Convert.ToInt32(Console.ReadLine());
            return adType;
        }

        public static string getRoute()
        {
            Console.WriteLine("Please write the stops on your route: ");
            string route = Console.ReadLine();
            return route;
        }

        public static DateOnly getPickUpDate()
        {
            Console.WriteLine("Please give me the year: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the month: ");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the day: ");
            int day = Convert.ToInt32(Console.ReadLine());
            DateOnly date = new DateOnly(year, month, day);
            return date;
        }

        public static TimeOnly getPickUpTime()
        {
            Console.WriteLine("Please give me the hour: ");
            int hour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the minute: ");
            int minute = Convert.ToInt32(Console.ReadLine());
            TimeOnly time = new TimeOnly(hour, minute);
            return time;
        }


        public static DateTime getPickUpDateAndTime()
        {
            Console.WriteLine("Please give me the year: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the month: ");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the day: ");
            int day = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the hour: ");
            int hour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please give me the minute: ");
            int minute = Convert.ToInt32(Console.ReadLine());
            int second = 0;
            DateTime DateAndTime = new DateTime(year, month, day, hour, minute, second);
            return DateAndTime;
        }



        public static int getNumberOfSeats()
        {
            Console.WriteLine("How many seats are you offering / looking for?");
            int seats = Convert.ToInt32(Console.ReadLine());
            return seats;
        }

        public static string getSpecificRequests()
        {
            Console.WriteLine("Do you have any specific requests?");
            string requests = Console.ReadLine();
            return requests;
        }


        public static void printAdlist(List<Ad> AdList)
        {
            foreach (Ad ad in AdList)
            {
                    Console.WriteLine(ad);
            }

        }

        public static void printCompanyEmailNotValid()
        {
            Console.WriteLine("This email address is not valid! Please give me a valid company email address: ");
        }

    }
}
