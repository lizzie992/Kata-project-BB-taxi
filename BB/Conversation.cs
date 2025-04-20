using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        private List<Message>? _messages;
        public List<Message>? messages 
        {
            get { return _messages; }
            set { _messages = value; }
        }


        public User _adOwnerUser;
        public User adOwnerUser
        {
            get { return _adOwnerUser; }
            set { _adOwnerUser = value; }
        }


        public User _contactingUser;
        public User contactingUser
        {
            get { return _contactingUser; }
            set { _contactingUser = value; }
        }


    }
}
