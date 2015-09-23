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

		/// <summary>
		/// ApplicationSettings
		/// </summary>
		/// <returns>The application settings table script.</returns>
		public static string CreateApplicationSettingsTableScript()
		{
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS ApplicationSettings(ApplicationSettingsID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ";
			sql += "DeviceID VARCHAR NOT NULL UNIQUE)";

			return sql;
		}

		/// <summary>
		/// User Table
		/// </summary>
		/// <returns>The users table script.</returns>
		public static string CreateUsersTableScript(){
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS Users(UserID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ";
			sql += "GlobalID TEXT NOT NULL UNIQUE, ";
			sql += "Email TEXT NOT NULL UNIQUE, ";
			sql += "Password TEXT NOT NULL UNIQUE, ";
			sql += "LoginType_FK INTEGER NOT NULL, ";
			sql += "FirstName TEXT, ";
			sql += "LastName TEXT, ";
			sql += "HomeAddress TEXT, ";
			sql += "DateCreated DATETIME NOT NULL, ";
			sql += "LastModified DATETIME NOT NULL)";

			return sql;
		}

		/// <summary>
		/// LoginTypes
		/// </summary>
		/// <returns>The login types table script.</returns>
		public static string CreateLoginTypesTableScript(){
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS LoginTypes(LoginTypeID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ";
			sql += "Name TEXT NOT NULL)";

			return sql;
		}

		/// <summary>
		/// Contacts
		/// </summary>
		/// <returns>The contacts table script.</returns>
		public static string CreateContactsTableScript(){
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS Contacts(ContactID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ";
			sql += "GlobalID TEXT NOT NULL UNIQUE, ";
			sql += "Email TEXT NOT NULL UNIQUE, ";
			sql += "FirstName TEXT, ";
			sql += "LastName TEXT, ";
			sql += "DateCreated DATETIME)";

			return sql;
		}

		/// <summary>
		/// Links between Users and Contacts
		/// </summary>
		/// <returns>The contact links.</returns>
		public static string UserContactLinks(){
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS UserContactLinks(UserContactLinkeID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ";
			sql += "User_FK TEXT NOT NULL UNIQUE, ";
			sql += "Contact_FK TEXT NOT NULL UNIQUE, ";

			return sql;
		}

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

		public static string CreateLogMessagesTableScript(){
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS LogMessages(LogMessagesID INTEGER PRIMARY KEY NOT NULL UNIQUE, ";
			sql += "LogType_FK INTEGER NOT NULL, ";
			sql += "Message TEXT NOT NULL, ";
			sql += "DateCreated DATETIME NOT NULL)";

			return sql;
		}

		public static string CreateLogMessageTypesScript(){
			string sql = "";

			sql += "CREATE TABLE IF NOT EXISTS LogTypes(LogTypeID INTEGER PRIMARY KEY NOT NULL UNIQUE, ";
			sql += "Name TEXT NOT NULL)";

			return sql;

		}


		#endregion
	}
}

