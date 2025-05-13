using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Net;
using EventExplorer;

namespace EventExplorer.DataProviders.TicketMaster
{
    // Root response object for Ticketmaster API
     public class TicketMasterResponse
    {
        [JsonPropertyName("_embedded")]
        public TicketMasterEmbedded Embedded { get; set; }

        [JsonPropertyName("_links")]
        public TicketMasterLinks Links { get; set; }

        [JsonPropertyName("page")]
        public TicketMasterPage Page { get; set; }
    }

    public class TicketMasterLinks
    {
        [JsonPropertyName("self")]
        public TicketMasterLink Self { get; set; }

        [JsonPropertyName("first")]
        public TicketMasterLink First { get; set; }

        [JsonPropertyName("next")]
        public TicketMasterLink Next { get; set; }

        [JsonPropertyName("last")]
        public TicketMasterLink Last { get; set; }

        [JsonPropertyName("prev")]
        public TicketMasterLink Prev { get; set; }
    }

    public class TicketMasterLink
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

    public class TicketMasterPage
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("totalElements")]
        public int TotalElements { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }
    }

    public class TicketMasterEmbedded
    {
        [JsonPropertyName("events")]
        public List<TicketMasterEvent> Events { get; set; }
    }

    public class TicketMasterEvent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("classifications")]
        public List<TicketMasterClassification> Classifications { get; set; }

        [JsonPropertyName("_embedded")]
        public TicketMasterVenueEmbedded Embedded { get; set; }

        [JsonPropertyName("images")]
        public List<TicketMasterImage> Images { get; set; }

        [JsonPropertyName("dates")]
        public TicketMasterDates Dates { get; set; }

        [JsonPropertyName("accessibility")]
        public TicketMasterAccessibility Accessibility { get; set; }

        [JsonPropertyName("ticketLimit")]
        public TicketMasterTicketLimit TicketLimit { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> Aliases { get; set; }

        [JsonPropertyName("priceRanges")]
        public List<TicketMasterPriceRange> PriceRanges { get; set; }
    }

    public class TicketMasterPriceRange
    {
        [JsonPropertyName("min")]
        public float Min { get; set; }

        [JsonPropertyName("max")]
        public float Max { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class TicketMasterImage
    {
        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("ratio")]
        public string Ratio { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class TicketMasterDates
    {
        [JsonPropertyName("start")]
        public TicketMasterStart Start { get; set; }

        [JsonPropertyName("end")]
        public TicketMasterEnd End { get; set; }
    }

    public class TicketMasterStart
    {
        [JsonPropertyName("localDate")]
        public string LocalDate { get; set; }

        [JsonPropertyName("localTime")]
        public string LocalTime { get; set; }

        [JsonPropertyName("dateTime")]
        public string DateTime { get; set; }
    }

    public class TicketMasterEnd
    {
        [JsonPropertyName("localDate")]
        public string LocalDate { get; set; }

        [JsonPropertyName("localTime")]
        public string LocalTime { get; set; }

        [JsonPropertyName("dateTime")]
        public string DateTime { get; set; }
    }

    public class TicketMasterClassification
    {
        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("segment")]
        public TicketMasterSegment Segment { get; set; }

        [JsonPropertyName("genre")]
        public TicketMasterGenre Genre { get; set; }

        [JsonPropertyName("subGenre")]
        public TicketMasterSubGenre SubGenre { get; set; }

        [JsonPropertyName("family")]
        public bool Family { get; set; }
    }

    public class TicketMasterSegment
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TicketMasterGenre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TicketMasterSubGenre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TicketMasterVenueEmbedded
    {
        [JsonPropertyName("venues")]
        public List<TicketMasterVenue> Venues { get; set; }
    }

    public class TicketMasterVenue
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("city")]
        public TicketMasterCity City { get; set; }

        [JsonPropertyName("state")]
        public TicketMasterState State { get; set; }

        [JsonPropertyName("address")]
        public TicketMasterAddress Address { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("location")]
        public TicketMasterVenueLocation Location { get; set; }
    }

    public class TicketMasterCity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TicketMasterState
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TicketMasterAddress
    {
        [JsonPropertyName("line1")]
        public string Line1 { get; set; }
    }

    public class TicketMasterVenueLocation
    {
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
    }

    public class TicketMasterAccessibility
    {
        [JsonPropertyName("info")]
        public string Info { get; set; }
    }

    public class TicketMasterTicketLimit
    {
        [JsonPropertyName("info")]
        public string Info { get; set; }
    }
}
