using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class RecurringAd
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

        private User? _adOwner;
        public User? AdOwner
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


        private AdDirection _adDirection;
        public AdDirection AdDirection
        {
            get { return _adDirection; }
            set { _adDirection = value; }
        }



        private string _pickUpDropOffLocation;

        [Required]
        public string PickUpDropOffLocation
        {
            get { return _pickUpDropOffLocation; }
            set { _pickUpDropOffLocation = value; }
        }


        private double? _latitude;

        [Range(00.0000000000001, 99.9999999999999, ErrorMessage = "Coordinates cannot be empty, please, move the marker to a valid location")]
        public double? Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }



        private double? _longitude;

        [Range(00.0000000000001, 99.9999999999999, ErrorMessage = "Coordinates cannot be empty, please, move the marker to a valid location")]
        public double? Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private List<DayOfWeek> _pickUpDay;

        public List<DayOfWeek> PickUpDay
        {
            get { return _pickUpDay; }
            set { _pickUpDay = value; }
        }


        private TimeOnly _pickUpTimeMonday;

        public TimeOnly PickUpTimeMonday
        {
            get { return _pickUpTimeMonday; }
            set { _pickUpTimeMonday = value; }
        }

        private TimeOnly _pickUpTimeTuesday;
        public TimeOnly PickUpTimeTuesday
        {
            get { return _pickUpTimeTuesday; }
            set { _pickUpTimeTuesday = value; }
        }

        private TimeOnly _pickUpTimeWednesday;
        public TimeOnly PickUpTimeWednesday
        {
            get { return _pickUpTimeWednesday; }
            set { _pickUpTimeWednesday = value; }
        }

        private TimeOnly _pickUpTimeThursday;
        public TimeOnly PickUpTimeThursday
        {
            get { return _pickUpTimeThursday; }
            set { _pickUpTimeThursday = value; }
        }

        private TimeOnly _pickUpTimeFriday;
        public TimeOnly PickUpTimeFriday
        {
            get { return _pickUpTimeFriday; }
            set { _pickUpTimeFriday = value; }
        }

        private int _numberOfSeats;

        [Range(1, 6, ErrorMessage = "You need to select the number of seats!")]
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


        public List<Conversation> _adConversations = new();
        public List<Conversation> adConversations
        {
            get { return _adConversations; }
            set { _adConversations = value; }
        }


    }
}
