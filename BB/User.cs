using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class User
    {
		private UserType _userType;

		public UserType UserType
		{
			get { return _userType; }
			set { _userType = value; }
		}


		private string _emailAddress;

		public string EmailAddress
		{
			get { return _emailAddress; }
			set { _emailAddress = value; }
		}


		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		private string _lastName;

		public string LastName
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

		private string _location;

		public string Location
		{
			get { return _location; }
			set { _location = value; }
		}

		private int _rating;

		public int Rating
		{
			get { return _rating; }
			set { _rating = value; }
		}

		private List<int> _listOfRatings;

		public List<int> ListOfRatings
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

        private string _contact;

        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
    }
}
