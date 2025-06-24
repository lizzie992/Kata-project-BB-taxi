using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        private AdDirection _adDirection;
        public AdDirection AdDirection
        {
            get { return _adDirection; }
            set { _adDirection = value; }
        }


        
        private string? _pickUpDropOffLocation;

        [Required]
        public string? PickUpDropOffLocation
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



        private DateTime _pickUpDateAndTime;

        public DateTime PickUpDateAndTime
        {
            get { return _pickUpDateAndTime; }
            set { _pickUpDateAndTime = value; }
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

        public override string ToString()
        {
            return $"Ad type: {AdType}\r\n" +
                $"Direction: {AdDirection}\r\n" +
                $"From/to the following address: {PickUpDropOffLocation}\r\n" +
                $"On {PickUpDateAndTime}\r\n" +
                $"Available seats: {NumberOfSeats}\r\n" +
                $"Any speicifc requests: {SpecificRequests}\r\n";
        }



        public List<Conversation> _adConversations = new();
        public List<Conversation> adConversations
        {
            get { return _adConversations; }
            set { _adConversations = value; }
        }


    }
}
