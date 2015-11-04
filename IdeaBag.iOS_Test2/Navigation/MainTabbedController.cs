using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace IdeaBag.Client.iOS
{
	partial class MainTabbedController : UITabBarController
	{
		UIViewController _settingsTab;
		UIViewController _mapTab;
		UIViewController _ideasTab;
		UIViewController _contactsTab;
		UIViewController _notificationsTab;

		public MainTabbedController (IntPtr handle) : base (handle)
		{
			///  Setup Tabs 

			//- Settings
			_settingsTab = new UIViewController();
			_settingsTab.Title = "Settings";
			_settingsTab.View.BackgroundColor = UIColor.Red;

			//- Ideas
			_ideasTab = new UIViewController();
			_ideasTab.Title = "Ideas";
			_ideasTab.View.BackgroundColor = UIColor.Purple;

			//- Notifications
			_notificationsTab = new UIViewController();
			_notificationsTab.Title = "Notifications";
			_notificationsTab.View.BackgroundColor = UIColor.Yellow;

			//- Contacts
			_contactsTab = new UIViewController();
			_contactsTab.Title = "Friends";
			_contactsTab.View.BackgroundColor = UIColor.Green;

			//- Map
			_mapTab = new UIViewController();
			_mapTab.Title = "Map";
			_mapTab.View.BackgroundColor = UIColor.Blue;

			var tabs = new UIViewController[] {
				_settingsTab, 
				_ideasTab, 
				_notificationsTab,
				_contactsTab,
				_mapTab
			};

			this.ViewControllers = tabs;

		}

		public override void ViewDidLoad ()
		{
			_settingsTab = new UIViewController();
			_settingsTab.Title = "Settings";
			_settingsTab.View.BackgroundColor = UIColor.Red;

			//- Ideas
			_ideasTab = new UIViewController();
			_ideasTab.Title = "Ideas";
			_ideasTab.View.BackgroundColor = UIColor.Purple;

			//- Notifications
			_notificationsTab = new UIViewController();
			_notificationsTab.Title = "Notifications";
			_notificationsTab.View.BackgroundColor = UIColor.Yellow;

			//- Contacts
			_contactsTab = new UIViewController();
			_contactsTab.Title = "Friends";
			_contactsTab.View.BackgroundColor = UIColor.Green;

			//- Map
			_mapTab = new UIViewController();
			_mapTab.Title = "Map";
			_mapTab.View.BackgroundColor = UIColor.Blue;

			var tabs = new UIViewController[] {
				_settingsTab, 
				_ideasTab, 
				_notificationsTab,
				_contactsTab,
				_mapTab
			};

			this.ViewControllers = tabs;
			base.ViewDidLoad ();
		}
	}
}
