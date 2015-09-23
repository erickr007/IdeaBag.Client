using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using SQLite;

namespace IdeaBag.Client.iOS.DataAccess
{
	public static class DatabaseHelper
	{
		#region Execute Query

		public static List<T> ExecuteQuery<T>(string dbpath, string cmd){
			using (var conn = new SQLiteConnection (dbpath)) {
				SQLiteCommand command = new SQLiteCommand (conn);
				command.CommandText = cmd;

				List<T> results = command.ExecuteQuery<T> ();

				return results;
			}
		}

		#endregion


		#region Execute Non Query

		public static int ExecuteNonQuery(string dbpath, string cmd){
			using (var conn = new SQLiteConnection (dbpath)) {
				SQLiteCommand command = new SQLiteCommand (conn);
				command.CommandText = cmd;

				int results = command.ExecuteNonQuery();

				return results;
			}
		}

		#endregion
	}
}

