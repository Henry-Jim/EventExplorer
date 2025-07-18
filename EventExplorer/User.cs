﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    public class User
    {
		private int _id;

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}


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

		private List<Event> _savedEvents;

		public List<Event> SavedEvents
		{
			get { return _savedEvents; }
			set { _savedEvents = value; }
		}

		private List<Event> _likedEvents;

		public List<Event> LikedEvents
		{
			get { return _likedEvents; }
			set { _likedEvents = value; }
		}

		private List<Event> _postedEvents;

		public List<Event> PostedEvents
		{
			get { return _postedEvents; }
			set { _postedEvents = value; }
		}

		private List<Event> _favouriteEvents;

		public List<Event> FavouriteEvents
		{
			get { return _favouriteEvents; }
			set { _favouriteEvents = value; }
		}



	}
}
