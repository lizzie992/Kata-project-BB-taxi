using System.Net.Mail;

namespace BB
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //main menu part:

            string answer = string.Empty;
            answer = UserInterface.MainMenuSelection(answer);
            UserInterface.ClearScreen();

            if (answer == Constants.SELECTREGISTER)
            {
                UserInterface.WriteNewUserMessage();
                User newUser = new User();

                UserInterface.ClearScreen();

                newUser.EmailAddress = UserInterface.getEmail();
                UserInterface.ClearScreen();
                newUser.FirstName = UserInterface.getFirstName();
                UserInterface.ClearScreen();
                newUser.LastName = UserInterface.getLastName();
                UserInterface.ClearScreen();

                UserInterface.printList(typeof(Department));
                int numberOfDepartment = UserInterface.getDepartmentName();
                newUser.Department = (Department)numberOfDepartment - 1;
                UserInterface.ClearScreen();

                UserInterface.printList(typeof(PreferredLanguage));
                int numberOfPreferredLanguage = UserInterface.getPreferrefLanguage();
                newUser.PreferredLanguage = (PreferredLanguage)numberOfPreferredLanguage - 1;
                UserInterface.ClearScreen();

                newUser.Contact = UserInterface.getContact();
                UserInterface.ClearScreen();

                newUser.Rating = 0;
                newUser.NumberOfWarnings = 0; //these are just basic setup for all users at the beginning:)
            }
        }
    }
}
