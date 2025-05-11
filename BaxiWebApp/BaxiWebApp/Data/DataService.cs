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

        public void ReportConversation(Conversation C, User currentlyLoggedInUser)
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
                mailMessage.Body = $"Please check out the following conversation below that {currentlyLoggedInUser} reported to you: \r\n{message}";
                mailMessage.Subject = "A conversation was just reported to you";
                client.Send(mailMessage);

            }
        }

        public void reportAd(Ad ad, User currentlyLoggedInUser)
        {
            string adDetails = $"Ad owner: {showUserNameWithStatus(ad.AdOwner)}\r\nAd type: {ad.AdType}\r\nRoute: {ad.Route}\r\nPick up date and time: {ad.pickUpDateAndTime}\r\nNumber of seats: {ad.NumberOfSeats}\r\nSpecific requests: {ad.SpecificRequests}\r\n";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add("gulyaskata99@gmail.com");
            mailMessage.Body = $"Please check out the following ad below that {showUserNameWithStatus(currentlyLoggedInUser)} reported to you: \r\n{adDetails}\r\nOpen the ad here: LINK";
            mailMessage.Subject = "An ad was just reported to you";
            client.Send(mailMessage);
        }




        public void sendEmailNewAd(Ad ad)
        {
            string message = $"You might be interested in this new Ad:\r\n Ad type: {ad.AdType}\r\n In the following route: {ad.Route}\r\n On {ad.pickUpDateAndTime}\r\n Available seats: {ad.NumberOfSeats}\r\n Any speicifc requests: {ad.SpecificRequests}\r\n";
            if (ad.Route.Contains("Sendling"))
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
                client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
                mailMessage.To.Add("gulyaskata99@gmail.com");
                mailMessage.Body = message;
                mailMessage.Subject = "Check out this new ad: ";
                client.Send(mailMessage);
            }
        }

        public void giveUserAWarning(User user)
        {
            int maxNumberOfWarnings = 3;
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.NumberOfWarnings++;
                if (user.NumberOfWarnings == maxNumberOfWarnings)
                {
                    inactivateUser(user);
                    context.SaveChanges();
                }
                context.Update(user);
                context.SaveChanges();
            }
        }

        public void inactivateUser(User user)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isActive = false;
                context.Update(user);
                context.SaveChanges();
            }
        }

        public void reactivateUser(User user)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isActive = true;
                context.Update(user);
                context.SaveChanges();
            }
        }


        public string showUserNameWithStatus(User user)
        {
            string name = "";
            if (user.isActive == true)
            {
                name = $"{user.FirstName} {user.LastName}";
            }
            if (user.isActive  == false)
            {
                name = $"{user.FirstName} {user.LastName} - INACTIVATED";
            }
            return name;
        }


    }
}
