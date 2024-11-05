using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class AdList
    {
		private List<Ads> _adList;

		public List<Ads> AdvertList
		{
			get { return _adList; }
			set { _adList = value; }
		}

	}
}
