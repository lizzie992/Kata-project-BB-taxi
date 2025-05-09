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
    }
}
