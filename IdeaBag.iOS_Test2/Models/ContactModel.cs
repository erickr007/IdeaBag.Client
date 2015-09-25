using System;

namespace IdeaBag.Client.iOS.Models
{
	public class ContactModel
	{
		#region Private Properties

		private int _contactID;
		private string _globalID;
		private string _email;
		private string _firstName;
		private string _lastName;
		private DateTime _dateCreated;

		#endregion


		#region Public Properties

		public int ContactID {
			get{ return _contactID; }
			set{ _contactID = value; }
		}

		public string GlobalID {
			get{ return _globalID; }
			set{ _globalID = value; }
		}

		public string Email {
			get{ return _email; }
			set{ _email = value; }
		}

		public string FirstName {
			get{ return _firstName; }
			set{ _firstName = value; }
		}

		public string LastName {
			get{ return _lastName; }
			set{ _lastName = value; }
		}

		public DateTime DateCreated {
			get{ return _dateCreated; }
			set{ _dateCreated = value; }
		}

		#endregion

		#region Constructor

		public ContactModel ()
		{
			_contactID = -1;
			_email = "";
			_firstName = "";
			_lastName = "";
			_globalID = "";
			_dateCreated = DateTime.UtcNow;
		}

		#endregion
	}
}

