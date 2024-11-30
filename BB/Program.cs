using System.Net.Mail;

namespace BB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> UserList = new List<User>();
            string answer = string.Empty;
            
            do
            {
                UserList = LogicalCode.LoadListFromFile();

                //main menu part:
                answer = UserInterface.MainMenuSelection(answer);
                UserInterface.ClearScreen();


                if (answer == Constants.SELECT_REGISTER)
                {
                    UserInterface.WriteNewUserMessage();
                    User newUser = new User();

                    newUser.EmailAddress = UserInterface.getEmail();
                    newUser.FirstName = UserInterface.getFirstName();
                    newUser.LastName = UserInterface.getLastName();

                    UserInterface.printList(typeof(Department));
                    int numberOfDepartment = UserInterface.getDepartmentName();
                    newUser.Department = (Department)numberOfDepartment - 1;

                    UserInterface.printList(typeof(PreferredLanguage));
                    int numberOfPreferredLanguage = UserInterface.getPreferrefLanguage();
                    newUser.PreferredLanguage = (PreferredLanguage)numberOfPreferredLanguage - 1;

                    newUser.Contact = UserInterface.getContact();

                    newUser.Rating = 0;
                    newUser.NumberOfWarnings = 0; //these are just basic setup for all users at the beginning:)

                    UserList.Add(newUser);
                    LogicalCode.SaveToFile(UserList);
                    UserInterface.ClearScreen();
                }

            } while (answer != Constants.SELECT_EXIT);
        }
    }
}
