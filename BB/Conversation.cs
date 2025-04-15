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



        private List<Message>? _conversation;
        public List<Message>? conversation
        {
            get { return _conversation; }
            set { _conversation = value; }
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
