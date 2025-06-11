using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventExplorer
{
    public class Event
    {
		private int _id;

		public int ID
		{
			get { return _id; }
			set { _id = value; }
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

		private DateTime _startTime;

		public DateTime StartTime
		{
			get { return _startTime; }
			set { _startTime = value; }
		}

		private DateTime _endTime;

		public DateTime EndTime
		{
			get { return _endTime; }
			set { _endTime = value; }
		}


		private Location _location;

		public Location Location
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

		private string _category;

        public string Category
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

		public string URL
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

		// Could be genre sub-genre or cities
		private List<string> _tags;

		public List<string> Tags
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

		private EventType _type;

		public EventType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		private string? _originalID;

		public string OriginalId
		{
			get { return _originalID; }
			set { _originalID = value; }
		}

		private EventSource _eventSource;

		public EventSource Source
		{
			get { return _eventSource; }
			set { _eventSource = value; }
		}

	}
}
