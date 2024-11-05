using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class Users
    {
		private UserTypes _userType;

		public UserTypes UserType
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

		private string _password;

		public string Password
		{
			get { return _password; }
			set { _password = value; }
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

		private Departments _department;

		public Departments Department
		{
			get { return _department; }
			set { _department = value; }
		}

		private PreferredLanguages _preferredLanguage;

		public PreferredLanguages PreferredLanguage
        {
			get { return _preferredLanguage; }
			set { _preferredLanguage = value; }
		}

		private Locations _location;

		public Locations Location
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

		private int _numberOfWarnings;

		public int NumberOfWarnings
        {
			get { return _numberOfWarnings; }
			set { _numberOfWarnings = value; }
		}

	}
}
