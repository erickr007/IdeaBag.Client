using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using IdeaBag.Client.iOS.ViewModels;
using IdeaBag.Portable.Data.Models;

namespace IdeaBag.Client.iOS
{


	partial class SignupViewController : UIViewController
	{
		#region Private Properties

		private SignupViewModel _viewmodel;
		private UserModel _user;
		private string _signupurl = "http://idea-bag.com/authentication/signupstandarduser";//"http://idea-bag.com/authentication/echoposttest";//
		#endregion


		#region Constructor

		public SignupViewController (IntPtr handle) : base (handle)
		{
			this._viewmodel = new SignupViewModel (_signupurl);
			this._viewmodel.OnSignupCompleted += HandleSignupCompleted;
		}

		#endregion


		#region UIViewController Overrides

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			tbEmail.EditingChanged +=	(sender, e) => {
				_viewmodel.User.UserID = tbEmail.Text;
			};

			tbPassword.EditingChanged += (sender, e) => {
				_viewmodel.User.PasswordHash = tbPassword.Text;
			};

			btnCreate.TouchUpInside += _viewmodel.SignupTouchUpInside;

		}

		#endregion


		#region UI Handlers

		private void HandleSignupCompleted(object sender, SignupResultModel e){

		}

		#endregion
	}
}
