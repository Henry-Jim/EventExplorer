using System.Threading.Tasks;
using EventExplorer;
using EventExplorer.DataProviders.TicketMaster;
using Microsoft.EntityFrameworkCore;

namespace EventExplorerWebApp.Data
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }


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

        // Basic CRUD operations

        // Get all events
        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Location)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        // Get event by ID
        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        // Add a new event (works for user-created event or any source)
        public async Task AddEventAsync(Event newEvent)
        {
            // Set default source if not specified
            if (newEvent.Source == 0)
            {
                newEvent.Source = EventSource.Internal; // Assuming 0 is the default for internal events
            }

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
        }

        // Add multiple events (for bulk imports)
        public async Task AddEventsAsync(List<Event> events)
        {
            foreach (Event e in events)
            {
                // Set default source if not specified
                if (e.Source == 0)
                {
                    e.Source = EventSource.Internal; // Assuming 0 is the default for internal events
                }
            }

            _context.Events.AddRange(events);
            await _context.SaveChangesAsync();
            
        }

        // Update an existing event using EF's built-in method
        public async Task<Event?> UpdateEventAsync(Event updatedEvent)
        {
            var exisitngEvent = await _context.Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(e => e.ID == updatedEvent.ID);

            if (exisitngEvent == null)
            {
                throw new KeyNotFoundException($"Event with ID {updatedEvent.ID} not found.");
            }

            // Entity Framework automatically map all scalar properties
            _context.Entry(exisitngEvent).CurrentValues.SetValues(updatedEvent);

            // Handle navigation property (Location) separately if needed
            if (updatedEvent.Location != null)
            {
                if (exisitngEvent.Location == null)
                {
                    exisitngEvent.Location = updatedEvent.Location;
                }
                else
                {
                    _context.Entry(exisitngEvent.Location).CurrentValues.SetValues(updatedEvent.Location);
                }
            }
            else
            {
                // if updated event does not have a location, we can set it to null
                exisitngEvent.Location = null;
            }

            await _context.SaveChangesAsync();
            return exisitngEvent;
        }

        // We could also use simple complete update (for disconnected scenarios) e.g. Ticketmaster
        public async Task<Event?> UpdateEventCompleteAsync(Event updatedEvent)
        {
            var exists = await _context.Events.AnyAsync(e => e.ID == updatedEvent.ID);
            if (!exists)
            {
                return null;
            }

            // EF handles entire update automatically
            _context.Events.Update(updatedEvent);
            await _context.SaveChangesAsync();
            return updatedEvent;
        }

        // Delete an event by ID
        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete == null)
            {
                return false; // Event not found
            }

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if an event exists by original ID and source (prevents duplicates)
        public async Task<bool> EventExistsAsync(int originalId, EventSource source)
        {
            return await _context.Events.AnyAsync(e => e.OriginalId == originalId.ToString() && e.Source == source);
        }

        // Search and Filter Methods

        // Search events by keyword
        public async Task<List<Event>> SearchEventsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await GetAllEventsAsync(); // Return all events if no keyword is provided
            }

            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.Title.Contains(keyword) ||
                            (e.Description != null && e.Description.Contains(keyword)) ||
                            (e.Location != null && e.Location.VenueName != null && e.Location.VenueName.Contains(keyword)) ||
                            (e.Location != null && e.Location.City != null && e.Location.City.Contains(keyword)))
                .OrderBy(e => e.StartTime)
                .ToListAsync();

        }

        // Get events by category
        public async Task<List<Event>> GetEventsByCategoryAsync(string category)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.Category == category)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        // Get events by city
        public async Task<List<Event>> GetEventByCityAsync(string city)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.Location != null && e.Location.City == city)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        // Get events by source (e.g., TicketMaster, Internal etc.)
        public async Task<List<Event>> GetEventBySourceAsync(EventSource source)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.Source == source)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        // Get events by date range
        public async Task<List<Event>> GetEventsInDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.StartTime >= startDate && e.EndTime <= endDate)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        // Get upcoming events 
        public async Task<List<Event>> GetUpcomingEventsAsync(int count = 20)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.StartTime > DateTime.Now)
                .OrderBy(e => e.StartTime)
                .Take(count)
                .ToListAsync();
        }

        // Get events by type (Online, Offline, Hybrid)
        public async Task<List<Event>> GetEventsByTypeAsync(EventType eventType)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.Type == eventType)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        // Get Family Friendly events
        public async Task<List<Event>> GetFamilyFriendlyEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Location)
                .Where(e => e.IsFamilyFriendly)
                .OrderBy(e => e.StartTime)
                .ToListAsync();

        }

        // Proximity search by city enum (simple city-based search)
        public async Task<List<Event>> SearchByCityAsync(City city, EventType? eventType = null)
        {
            string cityName = city.ToString();

            var query = _context.Events
                .Include(e => e.Location)
                .Where(e => e.Location != null && e.Location.City.Contains(cityName));

            if (eventType.HasValue)
            {
                query = query.Where(e => e.Type == eventType.Value);
            }

            return await query.OrderBy(e => e.StartTime).ToListAsync();

            throw new NotImplementedException("SearchByCityAsync method is not implemented yet.");
        }

        public List<Event> SearchByProximity(City city, double radius, EventType? eventType = null)
        {
            throw new NotImplementedException("SearchByProximity method is not implemented yet.");
        }


    }
}
