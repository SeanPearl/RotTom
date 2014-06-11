using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Content.PM;

using RotTom.Portable;
using RotTom.Portable.Data;

namespace RotTom
{
	// Handles the display of lists of movies
	[Activity (Label = "RotTom", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MoviesActivity : ListActivity
	{
		private enum Tab { BOX_OFFICE, OPENING, IN_THEATERS, UPCOMING };

		protected override void OnCreate (Bundle bundle)
		{

			FileCache.SaveLocation = CacheDir.AbsolutePath;
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
	
			// Set the ActionBar for tabbed navigation
			this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			// Set up the tab strings for easy lookup
			Dictionary<Tab, int> tabStrings = new Dictionary<Tab, int>();
			tabStrings.Add (Tab.BOX_OFFICE, Resource.String.boxoffice_tab_title);
			tabStrings.Add (Tab.UPCOMING, Resource.String.upcoming_tab_title);
			tabStrings.Add (Tab.IN_THEATERS, Resource.String.intheaters_tab_title);
			tabStrings.Add (Tab.OPENING, Resource.String.opening_tab_title);

			//List<ActionBar.Tab> tabs = new List<ActionBar.Tab> ();

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
					case (Tab.BOX_OFFICE):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, new BoxOfficeListFragment());
						this.Title = GetString(Resource.String.boxoffice_activity_title);
						break;
					case (Tab.UPCOMING):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, new UpcomingListFragment());
						this.Title = GetString(Resource.String.upcoming_activity_title);
						break;
					case (Tab.IN_THEATERS):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, new InTheatersListFragment());
						this.Title = GetString(Resource.String.intheatres_activity_title);
						break;
					case (Tab.OPENING):
						e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, new OpeningListFragment());
						this.Title = GetString(Resource.String.opening_activity_title);
						break;
					}
				};

				//tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				//	e.FragmentTransaction.Remove(this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer));
				//};

				// Finally, add the tab
				this.ActionBar.AddTab (tab);
				//tabs.Add (tab);
			}


			if (bundle != null)
				this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));


		}

		protected override void OnStart ()
		{
			base.OnStart ();

		}


	}
}

