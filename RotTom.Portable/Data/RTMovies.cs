using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace RotTom.Portable.Data
{
	public enum CRITIC_RATING { ROTTEN, FRESH, CERTIFIED };
    public enum AUDIENCE_RATING { UPRIGHT, SPILLED } ;

    [DataContract]
    public class RTMovies
    {
        [DataMember]
        public List<Movie> movies { get; set; }
        [DataMember]
        public Links2 links { get; set; }
        [DataMember]
        public string link_template { get; set; }
    }

    public class ReleaseDates
    {
        public string theater { get; set; }
        public string dvd { get; set; }
    }

    public class Ratings
    {
        public string critics_rating
        {
            set
            {
                switch (value.ToUpper())
                {
                    case "ROTTEN":
                        this.critics_enumerating = CRITIC_RATING.ROTTEN;
                        break;
                    case "FRESH":
                        this.critics_enumerating = CRITIC_RATING.FRESH;
                        break;
                    case "CERTIFIED":
                    default:
                        this.critics_enumerating = CRITIC_RATING.CERTIFIED;
                        break;
                }
            }
        }
        public CRITIC_RATING critics_enumerating { get; set; }
        public int critics_score { get; set; }
        public string audience_rating
        {
            set
            {
                switch (value.ToUpper())
                {
                    case "SPILLED":
                        this.audience_enumerating = AUDIENCE_RATING.SPILLED;
                        break;
                    case "UPRIGHT":
                    default:
                        this.audience_enumerating = AUDIENCE_RATING.UPRIGHT;
                        break;
                }
            }
        }
        public int audience_score { get; set; }
        public AUDIENCE_RATING audience_enumerating { get; set; }
    }

    public class Posters
    {
        public string thumbnail { get; set; }
        public string profile { get; set; }
        public string detailed { get; set; }
        public string original { get; set; }
    }

    public class AbridgedCast
    {
        public string name { get; set; }
        public string id { get; set; }
        public List<string> characters { get; set; }
    }

    public class AlternateIds
    {
        public string imdb { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string alternate { get; set; }
        public string cast { get; set; }
        public string clips { get; set; }
        public string reviews { get; set; }
        public string similar { get; set; }
    }

    public class Movie
    {
        public string id { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string mpaa_rating { get; set; }
        public int runtime
        {
            set
            {
                var time = TimeSpan.FromMinutes(value);
				runtime_string = time.ToString("%h") + " hr. " + time.ToString("%m") + " min.";
            }
        }
        public string runtime_string { get; set; }
        public string critics_consensus { get; set; }
        public ReleaseDates release_dates { get; set; }
        public Ratings ratings { get; set; }
        public string synopsis { get; set; }
        public Posters posters { get; set; }
        public List<AbridgedCast> abridged_cast { get; set; }
        public AlternateIds alternate_ids { get; set; }
        public Links links { get; set; }
		public string ToJSON()
		{
			return JsonConvert.SerializeObject (this);
		}
		public static Movie FromJSON(string Json)
		{
			return JsonConvert.DeserializeObject<Movie> (Json);
		}
    }

    public class Links2
    {
        public string self { get; set; }
        public string alternate { get; set; }
    }

}