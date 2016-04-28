using System;
using Android.App;
using Android.Util;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.Content;
using Android.OS;

namespace GeoList
{
	[Service]
	public class GeoListManager: IntentService
	{
		IList<IParcelable> fakeData = new List<IParcelable>();

		public GeoListManager ()
		{
		}

		public void prepareTestData() {
			for (int i = 0; i < 10; i++) {
				GeoLocation location = new GeoLocation ();

				location.latitude = 65.1233;
				location.longitude = 23.423423;
				fakeData.Add (location);
			}
		}

		#region implemented abstract members of IntentService

		protected override void OnHandleIntent (Android.Content.Intent intent)
		{
			//GeoLocationDatabase database = new GeoLocationDatabase ();

			//IEnumerable<GeoLocation> geoLocations = database.getGeoLocations ();
			prepareTestData();
			Intent broadcastIntent = new Intent();

			broadcastIntent.SetAction (GeoListReceiver.PROCESS_RESPONSE);
			broadcastIntent.AddCategory (Intent.CategoryDefault);
			broadcastIntent.PutParcelableArrayListExtra ("geoLocationList", fakeData);
			SendBroadcast (broadcastIntent);
			LocalBroadcastManager.GetInstance (this).SendBroadcast (broadcastIntent);
			Log.Debug ("IntentService", "Yes");
		}

		#endregion
	}
}

