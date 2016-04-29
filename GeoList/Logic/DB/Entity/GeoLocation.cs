using System;
using SQLite;
using Java.Interop;
using Android.OS;

namespace GeoList
{
	public class GeoLocation: Java.Lang.Object, IParcelable
	{
		public GeoLocation ()
		{
		}
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }

		public GeoLocation(Parcel parcel) {
			ID = parcel.ReadInt ();
			latitude = parcel.ReadDouble ();
			longitude = parcel.ReadDouble ();
		}

		#region IParcelable implementation

		public int DescribeContents ()
		{
			return 0;
		}

		public void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
		{
			dest.WriteInt (ID);
			dest.WriteDouble (latitude);
			dest.WriteDouble (longitude);
		}

		#endregion

		private static readonly GenericParcelableCreator<GeoLocation> _creator
		= new GenericParcelableCreator<GeoLocation>((parcel) => new GeoLocation(parcel));

		[ExportField("CREATOR")]
		public static GenericParcelableCreator<GeoLocation> GetCreator()
		{
			return _creator;
		}

		public sealed class GenericParcelableCreator<T> : Java.Lang.Object, IParcelableCreator
			where T : Java.Lang.Object, new()
		{
			private readonly Func<Parcel, T> _createFunc;

			public GenericParcelableCreator(Func<Parcel, T> createFromParcelFunc)
			{
				_createFunc = createFromParcelFunc;
			}

			#region IParcelableCreator Implementation

			public Java.Lang.Object CreateFromParcel(Parcel source)
			{
				return _createFunc(source);
			}

			public Java.Lang.Object[] NewArray(int size)
			{
				return new T[size];
			}

			#endregion
		}
	}
}

