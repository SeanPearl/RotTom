using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Threading.Tasks;

namespace RotTom.Portable.Data.Critics
{

	[DataContract]
	public class RTCritics
	{
		[DataMember]
		public int total { get; set; }
		[DataMember]
		public List<Review> reviews { get; set; }
		[DataMember]
		public Links2 links { get; set; }
		[DataMember]
		public string link_template { get; set; }

	}
	public class Links
	{
		public string review { get; set; }
	}

	public class Review
	{
		public string critic { get; set; }
		public string date { get; set; }
		public string freshness { 
			set
			{
				switch (value.ToUpper())
				{
				case "ROTTEN":
					this.fresh_enum = CRITIC_RATING.ROTTEN;
					break;
				case "FRESH":
					this.fresh_enum = CRITIC_RATING.FRESH;
					break;
				}
			}
		}

		public CRITIC_RATING fresh_enum { get; set; }
		public string publication { get; set; }
		public string quote { get; set; }
		public Links links { get; set; }
		public string original_score { get; set; }
	}

	public class Links2
	{
		public string self { get; set; }
		public string next { get; set; }
		public string alternate { get; set; }
		public string rel { get; set; }
	}
		
}


