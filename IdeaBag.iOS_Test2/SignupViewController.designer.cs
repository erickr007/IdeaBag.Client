// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace IdeaBag.Client.iOS
{
	[Register ("SignupViewController")]
	partial class SignupViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCreate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField tbEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField tbPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCreate != null) {
				btnCreate.Dispose ();
				btnCreate = null;
			}
			if (tbEmail != null) {
				tbEmail.Dispose ();
				tbEmail = null;
			}
			if (tbPassword != null) {
				tbPassword.Dispose ();
				tbPassword = null;
			}
		}
	}
}
