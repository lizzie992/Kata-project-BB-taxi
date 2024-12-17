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


                if (answer == Constants.SELECT_REGISTER.ToString())
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

                    newUser.Location = UserInterface.getLocation();

                    newUser.Contact = UserInterface.getContact();

                    newUser.Rating = 0;
                    newUser.NumberOfWarnings = 0; //these are just basic setup for all users at the beginning:)

                    UserList.Add(newUser);
                    LogicalCode.SaveUserToFile(UserList, Constants.FILE_PATH_USERDATA);
                    UserInterface.ClearScreen();
                }

                if (answer == Constants.SELECT_CREATE_AD.ToString())
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

                if (answer == Constants.SELECT_CHECK_ADS.ToString())
                {
                    UserInterface.printAdlist(AdList);
                    UserInterface.ClearScreen();
                }


                if (answer == Constants.SELECT_PROFILE.ToString())
                {
                    int user = UserList.Count - 1; //I decided to print the data of the last user of the list, as the login is not working yet, so we do not know who is using the system at this test phase yet
                    User User = UserList[user];
                    string profileAnswer = string.Empty;

                    do
                    {
                        UserInterface.printProfileData(UserList);
                        profileAnswer = UserInterface.printProfileMenu(profileAnswer);
                        UserInterface.ClearScreen();

                        if (profileAnswer == Constants.SELECT_CHANGE_PROFILE_DATA)
                        {
                            string changeAnswer = string.Empty;
                            changeAnswer = UserInterface.printProfileChangeOptions(changeAnswer);

                            if (changeAnswer == Constants.CHANGE_FIRST_NAME)
                            {
                                User.FirstName = UserInterface.getFirstName();
                            }

                            if (changeAnswer == Constants.CHANGE_LAST_NAME)
                            {
                                User.LastName = UserInterface.getLastName();
                            }

                            if (changeAnswer == Constants.CHANGE_DEPARTMENT.ToString())
                            {
                                UserInterface.printListofEnums(typeof(Department));
                                int numberOfDepartment = UserInterface.getDepartmentName();
                                User.Department = (Department)numberOfDepartment - 1;
                            }

                            if (changeAnswer == Constants.CHANGE_PREFERRED_LANGUAGE)
                            {
                                UserInterface.printListofEnums(typeof(PreferredLanguage));
                                int numberOfPreferredLanguage = UserInterface.getPreferrefLanguage();
                                User.PreferredLanguage = (PreferredLanguage)numberOfPreferredLanguage - 1;
                            }

                            if (changeAnswer == Constants.CHANGE_LOCATION.ToString())
                            {
                                User.Location = UserInterface.getLocation();
                            }

                            if (changeAnswer == Constants.CHANGE_CONTACT.ToString())
                            {
                                User.Contact = UserInterface.getContact();
                            }

                            UserList[user] = User;
                            LogicalCode.SaveUserToFile(UserList, Constants.FILE_PATH_USERDATA);

                            UserInterface.ClearScreen();
                        }


                        if (profileAnswer == Constants.SELECT_CHECK_MY_OWN_ADS)
                        {
                            UserInterface.printAdlist(AdList); //at the moment all ads as we are not logged in
                            string changeAnswer = string.Empty;
                            changeAnswer = UserInterface.printCheckAdsMenu(changeAnswer);

                            if (changeAnswer == Constants.CHANGE_AD.ToString())
                            {

                                int number = 0;
                                number = UserInterface.printChooseAdMessage(number);

                                Ad ad = new Ad();
                                ad = AdList[number - 1];

                                UserInterface.ClearScreen();

                                answer = UserInterface.printAdChangeOptions(ad, answer);

                                if (answer == Constants.CHANGE_AD_TYPE)
                                {
                                    UserInterface.printListofEnums(typeof(AdType));
                                    int adType = UserInterface.getAdType();
                                    ad.AdType = (AdType)adType - 1;
                                }

                                if (answer == Constants.CHANGE_ROUTE)
                                {
                                    ad.Route = UserInterface.getRoute();
                                }

                                if (answer == Constants.CHANGE_DATE_TIME)
                                {
                                    ad.pickUpDateAndTime = UserInterface.getPickUpDateAndTime();
                                }

                                if (answer == Constants.CHANGE_NUMBER_OF_SEATS)
                                {
                                    ad.NumberOfSeats = UserInterface.getNumberOfSeats();
                                }

                                if (answer == Constants.CHANGE_SPECIFIC_REQUESTS)
                                {
                                    ad.SpecificRequests = UserInterface.getSpecificRequests();
                                }

                                AdList[number - 1] = ad;
                                LogicalCode.SaveAdToFile(AdList, Constants.FILE_PATH_AD);


                                if (answer == Constants.DELETE_AD.ToString())
                                {
                                    AdList.Remove(ad);
                                    LogicalCode.SaveAdToFile(AdList, Constants.FILE_PATH_AD);
                                }

                            }
                            UserInterface.ClearScreen();


                        }

                    } while (profileAnswer != Constants.SELECT_EXIT.ToString());
                }


            } while (answer != Constants.SELECT_EXIT.ToString());
        }
    }
}
