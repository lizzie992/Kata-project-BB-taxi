using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class User : IdentityUser
    {
        private UserType _userType;

        public UserType UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        //public CultureInfo? UserCulture { get; set; }


        public bool isActive
        {
            get; set;
        }

        public bool isDeleted
        {
            get; set;
        }


        private string? _emailAddress;

        public string? EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }


        private string? _firstName;

        public string? FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string? _lastName = "";

        public string? LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private Department _department;

        public Department Department
        {
            get { return _department; }
            set { _department = value; }
        }

        private PreferredLanguage _preferredLanguage;

        public PreferredLanguage PreferredLanguage
        {
            get { return _preferredLanguage; }
            set { _preferredLanguage = value; }
        }


        private int _rating;

        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        private List<int>? _listOfRatings;

        public List<int>? ListOfRatings
        {
            get { return _listOfRatings; }
            set { _listOfRatings = value; }
        }


        private int _numberOfWarnings;

        public int NumberOfWarnings
        {
            get { return _numberOfWarnings; }
            set { _numberOfWarnings = value; }
        }

        private string? _contact;

        public string? Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        private AdType _defaultAdType;
        public AdType DefaultAdType
        {
            get { return _defaultAdType; }
            set { _defaultAdType = value; }
        }

        private string _defaultPickUpAddress;
        public string DefaultPickUpAddress
        {
            get { return _defaultPickUpAddress; }
            set { _defaultPickUpAddress = value; }
        }

        private double? _defaultLatitude;
        public double? DefaultLatitude
        {
            get { return _defaultLatitude; }
            set { _defaultLatitude = value; }
        }

        private double? _defaultLongitude;
        public double? DefaultLongitude
        {
            get { return _defaultLongitude; }
            set { _defaultLongitude = value; }
        }

        private int _defaultNumberOfSeats;
        public int DefaultNumberOfSeats
        {
            get { return _defaultNumberOfSeats; }
            set { _defaultNumberOfSeats = value; }
        }

        private string _defaultSpecificRequests;
        public string DefaultSpecificRequests
        {
            get { return _defaultSpecificRequests; }
            set { _defaultSpecificRequests = value; }
        }

        private bool _areNotificationsOn;
        public bool AreNotificationsOn
        {
            get { return _areNotificationsOn; }
            set { _areNotificationsOn = value; }
        }

        private string _culture;
        public string Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }

    }
}
