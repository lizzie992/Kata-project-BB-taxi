using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BB
{
    public class Conversation
    {

        private int _id;
        public int ID
        {

            get { return _id; }
            set { _id = value; }
        }

        private int _AdID;
        public int AdID
        {

            get { return _AdID; }
            set { _AdID = value; }
        }


        public List<Message>? messages
        {
            get;set;
        }


        private User _adOwnerUser;
        public User adOwnerUser
        {
            get { return _adOwnerUser; }
            set { _adOwnerUser = value; }
        }


        private User _contactingUser;
        public User contactingUser
        {
            get { return _contactingUser; }
            set { _contactingUser = value; }
        }


        public Message MostRecentMessage
        {
            get
            {
                return messages.OrderByDescending(m => m.timeStamp).First();
            }
        }


    }
}
