using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdeaBag.Portable.Data;
using IdeaBag.Portable.Data.Models;
using IdeaBag.Portable.Utilty;


namespace IdeaBag.Client.iOS.DataAccess
{
	public class RestDataManager
	{
		#region Private Properties

		private string _serverurl;

		#endregion


		#region Public Properties



		#endregion

		
		#region Constructor

		public RestDataManager (string serverurl)
		{
			this._serverurl = serverurl;
		}

		#endregion


		#region Users

		/// <summary>
		/// Retrieve complete user information from the server
		/// </summary>
		public async Task<UserModel> GetUserInfo(string email){
			string userinfourl = string.Format (_serverurl + "/data/getuserinfo?email={0}", email);

			WebClient client = new WebClient ();

			Task<UserModel> userinfotask = Task.Run(() =>{
				string result = client.DownloadString (userinfourl);

				UserModel user = JsonTools.Deserialize<UserModel>(result);

				return user;
			});

			return await userinfotask;
		}

		/// <summary>
		/// Gets the user contacts.
		/// </summary>
		public async Task<List<UserModel>> GetUserContacts(string email, DateTime utcdate){
			string usercontactsurl = string.Format (_serverurl + "/data/GetUserContacts?email={0}&utcdate={1}", email, utcdate.ToString ());

			WebClient client = new WebClient ();

			Task<List<UserModel> > usercontactstask = Task.Run (() => {
				string result = client.DownloadString(usercontactsurl);

				List<UserModel> contacts = JsonTools.Deserialize<List<UserModel>>(result);

				return contacts;
			});

			return await usercontactstask;
		}

		#endregion


	}
}

