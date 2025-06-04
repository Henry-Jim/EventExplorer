using EventExplorer;

namespace EventExplorerWebApp.Data
{
    // Interface for event service to handle event-related operations
    public interface IEventService
    {
        // Gets events from API
        Task<List<Event>> GetEventsFromApiAsync(string keyword = null, string city = null, string category = null, int size = 20);

        // Database operations
        Task<List<Event>> GetEventsFromDatabaseAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task SaveEventAsync(Event eventToSave);
        Task<List<Event>> SearchEventsAsync(string searchTerm);

        // Combined operations
        Task<List<Event>> GetEventsAsync(bool useCache = true, string keyword = null, string city = null, string category = null);


        // User specific methods
        Task<List<Event>> GetEventsByUserAsync(string userId);
        Task<Event> CreatedUserEventAsync(Event newEvent, string userId);
        Task UpdateUserEventAsync(Event updatedEvent, string userId);
        Task DeleteUserEventAsync(int eventId, string userId);

        // Social features
        Task<List<Event>> GetPopularEventsAsync();
        Task<List<Event>> GetEventsNearLocationAsync(string city);
        Task<bool> CanUserEditEventAsync(int eventId, string userId);

        // NEW - Methods for User Preferences --------------------------

        // Save/Unsave events (for User.SavedEvents)
        Task SaveEventForUserAsync(int eventId, string userId);
        Task UnsaveEventForUserAsync(int eventId, string userId);
        Task<List<Event>> GetSavedEventsByUserAsync(string userId);

        // Liked/Unliked events (for User.LikedEvents)
        Task LikeEventAsync(int eventId, string userId);
        Task UnlikeEventAsync(int eventId, string userId);
        Task<List<Event>> GetLikedEventsByUserAsync(string userId);

        // Favourite/Unfavourite events (for User.FavouriteEvents)
        Task FavoriteEventAsync(int eventId, string userId);
        Task UnfavoriteEventAsync(int eventId, string userId);
        Task<List<Event>> GetFavoriteEventsByUserAsync(string userId);

        // User Posted Events (for User.PostedEvents)
        Task<List<Event>> GetPostedEventsByUserAsync(string userId);

        // Check User Event status
        Task<bool> IsEventSavedByUserAsync(int eventId, string userId);
        Task<bool> IsEventLikedByUserAsync(int eventId, string userId);
        Task<bool> IsEventFavoritedByUserAsync(int eventId, string userId);

        // Utility method to avoid duplicate events from API
        Task SaveEventIfNewAsync(Event eventToSave);
    }
}
