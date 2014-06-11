
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using RotTom.Portable.Data;

namespace RotTom
{

	public class UpcomingListAdapter : BaseAdapter<Movie>
	{

		Activity context;
		List<Movie> movies = new List<Movie>();


		public UpcomingListAdapter(Activity context, List<Movie> movies) : base()
		{
			this.context = context;
			this.movies = movies;

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
				view = context.LayoutInflater.Inflate(Resource.Layout.UpcomingItem, null);

			// set view properties to reflect data for the given row
			ImageView movieImage = view.FindViewById<ImageView> (Resource.Id.movieImage);

			#pragma warning disable 4014
			//Task imageTask = Task.Run(async () => { await 
			Images.SetImageFromUrlAsync (movieImage, movies[position].posters.profile); 
			//});
			#pragma warning restore 4014

			view.FindViewById<TextView>(Resource.Id.movieTitle).Text = (movies[position].title);

			// return the view, populated with data, for display
			return view;

		}

		public override int Count {
			get {
				return movies.Count;
			}
		}

		#endregion

		#region implemented abstract members of BaseAdapter

		public override Movie this [int index] {
			get {
				return movies[index];
			}
		}

		#endregion
	}
}


