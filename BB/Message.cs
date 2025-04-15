using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class Message
    {
        public int Id { get; set; }

        public int ConversationID { get; set; }

        private User _fromUser;
        public User fromUser
        {
            get { return _fromUser; }
            set { _fromUser = value; }
        }

        private User _toUser;
        public User toUser
        {
            get { return _toUser; }
            set { _toUser = value; }
        }

        private string _messageText;
        public string messageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        private DateTime _timeStamp;
        public DateTime timeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }


    }
}
