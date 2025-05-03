using BB;
using Microsoft.EntityFrameworkCore;

namespace BaxiWebApp.Data
{
    public class DataService
    {

        IDbContextFactory<BaxiWebAppContext> _dbcFactory;
        public DataService(IDbContextFactory<BaxiWebAppContext> dbcFactory) //dependency injection of the DbContextFactory
        {
            _dbcFactory = dbcFactory;
        }

        public void sendMessage(User currentlyLoggedInUser, Conversation CurrentConversation, string TextMessage)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                Message message = new Message();
                message.fromUser = currentlyLoggedInUser;
                if (currentlyLoggedInUser == CurrentConversation.contactingUser)
                {
                    message.toUser = CurrentConversation.adOwnerUser;
                }
                if (currentlyLoggedInUser == CurrentConversation.adOwnerUser)
                {
                    message.toUser = CurrentConversation.contactingUser;
                }
                message.messageText = TextMessage;
                message.timeStamp = DateTime.Now;
                CurrentConversation.messages.Add(message);
                context.SaveChanges();
                TextMessage = "";
            }    

        }




    }
}
