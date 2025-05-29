using BB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Runtime.InteropServices.Marshalling;


namespace BaxiWebApp.Data
{
    public class DataService
    {

        IDbContextFactory<BaxiWebAppContext> _dbcFactory;
        public DataService(IDbContextFactory<BaxiWebAppContext> dbcFactory) //dependency injection of the DbContextFactory
        {
            _dbcFactory = dbcFactory;
        }

        public void SendMessage(Conversation C, User currentlyLoggedInUser, string TextMessage, int ID)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                Conversation CurrentConversation = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
                Message message = new Message();
                message.fromUser = context.Users.Find(currentlyLoggedInUser.Id);
                if (currentlyLoggedInUser.Id == CurrentConversation.contactingUser.Id)
                {
                    message.toUser = CurrentConversation.adOwnerUser;
                }
                else if (currentlyLoggedInUser.Id == CurrentConversation.adOwnerUser.Id)
                {
                    message.toUser = CurrentConversation.contactingUser;
                }
                message.messageText = TextMessage;
                message.timeStamp = DateTime.Now;
                //  context.Entry(message.toUser).State = EntityState.Unchanged;
                //    context.Entry(message.fromUser).State = EntityState.Unchanged;
                CurrentConversation.messages.Add(message);
                //        context.Update(CurrentConversation);
                context.SaveChanges();
            }

        }

        public Conversation? GetCurrentConversation(int ID)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                var result = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
                return result;
            }
        }



        public void DeleteConversation(Conversation C)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                //first delete the messages that have the ID of this current conversation as long as the current conversation exists
                IEnumerable<Message> messagesToDelete = context.Messages.ToList<Message>().AsEnumerable();
                messagesToDelete = messagesToDelete.Where(m => m.ConversationID == C.ID);
                context.Messages.RemoveRange(messagesToDelete);
                //once the messages are removed I can delete the conversation
                context.Conversations.Remove(C);
                context.SaveChanges();
            }
        }

        public void ReportConversation(Conversation C, User currentlyLoggedInUser, string reasonForReporting)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                IEnumerable<Message> messagesToReport = context.Messages.Include(m => m.fromUser).Include(m => m.toUser).ToList<Message>().AsEnumerable();
                messagesToReport = messagesToReport.Where(m => m.ConversationID == C.ID).OrderBy(c => c.timeStamp); ;
                StringBuilder message = new StringBuilder($"Messages: \r\n\r\n");
                foreach (Message m in messagesToReport)
                {
                    message.Append($"From: {showUserNameWithStatus(m.fromUser)}\r\n");
                    message.Append($"To: {showUserNameWithStatus(m.toUser)}\r\n");
                    message.Append($"Message: {m.messageText.ToString()}\r\n");
                    message.Append($"Timestamp: {m.timeStamp.ToString()}\r\n\r\n");
                }
                string output = message.ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
                client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
                mailMessage.To.Add("gulyaskata99@gmail.com");
                mailMessage.Body = $"Please check out the following conversation below that {currentlyLoggedInUser} reported to you, with the following reason: {reasonForReporting}\r\n{message}";
                mailMessage.Subject = "A conversation was just reported to you";
                client.Send(mailMessage);

            }
        }

        public void reportAd(Ad ad, User currentlyLoggedInUser, string reasonForReporting)
        {
            string adDetails = $"Ad owner: {showUserNameWithStatus(ad.AdOwner)}\r\nAd type: {ad.AdType}\r\nAd Direction: {ad.AdDirection}\r\nAddress: {ad.PickUpDropOffLocation}\r\nPick up date and time: {ad.PickUpDateAndTime}\r\nNumber of seats: {ad.NumberOfSeats}\r\nSpecific requests: {ad.SpecificRequests}\r\n";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add("gulyaskata99@gmail.com");
            mailMessage.Body = $"Please check out the following ad below that {showUserNameWithStatus(currentlyLoggedInUser)} reported to you: \r\n{adDetails}\r\nThe reason for reporting is: {reasonForReporting}\r\nOpen the ad here: LINK";
            mailMessage.Subject = "An ad was just reported to you";
            client.Send(mailMessage);
        }




        public void sendEmailNewAd(Ad ad)
        {
            string message = $"You might be interested in this new Ad:\r\n Ad type: {ad.AdType}\r\nAd Direction: {ad.AdDirection}\r\nAddress: {ad.PickUpDropOffLocation}\r\n On {ad.PickUpDateAndTime}\r\n Available seats: {ad.NumberOfSeats}\r\n Any speicifc requests: {ad.SpecificRequests}\r\n";
            if (ad.PickUpDropOffLocation.Contains("Sendling"))
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
                client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
                mailMessage.To.Add("gulyaskata99@gmail.com"); //this will be changed to parameter string toEmailAddress
                mailMessage.Body = message;
                mailMessage.Subject = "Check out this new ad: ";
                client.Send(mailMessage);
            }
        }

        int maxNumberOfWarnings = 3;
        public void giveUserAWarning(User user, string reasonForWarning)
        {

            using (var context = _dbcFactory.CreateDbContext())
            {
                user.NumberOfWarnings++;
                context.Update(user);
                context.SaveChanges();
                sendEmailAboutWarning(user, reasonForWarning);
                if (user.NumberOfWarnings == maxNumberOfWarnings)
                {
                    string reason = "You have reached 3 warnings!";
                    deactivateUser(user, reason);
                    context.SaveChanges();
                }
            }
        }


        public void sendEmailAboutWarning(User user, string reasonForWarning)
        {
            string message = $"Dear {showUserNameWithStatus(user)}\r\nYou have received a warning from one of our admins.\r\nReason for the warning is: {reasonForWarning}\r\nYour current number of warnings is: {user.NumberOfWarnings}\r\nPlease be aware that as soon as you reach {maxNumberOfWarnings} warnings your account will be automatically inactivated!";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add("gulyaskata99@gmail.com");
            mailMessage.Body = message;
            mailMessage.Subject = "You received a warning on the Baxi app!";
            client.Send(mailMessage);

        }

        public void sendEmailAboutDeactivation(User user, string reasonForDeactivating)
        {
            string message = $"Dear {showUserNameWithStatus(user)}\r\nYour account has been deactivated!\r\nThe reason is: {reasonForDeactivating}\r\nPlease contact the admin team about the reasons behind it and the possible reactivation!";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add("gulyaskata99@gmail.com");
            mailMessage.Body = message;
            mailMessage.Subject = "Your Baxi accout has been deactivated!";
            client.Send(mailMessage);

        }

        public void sendEmailAboutReactivation(User user)
        {
            string message = $"Dear {showUserNameWithStatus(user)}\r\nYour account has been reactivated!\r\nYou can now use all functions again.\r\nPlease bring some flowers/chocolate to the admin team to show your gratitude ;)";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add("gulyaskata99@gmail.com");
            mailMessage.Body = message;
            mailMessage.Subject = "Your Baxi accout has been reactivated!";
            client.Send(mailMessage);

        }

        public void sendEmailAboutDeletingAdByAdmin(Ad ad, string reasonForDeleting)
        {
            string message = $"Dear {showUserNameWithStatus(ad.AdOwner)}\r\nYour ad has been removed by the admins for reasons: {reasonForDeleting}\r\n\r\nAd:\r\nAd type: {ad.AdType}\r\nAd Direction: {ad.AdDirection}\r\nAddress: {ad.PickUpDropOffLocation}\r\n On {ad.PickUpDateAndTime}\r\n Available seats: {ad.NumberOfSeats}\r\n Any speicifc requests: {ad.SpecificRequests}\r\n)";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add("gulyaskata99@gmail.com");
            mailMessage.Body = message;
            mailMessage.Subject = "Your Baxi ad is deleted!";
            client.Send(mailMessage);

        }



        public void deactivateUser(User user, string reasonForDeactivating)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isActive = false;
                context.Update(user);
                context.SaveChanges();
                sendEmailAboutDeactivation(user, reasonForDeactivating);
            }
        }

        public void reactivateUser(User user)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isActive = true;
                user.NumberOfWarnings = 0;
                context.Update(user);
                context.SaveChanges();
                sendEmailAboutReactivation(user);
            }
        }


        public string showUserNameWithStatus(User user)
        {
            string name = "";
            if (user.isDeleted == true)
            {
                name = "Deleted user";
            }
            if (user.isActive == true)
            {
                name = $"{user.FirstName} {user.LastName}";
            }
            if (user.isActive == false)
            {
                name = $"{user.FirstName} {user.LastName} - INACTIVATED";
            }
            return name;
        }


        public void deleteUser(User user)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isDeleted = true;
                user.isActive = false;
                user.FirstName = "Deleted user";
                user.LastName = "Deleted user";
                user.Location = "Deleted user";
                user.Contact = "Deleted user";
                user.Department = Department.SelectAll;
                user.PreferredLanguage = PreferredLanguage.SelectAll;
                user.Rating = 0;
                user.NumberOfWarnings = 0;
                user.LockoutEnd = DateTime.MaxValue;
                context.Update(user);
                context.SaveChanges();
            }
        }

        public bool isUserDeleted(string emailAddress)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                foreach (User u in context.Users.ToList<User>())
                {
                    if (u.Email == emailAddress && u.isDeleted == true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


    }
}
