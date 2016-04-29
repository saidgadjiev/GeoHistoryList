using System;
using Xamarin.Forms;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Android.OS;

namespace GeoList
{
	public class GeoLocationDatabase
	{
		private SQLiteConnection _database;

		public GeoLocationDatabase ()
		{
			_database = DependencyService.Get<ISQLite> ().GetConnection ();
			_database.CreateTable<GeoLocation> ();
		}

		public IList<GeoLocation> getGeoLocations() {
			return (from t in _database.Table<GeoLocation>() select t).ToList();
		}

		public void addGeoLocation(GeoLocation location) {
			_database.Insert (location);
		}
	}
}

