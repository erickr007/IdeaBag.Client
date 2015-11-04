using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using IdeaBag.Client.iOS.ViewModels;
using IdeaBag.Client.iOS.DataAccess;
using IdeaBag.Portable.Data;
using IdeaBag.Portable.Data.Models;
using IdeaBag.Portable.ViewModels;
using IdeaBag.Portable.Utilty;

namespace IdeaBag.Client.iOS
{
	partial class LoginViewController : UIViewController
	{
		#region Private Properties

		LoginViewModel _viewmodel;
		string _serverurl = "http://idea-bag.com";

		#endregion


		#region Constructor

		public LoginViewController (IntPtr handle) : base (handle)
		{
			_viewmodel = new LoginViewModel (_serverurl);
			_viewmodel.OnLoginCompleted +=	HandleLoginResult;
		}

		#endregion


		#region UIViewController Override Methods

		public override void ViewDidLoad(){
			base.ViewDidLoad ();

			//- Set handlers and bindings
			tbUsername.EditingChanged +=	(sender, e) => {
				_viewmodel.Username = tbUsername.Text;
			};
			tbPassword.EditingChanged += (sender, e) => {
				_viewmodel.Password = tbPassword.Text;
			};
			//btnLogin.TouchUpInside += _viewmodel.LoginTouchUpInside;

			//NotificationImage.Image = UIImage.FromBundle ("tabicon-notifications");


		}


		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

		}

		#endregion


		#region UI Methods

		private async void HandleLoginResult(object sender, LoginResultModel result){

			if (result.ResultStatus == LoginResultType.Success) {
				//- Synchronize this Users local database info with that stored on the Server
				UserModel user = await DatabaseManager.Instance.SyncUserWithServer(tbUsername.Text, _serverurl);


				lblValidation.Hidden = true;
				UIStoryboard board = UIStoryboard.FromName ("Main", null);

				UIViewController ctrl = (UIViewController)board.InstantiateViewController ("MainIdeas");
				//this.PresentViewController (ctrl, true, null);
			} 
			else if (result.ResultStatus == LoginResultType.UserNotFound) {
				lblValidation.Text = "User and/or Password not found.";
				lblValidation.Hidden = false;
			}
			else if (result.ResultStatus == LoginResultType.UserDisabled) {
				lblValidation.Text = "This user is not currently activated.";
				lblValidation.Hidden = false;
			}
		}

		#endregion

	}
}
