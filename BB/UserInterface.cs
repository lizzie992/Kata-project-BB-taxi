namespace BB
{
    public class UserInterface
    {

        public static string MainMenuSelection()
        {
            Console.WriteLine("Welcome to Baxi! Please, select from the following options: ");
            Console.WriteLine($"Press {Constants.SELECT_REGISTER} to Register a new user");
            Console.WriteLine($"Press {Constants.SELECT_LOGIN} for Login");
            Console.WriteLine($"Press {Constants.SELECT_PROFILE} to open your profile data");
            Console.WriteLine($"Press {Constants.SELECT_CREATE_AD} for Ad Creation");
            Console.WriteLine($"Press {Constants.SELECT_CHECK_ADS} to check out the ads posted");
            Console.WriteLine($"Press {Constants.SELECT_EXIT} to close the site");
            string answer = Console.ReadLine().ToUpper();
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

        public static string GetEmail()
        {
            Console.WriteLine("Email address (please, do not forget to use your company email address!): ");
            string emailAddress = Console.ReadLine().ToLower();
            return emailAddress;
        }

        public static string GetFirstName()
        {
            Console.WriteLine("First name: ");
            string firstName = Console.ReadLine();
            return firstName;
        }

        public static string GetLastName()
        {
            Console.WriteLine("Last name: ");
            string lastName = Console.ReadLine();
            return lastName;
        }

        public static Enum GetDepartmentName()
        {
            do
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (Enum.IsDefined(typeof(AdType), userInput))
                    {
                        return (Department)userInput;
                    }
                }
                else
                {
                    Console.WriteLine("Please give me a valid number: ");
                }
            } while (true);
        }

        public static Enum GetPreferrefLanguage()
        {
            do
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (Enum.IsDefined(typeof(AdType), userInput))
                    {
                        return (PreferredLanguage)userInput;
                    }
                }
                else
                {
                    Console.WriteLine("Please give me a valid number: ");
                }
            } while (true);
        }

        public static string GetLocation()
        {
            Console.WriteLine("Base location: ");
            string location = Console.ReadLine();
            return location;
        }


        public static string GetContact()
        {
            Console.WriteLine("Contact information (phone number / email) // not mandatory: ");
            string contact = Console.ReadLine();
            return contact;
        }


        public static void PrintListOfEnums(Type myEnumType)
        {
            int i = 1;
            foreach (var value in Enum.GetValues(myEnumType))
            {
                Console.WriteLine($"{i}: {value.ToString()}");
                i++;
            }
        }


        public static Enum GetAdType()
        {
            do
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (Enum.IsDefined(typeof(AdType), userInput))
                    {
                        return (AdType)userInput;
                    }
                }
                else
                {
                    Console.WriteLine("Please give me a valid number: ");
                }
            } while (true);
        }

        public static string GetRoute()
        {
            Console.WriteLine("Please write the stops on your route: ");
            string route = Console.ReadLine();
            return route;
        }

        public static int GetInt(string instruction)
        {
            do
            {
                Console.WriteLine(instruction);
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Please give me a valid number: ");
                }
            } while (true);
        }

        public static DateOnly GetPickUpDate()
        {
            int year = GetInt("Please give me the year: ");
            int month = GetInt("Please give me the month: ");
            int day = GetInt("Please give me the day: ");
            DateOnly date = new DateOnly(year, month, day);
            return date;
        }

        public static TimeOnly GetPickUpTime()
        {
            int hour = GetInt("Please give me the hour: ");
            int minute = GetInt("Please give me the minute: ");
            TimeOnly time = new TimeOnly(hour, minute);
            return time;
        }

        public static DateTime GetPickUpDateAndTime()
        {
            DateOnly date = GetPickUpDate();
            TimeOnly time = GetPickUpTime();
            return new DateTime(date, time);
        }



        public static int GetNumberOfSeats()
        {
            Console.WriteLine("How many seats are you offering / looking for?");
            int seats = Convert.ToInt32(Console.ReadLine());
            return seats;
        }

        public static string GetSpecificRequests()
        {
            Console.WriteLine("Do you have any specific requests?");
            string requests = Console.ReadLine();
            return requests;
        }


        public static void PrintAdList(List<Ad> AdList)
        {
            int number = 1;
            foreach (Ad ad in AdList)
            {
                Console.WriteLine($"{number}.: \r\n{ad}");
                number++;
            }
        }

        public static void PrintCompanyEmailNotValid()
        {
            Console.WriteLine("This email address is not valid! Please give me a valid company email address: ");
        }

        public static void PrintProfileData(List<User> UserList)
        {
            int user = UserList.Count - 1; //I decided to print the data of the last user of the list, as the login is not working yet, so we do not know who is using the system at this test phase yet
            User User = UserList[user];
            Console.WriteLine($"Email address: {User.EmailAddress}");
            Console.WriteLine($"Name: {User.FirstName} {User.LastName}");
            Console.WriteLine($"Department: {User.Department}");
            Console.WriteLine($"Preferred Language: {User.PreferredLanguage}");
            Console.WriteLine($"Location: {User.Location}");
            Console.WriteLine($"Contact: {User.Contact}");
            Console.WriteLine($"Rating: {User.Rating}");
            Console.WriteLine($"Number of warnings: {User.NumberOfWarnings}");
        }

        public static string PrintProfileMenu(string answer)
        {
            Console.WriteLine($"\r\nPlease press {Constants.SELECT_CHANGE_PROFILE_DATA} to change your personal data");
            Console.WriteLine($"Please press {Constants.SELECT_CHECK_MY_OWN_ADS} to see your ads");
            Console.WriteLine($"Press {Constants.SELECT_EXIT} to close the site");
            answer = Console.ReadLine().ToUpper();
            return answer;
        }

        public static string PrintProfileChangeOptions(string answer)
        {

            Console.WriteLine($"Please press {Constants.CHANGE_FIRST_NAME} to change your first name\r\n" +
                $"Please press {Constants.CHANGE_LAST_NAME} to change your last name\r\n" +
                $"Please press {Constants.CHANGE_DEPARTMENT} to change your department\r\n" +
                $"Please press {Constants.CHANGE_PREFERRED_LANGUAGE} to change your preferred language\r\n" +
                $"Please press {Constants.CHANGE_LOCATION} to change your base location\r\n" +
                $"Please press {Constants.CHANGE_CONTACT} to change your public contact info!\r\n");
            answer = Console.ReadLine().ToUpper();
            return answer;
        }

        public static string PrintCheckAdsMenu(string answer)
        {
            Console.WriteLine($"If you want to modify any of them, please, press {Constants.CHANGE_AD}");
            Console.WriteLine($"Press {Constants.SELECT_EXIT} to close the site");
            answer = Console.ReadLine().ToUpper();
            return answer;
        }

        public static int PrintChooseAdMessage(int number)
        {
            Console.WriteLine("Please select which ad you want to modify. Give me the number of the ad: ");
            number = Convert.ToInt32(Console.ReadLine());
            return number;
        }


        public static string PrintAdChangeOptions(Ad ad, string answer)
        {
            Console.WriteLine(ad);
            Console.WriteLine($"Press {Constants.CHANGE_AD_TYPE} to change the ad type!");
            Console.WriteLine($"Press {Constants.CHANGE_ROUTE} to change the route!");
            Console.WriteLine($"Press {Constants.CHANGE_DATE_TIME} to change the date and time!");
            Console.WriteLine($"Press {Constants.CHANGE_NUMBER_OF_SEATS} to change the number of seats!");
            Console.WriteLine($"Press {Constants.CHANGE_SPECIFIC_REQUESTS} to change the specific requests!");
            Console.WriteLine($"Press {Constants.DELETE_AD} to delete the ad!");
            answer = Console.ReadLine().ToUpper();
            return answer;
        }

    }
}
