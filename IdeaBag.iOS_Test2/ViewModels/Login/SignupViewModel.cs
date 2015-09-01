using System;
using System.Collections.Generic;
using System.Net;
using IdeaBag.Portable.Data;
using IdeaBag.Portable.Data.Models;
using IdeaBag.Portable.ViewModels;
using IdeaBag.Portable.Utilty;

namespace IdeaBag.Client.iOS.ViewModels
{
	public class SignupViewModel 
	{
		#region EventHandler Properties

		public EventHandler SignupTouchUpInside;

		#endregion


		#region Events

		public event EventHandler<SignupResultModel> OnSignupCompleted;

		#endregion
		
		
		#region Private Properties

		private UserModel _user;
		private string _signupurl;

		#endregion


		#region Public Properties

		public UserModel User {
			get{ return _user; }
			set{ _user = value; }
		}

		#endregion


		#region Constructor

		public SignupViewModel (string signupurl)
		{
			this._user = new UserModel (){ GlobalID = Guid.NewGuid ().ToString (), 
				FirstName = "", 
				LastName = "",
				ConnectionIDs = new List<string>()
			};
			this._signupurl = signupurl;

			SignupTouchUpInside = new EventHandler (OnSignupTouchUpInside);
		}

		#endregion


		#region Event Handlers

		private void OnSignupTouchUpInside(object sender, EventArgs args){
			

			string data = JsonTools.Serialize<UserModel> (User);
			data = string.Format ("user= {0}", data) ;
			WebClient client = new WebClient ();
			client.Headers [HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
			string result = client.UploadString (_signupurl, data);//client.UploadString (_signupurl, "{data: 'believe me!'}");//

			SignupResultModel model = JsonTools.Deserialize<SignupResultModel> (result);

			if (OnSignupCompleted != null)
				OnSignupCompleted (this, model);
		}

		#endregion
	}
}

