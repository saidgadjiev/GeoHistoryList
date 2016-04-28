using System;
using Android.Content;
using System.Collections.Generic;
using System.Collections;

namespace GeoList
{

	public class GeoListReceiver: BroadcastReceiver {
		#region implemented abstract members of BroadcastReceiver
		MainActivity _waitingActivity;
		public static readonly string PROCESS_RESPONSE = "intent.action.PROCESS_RESPONSE";

		public GeoListReceiver(MainActivity activity) {
			_waitingActivity = activity;
		}

		public override void OnReceive (Context context, Intent intent)
		{
			IList geoLocationList = intent.GetParcelableArrayListExtra ("geoLocationList");

			IList<GeoLocation> geoList = ConvertToListOf<GeoLocation> (geoLocationList);

			_waitingActivity.setGeoListAdapter (geoList);
		}
		#endregion

		public static IList<T> ConvertToListOf<T>(IList iList)
		{
			IList<T> result = new List<T>();
			foreach(T value in iList)
			{
				result.Add(value);
			}

			return result;
		}
	}
}

