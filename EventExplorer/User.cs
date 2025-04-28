using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    internal class User
    {
		private string _username;

		public string Username
		{
			get { return _username; }
			set { _username = value; }
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
