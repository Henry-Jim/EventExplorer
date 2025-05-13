using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventExplorer;

namespace EventExplorer.DataProviders.TicketMaster
{
    public class TicketMasterConverter
    {
        public static List<Event> ConvertEvents(TicketMasterResponse tmr)
        {
            List<Event> eventList = new List<Event>();

            if (tmr?.Embedded?.Events == null || tmr.Embedded.Events.Count == 0)
                return eventList;

            foreach (var tme in tmr.Embedded.Events)
            {
                Event myEvent = ConvertSingleEvent(tme);
                eventList.Add(myEvent);
            }

            return eventList;
        }

        private static Event ConvertSingleEvent(TicketMasterEvent tme)
        {
            // Create a new Event object
            Event myEvent = new Event();

            // Set basic properties
            myEvent.EventID = !string.IsNullOrEmpty(tme.Id) ? Convert.ToInt32(tme.Id.GetHashCode()) : 0;
            myEvent.Title = tme.Name;
            myEvent.Description = tme.Info;
            myEvent.EventURL = tme.URL;

            // Set the original ID
            myEvent.OriginalID = tme.Id;

            // Set the start and end times
            if (tme.Dates?.Start != null)
            {
                Console.WriteLine($"Raw start date: {tme.Dates.Start.DateTime}");

                if (!string.IsNullOrEmpty(tme.Dates.Start.DateTime))
                {
                    bool success = System.DateTime.TryParse(tme.Dates.Start.DateTime, out DateTime startDateTime);
                    Console.WriteLine($"Parse success: {success}, Parsed date: {startDateTime}");
                    myEvent.StartTime = startDateTime;
                }
                else
                {
                    Console.WriteLine("Start date is empty");
                    myEvent.StartTime = DateTime.MinValue;
                }
            }

            // Set the location
            if (tme.Embedded?.Venues != null && tme.Embedded.Venues.Count > 0)
            {
                var venue = tme.Embedded.Venues[0];
                EventExplorer.Location location = new EventExplorer.Location();

                // Parse address if available
                if (!string.IsNullOrEmpty(venue.Address?.Line1))
                {
                    location.AddressLine1 = venue.Address.Line1;
                }

                // AddressLine2 can be left empty as it's not provided by TicketMaster
                location.AddressLine2 = string.Empty;

                location.City = venue.City?.Name;
                location.VenueName = venue.Name;
                location.PostalCode = venue.PostalCode;

                // Convert latitude and longitude to int if available
                if (venue.Location != null)
                {
                    if (!string.IsNullOrEmpty(venue.Location.Latitude) && float.TryParse(venue.Location.Latitude, out float lat))
                    {
                        location.EventLatitude = (int)(lat * 1000000); // Convert to int
                    }
                    if (!string.IsNullOrEmpty(venue.Location.Longitude) && float.TryParse(venue.Location.Longitude, out float lon))
                    {
                        location.EventLongtitude = (int)(lon * 1000000); // Convert to int
                    }
                }

                myEvent.EventLocation = location;
            }

            // Set category based on classifications
            if (tme.Classifications != null && tme.Classifications.Count > 0)
            {
                // Find primary classification if available, otherwise use the first one
                var primaryClassification = tme.Classifications.FirstOrDefault(c => c.Primary)
                                          ?? tme.Classifications.FirstOrDefault();

                if (primaryClassification != null)
                {
                    myEvent.EventCategory = primaryClassification.Segment?.Name;

                    // Create tags from genre and subgenre
                    List<string> tags = new List<string>();
                    if (!string.IsNullOrEmpty(primaryClassification.Genre?.Name))
                    {
                        tags.Add(primaryClassification.Genre.Name);
                    }
                    if (!string.IsNullOrEmpty(primaryClassification.SubGenre?.Name))
                    {
                        tags.Add(primaryClassification.SubGenre.Name);
                    }

                    myEvent.EventTags = tags;

                    // Set family friendly flag
                    myEvent.IsFamilyFriendly = primaryClassification.Family;
                }
            }

            // Set image URL if available
            if (tme.Images != null && tme.Images.Count > 0)
            {
                // Try to find the best quality image
                var bestImage = tme.Images.OrderByDescending(img => img.Width * img.Height).FirstOrDefault(); // Sort by size

                myEvent.ImageURL = bestImage?.URL;
            }

            // Accessibility info
            myEvent.AccessibilityInfo = tme.Accessibility?.Info;

            // Aliases if available
            myEvent.Aliases = tme.Aliases ?? new List<string>();

            // Minimum price if available
            myEvent.MinPrice = tme.PriceRanges?.FirstOrDefault()?.Min ?? 0;

            // Event type (default to Offline for physical events)
            myEvent.EventType = EventType.Offline;

            return myEvent;
        }

    }
}
