using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    public class Event
    {
		private string _eventID;

		public string EventID
		{
			get { return _eventID; }
			set { _eventID = value; }
		}

		private string _title;

		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private DateTime _date;

		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}

		private Location _location;

		public Location EventLocation
		{
			get { return _location; }
			set { _location = value; }
		}

	}
}
