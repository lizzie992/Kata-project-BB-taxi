using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class Ads
    {
		private int _adType;

		public int AdType
		{
			get { return _adType; }
			set { _adType = value; }
		}


		private DirectionOfTravel _direction;

		public DirectionOfTravel Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		private string _route; //no map yet just a simple text :)

		public string Route
		{
			get { return _route; }
			set { _route = value; }
		}

		private DateTime _pickUpTime;

		public DateTime pickUpTime
		{
			get { return _pickUpTime; }
			set { _pickUpTime = value; }
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

		private string _contact;

		public string Contact
		{
			get { return _contact; }
			set { _contact = value; }
		}

	}
}
