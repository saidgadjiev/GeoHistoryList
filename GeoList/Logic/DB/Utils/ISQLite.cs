using System;
using SQLite;

namespace GeoList
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

