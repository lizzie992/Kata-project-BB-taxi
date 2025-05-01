using BB;

namespace BaxiWebApp.Data
{
    public class DataService
    {

        public void sendMessage(User currentlyLoggedInUser, Conversation CurrentConversation, string TextMessage)
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
        }




    }
}
