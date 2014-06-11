
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
	public class CastListAdapter : BaseAdapter<AbridgedCast>
	{

		Activity context;
		List<AbridgedCast> cast = new List<AbridgedCast>();

		public CastListAdapter(Activity context, List<AbridgedCast> cast) : base()
		{
			this.context = context;
			this.cast = cast;
		}
			

		#region implemented abstract members of BaseAdapter
		public override long GetItemId (int position)
		{
			return position;
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is supplied

			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.CastItem, null);

			view.FindViewById<TextView> (Resource.Id.actorName).Text = cast [position].name; 

			if (cast [position].characters != null) {
				view.FindViewById<TextView> (Resource.Id.actorRole).Text = cast [position].characters [0];
				for (int i = 1; i < cast [position].characters.Count; i++)
					view.FindViewById<TextView> (Resource.Id.actorRole).Text += ", " + cast [position].characters [i];
			} 
			else 
			{
				view.FindViewById<TextView> (Resource.Id.actorRole).Text = "";
			}

			return view;
		}

		public override int Count {
			get {
				return cast.Count;
			}
		}
		#endregion
		#region implemented abstract members of BaseAdapter
		public override AbridgedCast this [int index] {
			get {
				return cast[index];
			}
		}
		#endregion
	}

}

