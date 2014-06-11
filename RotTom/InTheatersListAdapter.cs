
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

	public class InTheatersListAdapter : BaseAdapter<Movie>
	{

		Activity context;
		List<Movie> movies = new List<Movie>();


		public InTheatersListAdapter(Activity context, List<Movie> movies) : base()
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
				view = context.LayoutInflater.Inflate(Resource.Layout.InTheatersItem, null);

			// set view properties to reflect data for the given row
			ImageView movieImage = view.FindViewById<ImageView> (Resource.Id.movieImage);

			#pragma warning disable 4014
			//Task imageTask = Task.Run(async () => { await 
			Images.SetImageFromUrlAsync (movieImage, movies[position].posters.profile); 
			//});
			#pragma warning restore 4014

			view.FindViewById<TextView>(Resource.Id.movieTitle).Text = (movies[position].title);
			view.FindViewById<TextView>(Resource.Id.criticScore).Text = (movies[position].ratings.critics_score) + "%";
			ImageView criticTomato = view.FindViewById<ImageView> (Resource.Id.criticTomato);
			switch(movies[position].ratings.critics_enumerating)
			{
			case (CRITIC_RATING.ROTTEN):
				criticTomato.SetImageResource (Resource.Drawable.rotten);
				break;
			case(CRITIC_RATING.FRESH):
			case(CRITIC_RATING.CERTIFIED):
				criticTomato.SetImageResource (Resource.Drawable.fresh);
				break;
			}
			if (movies[position].abridged_cast.Count > 0) {
				view.FindViewById<TextView> (Resource.Id.movieActors).Text = 
					(movies [position].abridged_cast [0].name);
			}
			if (movies[position].abridged_cast.Count > 1) {
				view.FindViewById<TextView> (Resource.Id.movieActors).Text += ", " +
					(movies [position].abridged_cast [1].name);
			}
			view.FindViewById<TextView>(Resource.Id.ratingAndRuntime).Text = 
				(movies[position].mpaa_rating) + ", " +
				(movies[position].runtime_string);


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


