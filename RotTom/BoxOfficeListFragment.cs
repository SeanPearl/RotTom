
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using RotTom.Portable.Data;

namespace RotTom
{
	public class BoxOfficeListFragment : ListFragment
	{

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			// Get the movies
			RTMovies movies = (new RotTom.Portable.RottenTomatoesAPIClient (GetString(Resource.String.rottentomatoes_api_key))).GetRTMovies (RotTom.Portable.RT_MOVIES_CATEGORIES.BOX_OFFICE);

			this.ListAdapter = new BoxOfficeListAdapter(this.Activity, movies.movies);
		}

		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			Movie movie = ((BoxOfficeListAdapter)ListAdapter)[index];
			if (movie != null) {
				StartActivity (MovieActivity.CreateIntent (this.Activity, movie));
			}
		}
			
	}
}

