using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    internal class User
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private List<City> _preferredLocations;

		public List<City> PreferredLocations
		{
			get { return _preferredLocations; }
			set { _preferredLocations = value; }
		}

		private List<Category> _preferredCategories;

		public List<Category> PreferredCategories
		{
			get { return _preferredCategories; }
			set { _preferredCategories = value; }
		}

		private List<string> _savedEvents;

		public List<string> SavedEvents
		{
			get { return _savedEvents; }
			set { _savedEvents = value; }
		}

	}
}
