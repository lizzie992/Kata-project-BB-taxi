using BB;
using Microsoft.AspNetCore.Identity;
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

        public void SendMessage(Conversation C, User currentlyLoggedInUser, string TextMessage)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                Conversation CurrentConversation = getCurrentConversation(C.ID);
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
                context.Update(getCurrentConversation(C.ID));
                context.SaveChanges();
                TextMessage = "";
            }

        }

        public Conversation? getCurrentConversation(int ID)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                var result = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
                return result;
            }
        }

    }
}
