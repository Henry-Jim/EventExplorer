using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    internal class User
    {
		private int _userID;

		public int UserID
		{
			get { return _userID; }
			set { _userID = value; }
		}

		private string _username;

		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		private string _userEmail;

		public string UserEmail
		{
			get { return _userEmail; }
			set { _userEmail = value; }
		}

		private string userPassword;

		public string UserPassword
		{
			get { return userPassword; }
			set { userPassword = value; }
		}

		private List<City> _preferredLocation;

		public List<City> PreferredLocation
		{
			get { return _preferredLocation; }
			set { _preferredLocation = value; }
		}

		private List<Category> _preferredCategory;

		public List<Category> PreferredCategory
		{
			get { return _preferredCategory; }
			set { _preferredCategory = value; }
		}

		private List<string> _savedEvents;

		public List<string> SavedEvents
		{
			get { return _savedEvents; }
			set { _savedEvents = value; }
		}

	}
}
