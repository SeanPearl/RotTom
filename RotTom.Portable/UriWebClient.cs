using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace RotTom.Portable
{
	public class UriWebClient
	{
        public UriWebClient()
        { 
        
        }

		public async Task<string> getUriAsync (Uri uri)
		{
			WebRequest request = WebRequest.Create (uri);

			WebResponse response = request.GetResponseAsync().Result;

            StreamReader reader = new StreamReader( response.GetResponseStream());

			return reader.ReadToEnd();
       	}
			
	}
}

