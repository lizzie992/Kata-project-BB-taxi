using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class Ad
    {

        private int _id;
        public int ID
        {

            get { return _id; }
            set { _id = value; }
        }

        private List<Ad> _adList;

        public List<Ad> AdvertList
        {
            get { return _adList; }
            set { _adList = value; }
        }

        private User _adOwner;
        public User AdOwner
        {
            get { return _adOwner; }
            set { _adOwner = value; }
        }


        private AdType _adType;

        public AdType AdType
        {
            get { return _adType; }
            set { _adType = value; }
        }


        private string _route; //no map yet just a simple text :)

        public string Route
        {
            get { return _route; }
            set { _route = value; }
        }

        private DateTime _pickUpDateAndTime;

        public DateTime pickUpDateAndTime
        {
            get { return _pickUpDateAndTime; }
            set { _pickUpDateAndTime = value; }
        }


        private int _numberOfSeats;

        public int NumberOfSeats
        {
            get { return _numberOfSeats; }
            set { _numberOfSeats = value; }
        }

        private string _specificRequests;

        public string SpecificRequests
        {
            get { return _specificRequests; }
            set { _specificRequests = value; }
        }

        public override string ToString()
        {
            return $"Ad type: {AdType}\r\n" +
                $"In the following route: {Route}\r\n" +
                $"On {pickUpDateAndTime}\r\n" +
                $"Available seats: {NumberOfSeats}\r\n" +
                $"Any speicifc requests: {SpecificRequests}\r\n";
        }

    }
}
