using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    public class Location
    {
		private string _streetNum;

		public string StreetNumber
		{
			get { return _streetNum; }
			set { _streetNum = value; }
		}

		private string _streetName;

		public string StreetName
		{
			get { return _streetName; }
			set { _streetName = value; }
		}

		private string _city;

		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		private string _postalCode;

		public string PostalCode
		{
			get { return _postalCode; }
			set { _postalCode = value; }
		}

		private string _venueName;

		public string VenueName
		{
			get { return _venueName; }
			set { _venueName = value; }
		}

        private int _eventLatitude;

        public int EventLatitude
        {
            get { return _eventLatitude; }
            set { _eventLatitude = value; }
        }

        private int _eventLongtitude;

		public int EventLongtitude
		{
			get { return _eventLongtitude; }
			set { _eventLongtitude = value; }
		}

	}
}
