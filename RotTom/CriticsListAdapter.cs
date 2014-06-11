
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
using RotTom.Portable.Data.Critics;

namespace RotTom
{
	public class CriticsListAdapter : BaseAdapter<Review>
	{

		Activity context;
		List<Review> reviews = new List<Review>();

		public CriticsListAdapter(Activity context, List<Review> reviews) : base()
		{
			this.context = context;
			this.reviews = reviews;
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
				view = context.LayoutInflater.Inflate(Resource.Layout.CriticItem, null);

			view.FindViewById<TextView> (Resource.Id.criticNamePublication).Text = 
				reviews [position].critic + ", " + 
				reviews [position].publication ; 
			view.FindViewById<TextView> (Resource.Id.reviewText).Text = reviews [position].quote;

			ImageView criticTomato = view.FindViewById<ImageView> (Resource.Id.criticTomato);
			switch(reviews[position].fresh_enum)
			{
			case (CRITIC_RATING.ROTTEN):
				criticTomato.SetImageResource (Resource.Drawable.rotten);
				break;
			case(CRITIC_RATING.FRESH):
			case(CRITIC_RATING.CERTIFIED):
				criticTomato.SetImageResource (Resource.Drawable.fresh);
				break;
			}

			return view;
		}
		public override int Count {
			get {
				return reviews.Count;
			}
		}
		#endregion
		#region implemented abstract members of BaseAdapter
		public override Review this [int index] {
			get {
				return reviews[index];
			}
		}
		#endregion
	}

}

