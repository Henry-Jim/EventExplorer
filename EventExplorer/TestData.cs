using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExplorer
{
    public static class TestData
    {
        public static List<Event> GenerateEvents()
        {
            List<Event> result = new List<Event>();

            Event e1 = new Event();
            e1.EventID = 1;
            e1.Title = "Kendrick Lamar Concert";
            e1.Aliases = new List<string> { "K.Dot", "Kung Fu Kenny" };
            e1.Description = "A concert by Kendrick Lamar.";
            e1.StartTime = new DateTime(2025, 10, 15, 20, 0, 0);
            e1.EventLocation = new Location
            {
                StreetNumber = "123",
                StreetName = "Main St",
                City = "Los Angeles",
                PostalCode = "90001",
                VenueName = "Staples Center",
                EventLatitude = 34043,
                EventLongtitude = -118265
            };
            e1.MinPrice = 50.0f;
            e1.EventCategory = Category.Arts;
            e1.EventType = EventType.Offline;
            e1.EventTags = new List<string> { "Hip-Hop", "Concert", "Live" };
            e1.ImageURL = "https://example.com/image1.jpg";
            e1.EventURL = "https://example.com/event1";
            e1.AccessibilityInfo = "Wheelchair space accessible";
            e1.IsFamilyFriendly = false;

            Event e2 = new Event();
            e2.EventID = 2;
            e2.Title = "F1 British Grand Prix";
            e2.Aliases = new List<string> { "British GP", "F1 Silverstone" };
            e2.Description = "Get up to speed with everything you need to know about the 2025 British Grand Prix, which takes place over 52 laps of the 5.891-kilometre Silverstone Circuit on Sunday, July 6.";
            e2.StartTime = new DateTime(2025, 7, 6, 15, 0, 0);
            e2.EventLocation = new Location
            {
                StreetNumber = "1",
                StreetName = "Silverstone Circuit",
                City = "Northamptonshire",
                PostalCode = "NN12 8TN",
                VenueName = "Silverstone Circuit",
                EventLatitude = 52073,
                EventLongtitude = -0880
            };
            e2.MinPrice = 100.0f;
            e2.EventCategory = Category.Sports;
            e2.EventType = EventType.Offline;
            e2.EventTags = new List<string> { "Motor Racing", "F1", "Grand Prix", "Motor Sport" };
            e1.ImageURL = "https://example.com/image2.jpg";
            e2.EventURL = "https://example.com/event2";
            e2.AccessibilityInfo = "Wheelchair space accessible";
            e2.IsFamilyFriendly = true;

            Event e3 = new Event();
            e3.EventID = 3;
            e3.Title = "Hackathon 2025";
            e3.Aliases = new List<string> { "Hackathon", "Coding Challenge" };
            e3.Description = "A 48-hour hackathon where developers come together to create innovative solutions.";
            e3.StartTime = new DateTime(2025, 11, 20, 9, 0, 0);
            e3.EventLocation = new Location
            {
                StreetNumber = "456",
                StreetName = "Tech Ave",
                City = "San Francisco",
                PostalCode = "94105",
                VenueName = "Tech Convention Center",
                EventLatitude = 37774,
                EventLongtitude = -122419,
            };
            e3.MinPrice = 0.0f;
            e3.EventCategory = Category.Technology;
            e3.EventType = EventType.Hybrid;
            e3.EventTags = new List<string> { "Coding", "Hackathon", "Innovation" };
            e3.ImageURL = "https://example.com/image3.jpg";
            e3.EventURL = "https://example.com/event3";
            e3.AccessibilityInfo = "Elevator and Wheelchair accessible";
            e3.IsFamilyFriendly = true;

            result.Add(e1);
            result.Add(e2);
            result.Add(e3);

            return result;
        }
    }
}
