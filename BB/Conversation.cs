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

        public int AdID { get; set; }

        public int ID
        {

            get { return _id; }
            set { _id = value; }
        }

        public List<Message>? messages
        {
            get; set;
        }


        private User? _adOwnerUser;
        public User? adOwnerUser
        {
            get { return _adOwnerUser; }
            set { _adOwnerUser = value; }
        }


        private User? _contactingUser;
        public User? contactingUser
        {
            get { return _contactingUser; }
            set { _contactingUser = value; }
        }


        public Message? MostRecentMessage
        {
            get
            {
                if (messages is null)
                {
                    return null;
                }
                if (messages.Count == 0)
                {
                    return null;
                }
                else
                {
                    return messages.OrderByDescending(m => m.timeStamp).First();
                }

            }
        }


        private DateTime? _timeStamp;
        public DateTime? TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

    }
}
