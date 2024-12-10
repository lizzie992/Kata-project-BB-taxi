using System.Net.Mail;

namespace BB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> UserList = new List<User>();
            List<Ad> AdList = new List<Ad>();
            string answer = string.Empty;

            do
            {
                UserList = LogicalCode.LoadUserListFromFile(Constants.FILE_PATH_USERDATA);
                AdList = LogicalCode.LoadAdListFromFile(Constants.FILE_PATH_AD);

                //main menu part:
                answer = UserInterface.MainMenuSelection(answer);
                UserInterface.ClearScreen();


                if (answer == Constants.SELECT_REGISTER)
                {
                    UserInterface.WriteNewUserMessage();
                    User newUser = new User();

                    do
                    {
                        newUser.EmailAddress = UserInterface.getEmail();
                        if (!LogicalCode.IsCompanyEmailValid(newUser.EmailAddress.ToLower()))
                        {
                            UserInterface.printCompanyEmailNotValid();
                        }
                    } while (!LogicalCode.IsCompanyEmailValid(newUser.EmailAddress.ToLower())!);


                    newUser.FirstName = UserInterface.getFirstName();
                    newUser.LastName = UserInterface.getLastName();

                    UserInterface.printListofEnums(typeof(Department));
                    int numberOfDepartment = UserInterface.getDepartmentName();
                    newUser.Department = (Department)numberOfDepartment - 1;

                    UserInterface.printListofEnums(typeof(PreferredLanguage));
                    int numberOfPreferredLanguage = UserInterface.getPreferrefLanguage();
                    newUser.PreferredLanguage = (PreferredLanguage)numberOfPreferredLanguage - 1;

                    newUser.Contact = UserInterface.getContact();

                    newUser.Rating = 0;
                    newUser.NumberOfWarnings = 0; //these are just basic setup for all users at the beginning:)

                    UserList.Add(newUser);
                    LogicalCode.SaveUserToFile(UserList, Constants.FILE_PATH_USERDATA);
                    UserInterface.ClearScreen();
                }

                if (answer == Constants.SELECT_CREATE_AD)
                {
                    Ad Ad = new Ad();

                    UserInterface.printListofEnums(typeof(AdType));
                    int adType = UserInterface.getAdType();
                    Ad.AdType = (AdType)adType - 1;

                    Ad.Route = UserInterface.getRoute();

                    Ad.pickUpDateAndTime = UserInterface.getPickUpDateAndTime();

                    Ad.NumberOfSeats = UserInterface.getNumberOfSeats();

                    Ad.SpecificRequests = UserInterface.getSpecificRequests();

                    AdList.Add(Ad);
                    LogicalCode.SaveAdToFile(AdList, Constants.FILE_PATH_AD);
                    UserInterface.ClearScreen();

                }

                if (answer == Constants.SELECT_CHECK_ADS)
                {
                    UserInterface.printAdlist(AdList);
                    UserInterface.ClearScreen();
                }


            } while (answer != Constants.SELECT_EXIT);
        }
    }
}
