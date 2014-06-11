
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

using RotTom.Portable.Data.Critics;

namespace RotTom
{
	public class CriticsListFragment : ListFragment
	{

		RTCritics critics { get; set; }

		public static CriticsListFragment NewInstance(RTCritics critics)
		{
			var criticFragment = new CriticsListFragment ();
			criticFragment.critics = critics;
			return criticFragment;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			this.ListAdapter = new CriticsListAdapter (this.Activity, critics.reviews);

		}
		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			var uri = Android.Net.Uri.Parse (((CriticsListAdapter)ListAdapter)[index].links.review);
			var intent = new Intent (Intent.ActionView, uri); 
			StartActivity (intent);     
		}
			
	}
}

