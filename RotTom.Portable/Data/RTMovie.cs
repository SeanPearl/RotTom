using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace RotTom.Portable.Data.RTMovie
{
	[DataContract]
	public class RTMovie
	{
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string title { get; set; }
		[DataMember]
		public int year { get; set; }
		[DataMember]
		public List<string> genres { get; set; }
		[DataMember]
		public string mpaa_rating { get; set; }
		[DataMember]
		public int runtime
		{
			set
			{
				var time = TimeSpan.FromMinutes(value);
				runtime_string = time.ToString("%h") + " hr. " + time.ToString("%m") + " min.";
			}
		}
		public string runtime_string { get; set; }
		[DataMember]
		public string critics_consensus { get; set; }
		[DataMember]
		public ReleaseDates release_dates { get; set; }
		[DataMember]
		public Ratings ratings { get; set; }
		[DataMember]
		public string synopsis { get; set; }
		[DataMember]
		public Posters posters { get; set; }
		[DataMember]
		public List<AbridgedCast> abridged_cast { get; set; }
		[DataMember]
		public List<AbridgedDirector> abridged_directors { get; set; }
		[DataMember]
		public string studio { get; set; }
		[DataMember]
		public AlternateIds alternate_ids { get; set; }
		[DataMember]
		public Links links { get; set; }
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

	public class AbridgedDirector
	{
		public string name { get; set; }
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

}

