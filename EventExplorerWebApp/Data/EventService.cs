using EventExplorer;
using EventExplorer.DataProviders.TicketMaster;

namespace EventExplorerWebApp.Data
{
    public class EventService : IEventService
    {
        private readonly EventContext _context;
        private readonly TicketMasterService _ticketMasterService;

        public EventService(EventContext context, TicketMasterService ticketMasterService)
        {
            _context = context;
            _ticketMasterService = ticketMasterService;
        }

        // Get events from API
        public async Task<List<Event>> GetEventsFromApiAsync(string keyword = null, string city = null, string category = null, int size = 20)
        {
            var allApiEvents = new List<Event>();

            try
            {
                // Get from TicketMaster API
                var ticketMasterEvents = await _ticketMasterService.GetEventsAsync(
                    keyword: keyword,
                    classificationName: category,
                    city: city,
                    size: size
                    );
                allApiEvents.AddRange(ticketMasterEvents);

                // Future: Get from other APIs
                // var eventbriteEvents = await _eventbriteService.GetEventsAsync(...);
                // allApiEvents.AddRange(eventbriteEvents);

                // save all new API events to database
                foreach (var apiEvent in allApiEvents)
                {
                    await SaveEventIfNewAsync(apiEvent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching from APIs: {ex.Message}");
                return new List<Event>();
            }
        }
    }
}
