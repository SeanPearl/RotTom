
using System;
using System.Threading.Tasks;
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
using RotTom.Portable.Data.RTMovie;


namespace RotTom
{
	public class MovieInfoFragment : Fragment
	{
		private RTMovie movie{ get; set; }

		public static MovieInfoFragment NewInstance(RTMovie movie)
		{
			var movieInfoFragment = new MovieInfoFragment ();
			movieInfoFragment.movie = movie;
			return movieInfoFragment;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.MovieInfo, container, false);

			var score = (movie.ratings.critics_score);
			if (score == -1) { //Movie not scored
				view = inflater.Inflate (Resource.Layout.MovieInfoNoScore, null);
			} else {
				view = inflater.Inflate (Resource.Layout.MovieInfo, null);
				view.FindViewById<TextView> (Resource.Id.criticScore).Text = (movie.ratings.critics_score) + "%";
				view.FindViewById<TextView> (Resource.Id.usersScore).Text = (movie.ratings.audience_score) + "%";
				ImageView criticTomato = view.FindViewById<ImageView> (Resource.Id.criticTomato);
				switch(movie.ratings.critics_enumerating)
				{
				case (CRITIC_RATING.ROTTEN):
					criticTomato.SetImageResource (Resource.Drawable.rotten);
					break;
				case(CRITIC_RATING.FRESH):
				case(CRITIC_RATING.CERTIFIED):
					criticTomato.SetImageResource (Resource.Drawable.fresh);
					break;
				}
				ImageView userPopcorn = view.FindViewById<ImageView> (Resource.Id.userPopcorn);
				switch (movie.ratings.audience_enumerating) 
				{
				case (AUDIENCE_RATING.SPILLED):
					userPopcorn.SetImageResource (Resource.Drawable.spilt);
					break;
				case (AUDIENCE_RATING.UPRIGHT):
					userPopcorn.SetImageResource (Resource.Drawable.popcorn);
					break;
				}
			}
			ImageView movieImage = view.FindViewById<ImageView> (Resource.Id.movieImage);

			#pragma warning disable 4014
			//Task imageTask = Task.Run(async () => { await 
			Images.SetImageFromUrlAsync (movieImage, movie.posters.profile); 
			//});	
			#pragma warning restore 4014

			view.FindViewById<TextView> (Resource.Id.movieTitle).Text = (movie.title);
		
			//view.FindViewById<ImageView> (Resource.Id.movieImage).SetImageBitmap();

			if (movie.abridged_cast.Count > 0) 
			{
				view.FindViewById<TextView> (Resource.Id.movieActors).Text = 
					(movie.abridged_cast [0].name);
			}
			if (movie.abridged_cast.Count > 1)
			{
				view.FindViewById<TextView> (Resource.Id.movieActors).Text += ", "
				+ (movie.abridged_cast [1].name);
			}

			view.FindViewById<TextView>(Resource.Id.ratingAndRuntime).Text = 
				(movie.mpaa_rating) + ", " +
				(movie.runtime_string);


			view.FindViewById<TextView> (Resource.Id.synopsis).Text += 
				(movie.synopsis);

			if (movie.abridged_directors.Count > 0) 
			{
				view.FindViewById<TextView> (Resource.Id.directorName).Text += 
					(movie.abridged_directors [0].name);
			}

			if (movie.genres != null) {
				view.FindViewById<TextView> (Resource.Id.movieGenres).Text += (movie.genres [0]);
					for (int i = 1; i < movie.genres.Count; i++)
					view.FindViewById<TextView> (Resource.Id.movieGenres).Text += ", " + (movie.genres [i]);
			} 
			else 
			{
				view.FindViewById<TextView> (Resource.Id.movieGenres).Text = "";
			}
			view.FindViewById<TextView> (Resource.Id.releaseDate).Text += (movie.release_dates.theater);

			return view;
		
		}

	}
}

