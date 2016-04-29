using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using System.Collections.Generic;
using System.Linq;
using Android;
using System;
using System.Threading.Tasks;
using System.Text;
using Android.Content;
using Android.Support.V7.Widget;
using System.Collections;
using Android.Support.V4.Content;

namespace GeoList
{
	[Activity (Label = "GeoList", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, ILocationListener
	{
		Location _currentLocation;
		LocationManager _locationManager;
		RecyclerView _recyclerView;
		RecyclerView.LayoutManager _layoutManager;
		GeoListAdapter _adapter;
		GeoListReceiver _receiver;

		string _locationProvider;
		TextView _latitudeText;
		TextView _longitudeText;
		bool _isFirstStart = true;
		GeoLocationDatabase _database;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);
			Xamarin.Forms.Forms.Init (this, bundle);

			_database = new GeoLocationDatabase ();
			_receiver = new GeoListReceiver (this);
			_recyclerView = FindViewById<RecyclerView> (Resource.Id.recycler_view);
			_recyclerView.AddItemDecoration (new GeoListItemDecoration (this, LinearLayoutManager.Vertical));
			_layoutManager = new LinearLayoutManager (this);
			_recyclerView.SetLayoutManager (_layoutManager);
			_latitudeText = FindViewById<TextView> (Resource.Id.latitude);
			_longitudeText = FindViewById<TextView> (Resource.Id.longitude);

			StartService(new Intent(this, typeof(GeoListManager)));
			IntentFilter filter = new IntentFilter (GeoListReceiver.PROCESS_RESPONSE);
			filter.AddCategory (Intent.CategoryDefault);
			LocalBroadcastManager.GetInstance (this).RegisterReceiver (_receiver, filter);
			InitializeLocationManager();
		}
		protected override void OnResume()
		{
			base.OnResume();
			_locationManager.RequestLocationUpdates (_locationProvider, 0, 0, this);
		}
		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			if (_receiver != null) {
				LocalBroadcastManager.GetInstance (this).UnregisterReceiver (_receiver);
			}
		}
		void InitializeLocationManager()
		{
			_locationManager = (LocationManager) GetSystemService(LocationService);
			Criteria criteriaForGPSService = new Criteria  
			{  
				Accuracy = Accuracy.Coarse,  
				PowerRequirement = Power.Medium  
			}; 
			_locationProvider = _locationManager.GetBestProvider(criteriaForGPSService, true);
		}

		public void setGeoListAdapter(IList<GeoLocation> geoLocationList) {
			if (_adapter == null) {
				_adapter = new GeoListAdapter (geoLocationList);
				_recyclerView.SetAdapter (_adapter);
			}
		}

		public void OnLocationChanged(Location location)
		{
			if (_isFirstStart) {
				_currentLocation = location;
				_latitudeText.Text = string.Format ("{0:f3}", _currentLocation.Latitude);
				_longitudeText.Text = string.Format ("{0:f3}", _currentLocation.Longitude);

				GeoLocation geoLocation = new GeoLocation();

				geoLocation.longitude = _currentLocation.Longitude;
				geoLocation.latitude = _currentLocation.Latitude;
				_database.addGeoLocation (geoLocation);
				_isFirstStart = false;
			}
		}
		public void OnProviderDisabled (string provider)
		{
		}

		public void OnProviderEnabled (string provider)
		{
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
		}
	}
}


