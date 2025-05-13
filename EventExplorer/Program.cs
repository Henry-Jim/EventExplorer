using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EventExplorer.DataProviders.TicketMaster;

namespace EventExplorer
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var ticketMasterService = new TicketMasterService();

            try
            {
                Console.WriteLine("Searching for events in the US...");
                Console.WriteLine("Please wait while we fetch the data...");

                // Simple search for events in the US
                var usEvents = await ticketMasterService.GetEventsAsync(
                    countryCode: "UK",
                    size: 10  // Fetch up to 10 events
                );

                // Display the results
                Console.WriteLine($"\nFound {usEvents.Count} events in the US:");
                Console.WriteLine("=======================================");

                if (usEvents.Count > 0)
                {
                    foreach (var evt in usEvents)
                    {
                        Console.WriteLine($"Event: {evt.Title}");

                        // Check if date is valid before formatting
                        if (evt.StartTime > DateTime.MinValue)
                            Console.WriteLine($"Date: {evt.StartTime.ToString("dd MMMM yyyy, HH:mm")}");
                        else
                            Console.WriteLine("Date: Not specified");

                        Console.WriteLine($"Venue: {evt.EventLocation?.VenueName ?? "Not specified"}");
                        Console.WriteLine($"Location: {evt.EventLocation?.City ?? "Not specified"}");
                        Console.WriteLine($"Category: {evt.EventCategory ?? "Not specified"}");

                        if (evt.MinPrice > 0)
                            Console.WriteLine($"Starting Price: ${evt.MinPrice:F2}");

                        if (!string.IsNullOrEmpty(evt.EventURL))
                            Console.WriteLine($"URL: {evt.EventURL}");

                        Console.WriteLine("----------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No events found in the US. This is unexpected.");

                    // Try without country code as a fallback
                    Console.WriteLine("\nTrying without country filter...");

                    var anyEvents = await ticketMasterService.GetEventsAsync(size: 5);

                    Console.WriteLine($"\nFound {anyEvents.Count} events (any country):");

                    if (anyEvents.Count > 0)
                    {
                        foreach (var evt in anyEvents)
                        {
                            Console.WriteLine($"Event: {evt.Title}");
                            Console.WriteLine($"Location: {evt.EventLocation?.City ?? "Not specified"}, {evt.EventLocation?.VenueName ?? "No venue"}");
                            Console.WriteLine("----------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No events found at all. There may be an issue with the API connection.");
                    }
                }

                // Let's also try a keyword search as an additional test
                Console.WriteLine("\nTrying a keyword search for 'basketball'...");
                var basketballEvents = await ticketMasterService.GetEventsAsync(
                    keyword: "basketball",
                    size: 5
                );

                Console.WriteLine($"\nFound {basketballEvents.Count} basketball events:");
                if (basketballEvents.Count > 0)
                {
                    foreach (var evt in basketballEvents)
                    {
                        Console.WriteLine($"Event: {evt.Title}");
                        Console.WriteLine($"Location: {evt.EventLocation?.City ?? "Not specified"}, {evt.EventLocation?.VenueName ?? "No venue"}");
                        Console.WriteLine("----------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //public static List<Event> Convert(TicketmasterResponse tmr)
        //{
        //    List<Event> events = new List<Event>();
        //    foreach (var tme in tmr.Embedded.Events)
        //    {
        //        Event ev = new Event
        //        {
        //            Title = tme.Name,
        //            Description = tme.Info,
        //            //StartTime = DateTime.Parse(tme.Dates.Start.DateTime),
        //            //EndTime = DateTime.Parse(tme.Dates.End.DateTime),
        //            Aliases = new List<string> { tme.Id }
        //        };
        //        events.Add(ev);
        //    }
        //    return events;
        //}
    }
}
