using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using IdeaBag.Client.iOS.ViewModels;
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
		string _loginurl = "http://idea-bag.com/authentication/loginstandarduser";

		#endregion


		#region Constructor

		public LoginViewController (IntPtr handle) : base (handle)
		{
			_viewmodel = new LoginViewModel (_loginurl);
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
			btnLogin.TouchUpInside += _viewmodel.LoginTouchUpInside;
		}


		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

		}

		#endregion


		#region UI Methods

		private void HandleLoginResult(object sender, LoginResultModel result){

			if (result.ResultStatus == LoginResultType.Success) {
				UIStoryboard board = UIStoryboard.FromName ("Main", null);

				UIViewController ctrl = (UIViewController)board.InstantiateViewController ("Ideas");
				this.PresentViewController (ctrl, true, null);
			}
		}

		#endregion

	}
}
