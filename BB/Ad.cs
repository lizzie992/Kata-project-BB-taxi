using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class Ad
    {

        private List<Ad> _adList;

        public List<Ad> AdvertList
        {
            get { return _adList; }
            set { _adList = value; }
        }


        private AdType _adType;

		public AdType AdType
		{
			get { return _adType; }
			set { _adType = value; }
		}


		private Route _route; //no map yet just a simple text :)

		public Route Route
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

		

	}
}
