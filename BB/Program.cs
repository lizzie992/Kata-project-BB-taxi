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
                answer = UserInterface.MainMenuSelection();
                UserInterface.ClearScreen();


                if (answer == Constants.SELECT_REGISTER.ToString())
                {
                    UserInterface.WriteNewUserMessage();
                    User newUser = new User();

                    do
                    {
                        newUser.EmailAddress = UserInterface.GetEmail();
                        if (!LogicalCode.IsCompanyEmailValid(newUser.EmailAddress.ToLower()))
                        {
                            UserInterface.PrintCompanyEmailNotValid();
                        }
                    } while (!LogicalCode.IsCompanyEmailValid(newUser.EmailAddress.ToLower())!);


                    newUser.FirstName = UserInterface.GetFirstName();
                    newUser.LastName = UserInterface.GetLastName();

                    UserInterface.printListOfEnums(typeof(Department));
                    int numberOfDepartment = UserInterface.GetDepartmentName();
                    newUser.Department = (Department)numberOfDepartment - 1;

                    UserInterface.printListOfEnums(typeof(PreferredLanguage));
                    int numberOfPreferredLanguage = UserInterface.GetPreferrefLanguage();
                    newUser.PreferredLanguage = (PreferredLanguage)numberOfPreferredLanguage - 1;

                    newUser.Location = UserInterface.GetLocation();

                    newUser.Contact = UserInterface.GetContact();

                    newUser.Rating = 0;
                    newUser.NumberOfWarnings = 0; //these are just basic setup for all users at the beginning:)

                    UserList.Add(newUser);
                    LogicalCode.SaveUserToFile(UserList, Constants.FILE_PATH_USERDATA);
                    UserInterface.ClearScreen();
                }

                if (answer == Constants.SELECT_CREATE_AD.ToString())
                {
                    Ad Ad = new Ad();

                    UserInterface.printListOfEnums(typeof(AdType));
                    int adType = UserInterface.GetAdType();
                    Ad.AdType = (AdType)adType - 1;

                    Ad.Route = UserInterface.GetRoute();

                    Ad.pickUpDateAndTime = UserInterface.GetPickUpDateAndTime();

                    Ad.NumberOfSeats = UserInterface.GetNumberOfSeats();

                    Ad.SpecificRequests = UserInterface.GetSpecificRequests();

                    AdList.Add(Ad);
                    LogicalCode.SaveAdToFile(AdList, Constants.FILE_PATH_AD);
                    UserInterface.ClearScreen();

                }

                if (answer == Constants.SELECT_CHECK_ADS.ToString())
                {
                    UserInterface.PrintAdList(AdList);
                    UserInterface.ClearScreen();
                }


                if (answer == Constants.SELECT_PROFILE.ToString())
                {
                    int user = UserList.Count - 1; //I decided to print the data of the last user of the list, as the login is not working yet, so we do not know who is using the system at this test phase yet
                    User User = UserList[user];
                    string profileAnswer = string.Empty;

                    do
                    {
                        UserInterface.PrintProfileData(UserList);
                        profileAnswer = UserInterface.PrintProfileMenu(profileAnswer);
                        UserInterface.ClearScreen();

                        if (profileAnswer == Constants.SELECT_CHANGE_PROFILE_DATA)
                        {
                            string changeAnswer = string.Empty;
                            changeAnswer = UserInterface.PrintProfileChangeOptions(changeAnswer);

                            if (changeAnswer == Constants.CHANGE_FIRST_NAME)
                            {
                                User.FirstName = UserInterface.GetFirstName();
                            }

                            if (changeAnswer == Constants.CHANGE_LAST_NAME)
                            {
                                User.LastName = UserInterface.GetLastName();
                            }

                            if (changeAnswer == Constants.CHANGE_DEPARTMENT.ToString())
                            {
                                UserInterface.printListOfEnums(typeof(Department));
                                int numberOfDepartment = UserInterface.GetDepartmentName();
                                User.Department = (Department)numberOfDepartment - 1;
                            }

                            if (changeAnswer == Constants.CHANGE_PREFERRED_LANGUAGE)
                            {
                                UserInterface.printListOfEnums(typeof(PreferredLanguage));
                                int numberOfPreferredLanguage = UserInterface.GetPreferrefLanguage();
                                User.PreferredLanguage = (PreferredLanguage)numberOfPreferredLanguage - 1;
                            }

                            if (changeAnswer == Constants.CHANGE_LOCATION.ToString())
                            {
                                User.Location = UserInterface.GetLocation();
                            }

                            if (changeAnswer == Constants.CHANGE_CONTACT.ToString())
                            {
                                User.Contact = UserInterface.GetContact();
                            }

                            UserList[user] = User;
                            LogicalCode.SaveUserToFile(UserList, Constants.FILE_PATH_USERDATA);

                            UserInterface.ClearScreen();
                        }


                        if (profileAnswer == Constants.SELECT_CHECK_MY_OWN_ADS)
                        {
                            UserInterface.PrintAdList(AdList); //at the moment all ads as we are not logged in
                            string changeAnswer = string.Empty;
                            changeAnswer = UserInterface.PrintCheckAdsMenu(changeAnswer);

                            if (changeAnswer == Constants.CHANGE_AD.ToString())
                            {

                                int number = 0;
                                number = UserInterface.PrintChooseAdMessage(number);

                                Ad ad = new Ad();
                                ad = AdList[number - 1];

                                UserInterface.ClearScreen();

                                answer = UserInterface.PrintAdChangeOptions(ad, answer);

                                if (answer == Constants.CHANGE_AD_TYPE)
                                {
                                    UserInterface.printListOfEnums(typeof(AdType));
                                    int adType = UserInterface.GetAdType();
                                    ad.AdType = (AdType)adType - 1;
                                }

                                if (answer == Constants.CHANGE_ROUTE)
                                {
                                    ad.Route = UserInterface.GetRoute();
                                }

                                if (answer == Constants.CHANGE_DATE_TIME)
                                {
                                    ad.pickUpDateAndTime = UserInterface.GetPickUpDateAndTime();
                                }

                                if (answer == Constants.CHANGE_NUMBER_OF_SEATS)
                                {
                                    ad.NumberOfSeats = UserInterface.GetNumberOfSeats();
                                }

                                if (answer == Constants.CHANGE_SPECIFIC_REQUESTS)
                                {
                                    ad.SpecificRequests = UserInterface.GetSpecificRequests();
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
