using System;
using System.Collections.Generic;
using System.Net;
using IdeaBag.Portable.Data;
using IdeaBag.Portable.Data.Models;
using IdeaBag.Portable.ViewModels;
using IdeaBag.Portable.Utilty;

namespace IdeaBag.Client.iOS.ViewModels
{
	public class LoginViewModel 
	{
		#region EventHandler Properties

		public EventHandler LoginTouchUpInside;

		#endregion


		#region Events

		public event EventHandler<LoginResultModel> OnLoginCompleted;

		#endregion
		
		
		#region Private Properties

		private string _username;
		private string _password;
		private string _serverurl;

		#endregion


		#region Public Properties

		public string Username {
			get{ return _username; }
			set{ _username = value; }
		}


		public string Password {
			get{ return _password; }
			set{ _password = value; }
		}

		#endregion


		#region Constructor

		public LoginViewModel (string serverurl)
		{
			this._username = string.Empty;
			this._password = string.Empty;
			this._serverurl = serverurl;

			LoginTouchUpInside = new EventHandler (OnLoginTouchUpInside);
		}

		#endregion


		#region Event Handlers

		private void OnLoginTouchUpInside(object sender, EventArgs args){
			string loginurl = _serverurl + "/authentication/loginstandarduser";

			string url = string.Format ("{0}?uid={1}&pw={2}", loginurl, _username, _password);

			WebClient client = new WebClient ();
			string result = client.DownloadString (url);

			LoginResultModel model = JsonTools.Deserialize<LoginResultModel> (result);

			if (OnLoginCompleted != null)
				OnLoginCompleted (this, model);
		}

		#endregion
	}
}

