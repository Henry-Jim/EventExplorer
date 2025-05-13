using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EventExplorer.DataProviders.TicketMaster
{
    internal class TicketMasterService
    {
        private readonly string _apiKey;
        private readonly string _rootURL = "https://app.ticketmaster.com/discovery/v2/";
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        // Constructor to load the API key from file
        public TicketMasterService()
        {
            _apiKey = LoadApiKey();
            _httpClient = new HttpClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
            };
        }

        // Constructor that allows passing API key directly
        public TicketMasterService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
            };
        }

        // Load API from file
        private string LoadApiKey()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "api_keys.txt");

                // Check if file exits
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("API keys file not found. Please create 'api_keys.txt' with your keys.");
                    return string.Empty;
                }

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Find the line that starts with "ticketmaster"
                foreach (string line in lines)
                {
                    if (line.StartsWith("ticketmaster="))
                    {
                        return line.Substring("ticketmaster=".Length).Trim();
                    }
                }

                Console.WriteLine("Ticketmaster API key not found in the file.");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading API key: {ex.Message}");
                return string.Empty;
            }
        }

        // Get events with various optional parameters
        public async Task<List<Event>> GetEventsAsync(string keyword = null, string classificationName = null, string city = null, string countryCode = null, string startDateTime = null, string endDateTime = null, int size = 20)
        {
            try
            {
                // Check if API key is available
                if (string.IsNullOrEmpty(_apiKey))
                {
                    Console.WriteLine("API key is not available. Please check your api_keys.txt file.");
                    return new List<Event>();
                }

                // Build the URL with query parameters
                var urlBuilder = new StringBuilder($"{_rootURL}events.json?apikey={_apiKey}&size={size}");

                // Add optional parameters to the URL
                if (!string.IsNullOrEmpty(keyword))
                {
                    urlBuilder.Append($"&keyword={Uri.EscapeDataString(keyword)}");
                }

                if (!string.IsNullOrEmpty(classificationName))
                {
                    urlBuilder.Append($"&classificationName={Uri.EscapeDataString(classificationName)}");
                }

                if (!string.IsNullOrEmpty(city))
                {
                    urlBuilder.Append($"&city={Uri.EscapeDataString(city)}");
                }

                if (!string.IsNullOrEmpty(countryCode))
                {
                    urlBuilder.Append($"&countryCode={Uri.EscapeDataString(countryCode)}");
                }

                if (!string.IsNullOrEmpty(startDateTime))
                {
                    urlBuilder.Append($"&startDateTime={Uri.EscapeDataString(startDateTime)}");
                }

                if (!string.IsNullOrEmpty(endDateTime))
                {
                    urlBuilder.Append($"&endDateTime={Uri.EscapeDataString(endDateTime)}");
                }

                // Make the API request
                var response = await _httpClient.GetAsync(urlBuilder.ToString());
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tmr = JsonSerializer.Deserialize<TicketMasterResponse>(jsonResponse, _jsonOptions);

                // Convert using the exisiting converter
                return TicketMasterConverter.ConvertEvents(tmr);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching events: {ex.Message}");
                return new List<Event>();
            }
        }

        // Get a specific event by its original ID
        public async Task<Event> GetEventByOriginalIDAsync(string originalID)
        {
            try
            {
                // Check if API key is available
                if (string.IsNullOrEmpty(_apiKey))
                {
                    Console.WriteLine("API key is not available. Please check your api_keys.txt file.");
                    return null;
                }

                if (string.IsNullOrEmpty(originalID))
                {
                    Console.WriteLine("Original ID is not provided.");
                    return null;
                }

                // Build the URL for a specific event using the root URL and the original ID
                var url = $"{_rootURL}events/{originalID}.json?apikey={_apiKey}";

                // Make the API request
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tme = JsonSerializer.Deserialize<TicketMasterEvent>(jsonResponse, _jsonOptions);

                // Create a temportary TicketMasterResponse object to use the existing converter
                var tmr = new TicketMasterResponse
                {
                    Embedded = new TicketMasterEmbedded
                    {
                        Events = new List<TicketMasterEvent> { tme }
                    }
                };

                // Convert and return the first (and only) event
                var events = TicketMasterConverter.ConvertEvents(tmr);
                return events.Count > 0 ? events[0] : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching event by original ID: {ex.Message}");
                return null;
            }
        }
    }
}
