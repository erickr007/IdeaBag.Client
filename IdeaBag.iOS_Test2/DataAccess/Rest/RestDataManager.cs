using System;
using System.Net;
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

		#endregion


	}
}

