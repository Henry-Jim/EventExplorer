using EventExplorer;

namespace EventExplorerWebApp.Data
{
    public class DataService
    {
		private List<Event> _events;

		public List<Event> Events
		{
			get { return _events; }
			set { _events = value; }
		}

		public void addEvent(Event e)
		{
			_events.Add(e);
		}

	}
}
