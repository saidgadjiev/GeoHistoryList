using System;
using SQLite;
using System.IO;
using GeoList;
using Xamarin.Forms;

[assembly: Dependency (typeof (SQLite_Android))]
namespace GeoList
{
	public class SQLite_Android: ISQLite
	{
		public SQLite_Android ()
		{
		}

		public SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "GeoListSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}

