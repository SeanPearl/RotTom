
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Content.PM;

using RotTom.Portable;
using RotTom.Portable.Data.RTMovie;
using RotTom.Portable.Data.Critics;

namespace RotTom
{

	[Activity (Label = "RotTom", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class MovieActivity : Activity
	{
		private enum Tab { INFO , CAST, CRITICS };
		private RTMovie movie;

		protected override void OnCreate (Bundle bundle)
		{
		
			FileCache.SaveLocation = CacheDir.AbsolutePath;
			base.OnCreate (bundle);

			var RTClient = (new RotTom.Portable.RottenTomatoesAPIClient (GetString (Resource.String.rottentomatoes_api_key)));
			// Look for intents
			string id;
			if (Intent.HasExtra ("Movie")) {
				id = Intent.GetStringExtra ("Movie");
			} else {
				id = "770672122";
			}
			this.movie = RTClient.GetRTMovie (id);
			RTCritics critics = RTClient.GetRTCritics (id);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			this.Title = movie.title;

			// Set the ActionBar for tabbed navigation
			this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			// Set up the tab strings for easy lookup
			Dictionary<Tab, int> tabStrings = new Dictionary<Tab, int>();
			tabStrings.Add (Tab.INFO, Resource.String.movieinfo_tab_title);
			tabStrings.Add (Tab.CAST, Resource.String.moviecast_tab_title);
			tabStrings.Add (Tab.CRITICS, Resource.String.moviecritics_tab_title);

			// Add those tabs
			foreach (Tab tabid in (Tab[]) Enum.GetValues(typeof(Tab)))
			{
				// Create the tab
				var tab = this.ActionBar.NewTab ();

				// Name the tab
				tab.SetText(tabStrings[tabid]);

				// Set the callback routine
				tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
					switch(tabid)
					{
					case (Tab.CAST):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, CastListFragment.NewInstance(movie));
						break;
					case (Tab.INFO):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, MovieInfoFragment.NewInstance(movie));
						break;
					case (Tab.CRITICS):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, CriticsListFragment.NewInstance(critics));
						break;
					}
				};

				//tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				//	e.FragmentTransaction.Remove(this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer));
				//};

				// Finally, add the tab
				this.ActionBar.AddTab (tab);
			}

			if (bundle != null)
				this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));

		}

		public static Intent CreateIntent (Context context, RotTom.Portable.Data.Movie movie)
		{
			var intent = new Intent (context, typeof (MovieActivity));
			intent.PutExtra ("Movie", movie.id);
			return intent;
		}

		//protected override void OnSaveInstanceState (Bundle outState)
		//{
		//	outState.PutInt ("counter", c);
		//	base.OnSaveInstanceState (outState);
		//}
	}
}

