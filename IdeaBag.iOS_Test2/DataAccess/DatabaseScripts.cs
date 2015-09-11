using System;

namespace IdeaBag.Client.iOS.DataAccess
{
	public class DatabaseScripts
	{
		#region Constructor

		public DatabaseScripts ()
		{
		}

		#endregion

		#region Table Scripts

		public static string CreateMessageTableScript()
		{
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS Messages(GlobalID TEXT PRIMARY KEY NOT NULL UNIQUE, ";
			sql += "TargetUser_FK TEXT NOT NULL, ";
			sql += "SourceUser_FK TEXT NOT NULL, ";
			sql += "SendDate DATETIME NOT NULL, ";
			sql += "Message TEXT NOT NULL, ";
			sql += "Title TEXT NOT NULL, ";
			sql += "Latitude DOUBLE, ";
			sql += "Longitude DOUBLE, ";
			sql += "IsEvent BOOL, ";
			sql += "EventStartDate DATETIME)";

			return sql;
		}


		public static string CreateApplicationSettingsTableScript()
		{
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS ApplicationSettings(ApplicationSettingsID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ";
			sql += "DeviceID VARCHAR NOT NULL UNIQUE)";

			return sql;
		}

		#endregion
	}
}

