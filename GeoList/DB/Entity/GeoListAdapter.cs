using System;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;

namespace GeoList
{
	public class GeoListAdapter : RecyclerView.Adapter
	{
		private IList<GeoLocation> _geoLocaitonList;
		public GeoListAdapter (IList<GeoLocation> geoLocaitonList)
		{
			_geoLocaitonList = geoLocaitonList;
		}

		#region implemented abstract members of Adapter

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			ViewHolder geoListHolder = holder as ViewHolder;

			geoListHolder.latitude.Text = string.Format ("{0:f6}", _geoLocaitonList [position].latitude);
			geoListHolder.longitude.Text = string.Format ("{0:f6}", _geoLocaitonList [position].longitude);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (Android.Views.ViewGroup parent, int viewType)
		{
			View view = LayoutInflater.From (parent.Context).Inflate (Resource.Layout.geo_list_item, parent, false);

			return new ViewHolder (view);
		}

		public override int ItemCount {
			get {
				return _geoLocaitonList.Count;
			}
		}

		#endregion

		public class ViewHolder : RecyclerView.ViewHolder 
		{
			public TextView latitude { get; set; }
			public TextView longitude { get; set; }

			public ViewHolder (View itemView) : base (itemView)
			{
				latitude = itemView.FindViewById<TextView>(Resource.Id.item_latitude);
				longitude = itemView.FindViewById<TextView>(Resource.Id.item_longitude);
			}
		}
	}
}

