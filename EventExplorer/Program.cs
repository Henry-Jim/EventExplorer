using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventExplorer.DataProviders.TicketMaster;



namespace EventExplorer
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            // Create the service
            var ticketMasterService = new TicketMasterService();

            try
            {
                Console.WriteLine("Investigating location information in events...");

                // Get a few events to examine location data
                var testEvents = await ticketMasterService.GetEventsAsync(
                    countryCode: "US",
                    size: 5
                );

                // Debug location information
                Console.WriteLine("\nLocation details for found events:");
                Console.WriteLine("==================================");

                foreach (var evt in testEvents)
                {
                    Console.WriteLine($"\nEvent: {evt.Title}");

                    // Check if event has location data
                    if (evt.Location != null)
                    {
                        Console.WriteLine("Location data available:");
                        Console.WriteLine($"  Venue: {evt.Location.VenueName ?? "Not specified"}");
                        Console.WriteLine($"  Address Line 1: {evt.Location.AddressLine1 ?? "Not specified"}");
                        Console.WriteLine($"  Address Line 2: {evt.Location.AddressLine2 ?? "Not specified"}");
                        Console.WriteLine($"  City: {evt.Location.City ?? "Not specified"}");
                        Console.WriteLine($"  Postal Code: {evt.Location.PostalCode ?? "Not specified"}");
                        Console.WriteLine($"  Coordinates: {evt.Location.EventLatitude}, {evt.Location.EventLongtitude}");
                    }
                    else
                    {
                        Console.WriteLine("No location data available for this event!");
                    }
                }

                // Display events with formatted location information
                Console.WriteLine("\nDisplaying events with improved location format:");
                Console.WriteLine("===============================================");

                foreach (var evt in testEvents)
                {
                    DisplayEventWithFormattedLocation(evt);
                }

                // Now let's try different types of events to see location information
                Console.WriteLine("\nChecking location data for different event types...");

                // Check sports events
                var sportsEvents = await ticketMasterService.GetEventsAsync(
                    classificationName: "Sports",
                    size: 3
                );

                Console.WriteLine("\nSports Events:");
                foreach (var evt in sportsEvents)
                {
                    DisplayEventWithFormattedLocation(evt);
                }

                // Check music events
                var musicEvents = await ticketMasterService.GetEventsAsync(
                    classificationName: "Music",
                    size: 3
                );

                Console.WriteLine("\nMusic Events:");
                foreach (var evt in musicEvents)
                {
                    DisplayEventWithFormattedLocation(evt);
                }

                // Check events in specific cities
                var cityEvents = await ticketMasterService.GetEventsAsync(
                    city: "New York",
                    size: 3
                );

                Console.WriteLine("\nNew York Events:");
                foreach (var evt in cityEvents)
                {
                    DisplayEventWithFormattedLocation(evt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner error: {ex.InnerException.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Helper method to display an event with nicely formatted location
        private static void DisplayEventWithFormattedLocation(Event evt)
        {
            Console.WriteLine($"\nEvent: {evt.Title}");

            // Format date
            if (evt.StartTime > DateTime.MinValue)
                Console.WriteLine($"Date: {evt.StartTime:dd MMM yyyy, HH:mm}");
            else
                Console.WriteLine("Date: Not specified");

            // Format location in a user-friendly way
            if (evt.Location != null)
            {
                var location = evt.Location;

                // Venue name
                Console.WriteLine($"Venue: {location.VenueName ?? "Not specified"}");

                // Address
                var addressBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(location.AddressLine1))
                {
                    addressBuilder.Append(location.AddressLine1);

                    if (!string.IsNullOrEmpty(location.AddressLine2))
                    {
                        addressBuilder.Append(", ").Append(location.AddressLine2);
                    }

                    Console.WriteLine($"Address: {addressBuilder}");
                }

                // City and postal code
                var locationBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(location.City))
                {
                    locationBuilder.Append(location.City);

                    if (!string.IsNullOrEmpty(location.PostalCode))
                    {
                        locationBuilder.Append(", ").Append(location.PostalCode);
                    }

                    Console.WriteLine($"Location: {locationBuilder}");
                }
                else if (!string.IsNullOrEmpty(location.VenueName))
                {
                    Console.WriteLine($"Location: At {location.VenueName}");
                }
                else
                {
                    Console.WriteLine("Location: Not specified");
                }
            }
            else
            {
                Console.WriteLine("Location: Not available");
            }

            // Category
            Console.WriteLine($"Category: {evt.Category ?? "Not specified"}");

            // Price
            if (evt.MinPrice > 0)
                Console.WriteLine($"Starting Price: ${evt.MinPrice:F2}");
            else
                Console.WriteLine("Price: Visit event page for pricing details");

            // Event URL 
            if (!string.IsNullOrEmpty(evt.URL))
                Console.WriteLine($"URL: {evt.URL}");

            Console.WriteLine("---------------------------");
        }
    }
}