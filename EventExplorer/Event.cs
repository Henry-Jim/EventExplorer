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
		private int _eventID;

		public int EventID
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

		private List<string> _aliases;

		public List<string> Aliases
		{
			get { return _aliases; }
			set { _aliases = value; }
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

		private DateTime _time;

		public DateTime Time
		{
			get { return _time; }
			set { _time = value; }
		}


		private Location _location;

		public Location EventLocation
		{
			get { return _location; }
			set { _location = value; }
		}

		private float _minPrice;

		public float MinPrice
		{
			get { return _minPrice; }
			set { _minPrice = value; }
		}

		private bool _isFree;

		public bool IsFree
		{
			get { return _isFree; }
			set { _isFree = value; }
		}

		private Category _category;

        public Category EventCategory
		{
			get { return _category; }
            set { _category = value; }
        }

		private string _imageURL;

		public string ImageURL
		{
			get { return _imageURL; }
			set { _imageURL = value; }
		}

		private string _eventURL;

		public string EventURL
		{
			get { return _eventURL; }
			set { _eventURL = value; }
		}

		private string _accessibilityInfo;

		public string AccessibilityInfo
		{
			get { return _accessibilityInfo; }
			set { _accessibilityInfo = value; }
		}

		private List<string> _tags;

		public List<string> EventTags
		{
			get { return _tags; }
			set { _tags = value; }
		}

		private bool _isFamilyFriendy;

		public bool IsFamilyFriendly
		{
			get { return _isFamilyFriendy; }
			set { _isFamilyFriendy = value; }
		}

		private Type _type;

		public Type EventType
		{
			get { return _type; }
			set { _type = value; }
		}

	}
}
