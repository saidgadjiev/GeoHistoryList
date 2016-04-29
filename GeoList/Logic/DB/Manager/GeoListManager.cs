using System;
using Android.App;
using Android.Util;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.Content;
using Android.OS;
using System.Collections;

namespace GeoList
{
	[Service]
	public class GeoListManager: IntentService
	{
		public GeoListManager ()
		{
		}

		public static IList<IParcelable> ConvertToParcelableIListOf(IList<GeoLocation> iList)
		{
			IList<IParcelable> result = new List<IParcelable>();
			foreach(GeoLocation value in iList)
			{
				result.Add(value);
			}

			return result;
		}

		#region implemented abstract members of IntentService

		protected override void OnHandleIntent (Android.Content.Intent intent)
		{
			GeoLocationDatabase database = new GeoLocationDatabase ();

			IList<IParcelable> geoLocations = ConvertToParcelableIListOf(database.getGeoLocations ());
			Intent broadcastIntent = new Intent();

			broadcastIntent.SetAction (GeoListReceiver.PROCESS_RESPONSE);
			broadcastIntent.AddCategory (Intent.CategoryDefault);
			broadcastIntent.PutParcelableArrayListExtra ("geoLocationList", geoLocations);
			SendBroadcast (broadcastIntent);
			LocalBroadcastManager.GetInstance (this).SendBroadcast (broadcastIntent);
			Log.Debug ("IntentService", "Yes");
		}

		#endregion
	}
}

