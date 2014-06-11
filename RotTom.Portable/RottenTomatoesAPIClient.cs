using System;
using System.Net;
using System.Collections.Generic;

using RotTom.Portable.Data;
using RotTom.Portable.Data.Critics;
using RotTom.Portable.Data.RTMovie;
using Newtonsoft.Json;

namespace RotTom.Portable
{
    public enum RT_MOVIES_CATEGORIES { BOX_OFFICE, OPENING, IN_THEATERS, UPCOMING,  };
	public class RottenTomatoesAPIClient
	{
		private readonly String APIKey;
        private UriWebClient webClient;
	
		public RottenTomatoesAPIClient(String APIKey) : this(APIKey, new UriWebClient()) 
		{
		}

        public RottenTomatoesAPIClient(String APIKey, UriWebClient webClient) 
		{
			this.APIKey = APIKey;
            this.webClient = webClient;
		}

		public RTMovies GetRTMovies(RT_MOVIES_CATEGORIES category)
		{
            // Set up the URI base
            string uri = "http://api.rottentomatoes.com/api/public/v1.0/lists/movies/";

            // Choose the node based on the category
            switch(category)
            {
                case RT_MOVIES_CATEGORIES.BOX_OFFICE:
                    uri += "box_office.json";
                    break;
                case RT_MOVIES_CATEGORIES.IN_THEATERS:
                    uri += "in_theaters.json";
                    break;
                case RT_MOVIES_CATEGORIES.OPENING:
                    uri += "opening.json";
                    break;
                case RT_MOVIES_CATEGORIES.UPCOMING:
                    uri += "upcoming.json";
                    break;
            }
			uri += "?";

            // Add the API Key
            uri += "apikey=" + APIKey;

            // Get the API response JSON
            string APIResponse = webClient.getUriAsync(new Uri(uri)).Result;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            RTMovies movies = JsonConvert.DeserializeObject<RTMovies>(APIResponse, settings);
            return movies;
            
        }

		public RTMovie GetRTMovie (string movieID)
		{
			// Set up the URI base
			string uri = "http://api.rottentomatoes.com/api/public/v1.0/movies/"
				+ movieID + ".json?";

			// Add the API Key
			uri += "apikey=" + APIKey;

			// Get the API response JSON
			string APIResponse = webClient.getUriAsync(new Uri(uri)).Result;

			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.NullValueHandling = NullValueHandling.Ignore;
			RTMovie movie = JsonConvert.DeserializeObject<RTMovie>(APIResponse, settings);
			return movie;
		}

		public RTCritics GetRTCritics(string movieID)
		{
			// Set up the URI base
			string uri = "http://api.rottentomatoes.com/api/public/v1.0/movies/"
				+ movieID + "/reviews.json?review_type=top_critic&";

			// Add the API Key
			uri += "apikey=" + APIKey;

			// Get the API response JSON
			string APIResponse = webClient.getUriAsync(new Uri(uri)).Result;

			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.NullValueHandling = NullValueHandling.Ignore;
			RTCritics critics = JsonConvert.DeserializeObject<RTCritics>(APIResponse, settings);
			return critics;
		}

	}
}

