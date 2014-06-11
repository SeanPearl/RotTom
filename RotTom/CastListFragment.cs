
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

using RotTom.Portable.Data.RTMovie;

namespace RotTom
{
	public class CastListFragment : ListFragment
	{
		RTMovie movie { get; set; }
		public static CastListFragment NewInstance(RTMovie movie)
		{
			var castFragment = new CastListFragment ();
			castFragment.movie = movie;
			return castFragment;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			this.ListAdapter = new CastListAdapter(this.Activity, movie.abridged_cast);
		}

		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			//Movie movie = ((BoxOfficeListAdapter)ListAdapter)[index];
			//if (movie != null) {
			//	StartActivity (MovieActivity.CreateIntent (this.Activity, movie));
			//}
		}
			
	}
}

