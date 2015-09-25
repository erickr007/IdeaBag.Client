using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using IdeaBag.Client.iOS.Models;
using IdeaBag.Portable.Data;
using IdeaBag.Portable.Data.Models;


namespace IdeaBag.Client.iOS.DataAccess
{
	public class DatabaseManager
	{

		#region Private Properties

		private static DatabaseManager _instance;
		private string _dbpath;

		#endregion


		#region Public Properties

		public static DatabaseManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DatabaseManager();
				}

				return _instance;
			}
		}

		#endregion


		#region Constructor

		private DatabaseManager(){}

		#endregion



		#region Initialize


		/// <summary>
		/// Create database if it does not exist
		/// </summary>
		public void Initialize(string dbpath){

			_dbpath = dbpath;

			//- Create database file and insert data tables
			if (!File.Exists (_dbpath)) {
				try{
					//- Create database
					File.Create (_dbpath);

					//--- Insert database tables ---//

					// Application Settings
					string cmd = DatabaseScripts.CreateApplicationSettingsTableScript();
					int result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);

					//- insert the device id
					ApplicationSettingsModel appsettings = new ApplicationSettingsModel();
					appsettings.DeviceID = Guid.NewGuid().ToString();
					InsertInitialApplicationSettings(appsettings);

					// User Settings
					cmd = DatabaseScripts.CreateUsersTableScript();
					result = DatabaseHelper.ExecuteNonQuery(_dbpath,cmd);

					// Contacts
					cmd = DatabaseScripts.CreateContactsTableScript();
					result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);

					// Messages
					cmd = DatabaseScripts.CreateMessageTableScript();
					result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);

					// Log Messages
					cmd = DatabaseScripts.CreateLogMessagesTableScript();
					result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);

					// Log Message Types
					cmd = DatabaseScripts.CreateLogMessageTypesScript();
					result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);


				}
				catch(Exception ex){
					
				}
			}
		}

		#endregion


		#region Users

		/// <summary>
		/// Syncs the user with server.
		/// </summary>
		public async Task<UserModel> SyncUserWithServer(string email, string serverurl){
			RestDataManager restmanager = new RestDataManager (serverurl);

			UserModel serveruser = null;
			UserModel localuser = null;

			try {
				Task getserver = Task.Factory.StartNew (async () => {
					serveruser = await restmanager.GetUserInfo (email);
				});

				Task getlocal = Task.Factory.StartNew (async () => {
					localuser = await GetUserDetails (email);
				});

				if(serveruser == null)
					throw new Exception(string.Format("Unable to retrieve user: {0} from server: {1}", email,serverurl));

				//- Insert user locally if not available on client database
				if(localuser == null)
					InsertUserDetails(serveruser);

				Task.WaitAll (new Task[]{ getserver, getlocal });

				//- update local user if changes have been made on the server
				if (serveruser != null && localuser != null && serveruser.LastModified > localuser.LastModified) {
					UpdateUser (serveruser);
				}

				return serveruser;

			} catch (Exception ex) {
				throw ex;
			}

		}

		#region Select

		public async Task<UserModel> GetUserDetails(string email){
			UserModel user = null;

			string cmd = string.Format ("SELECT * FROM Users WHERE Email = '{0}'", email);
			Task<UserModel> usertask = Task.Run (() => {
				try {
					List<UserModel> users = DatabaseHelper.ExecuteQuery<UserModel> (_dbpath, cmd);

					if(users.Count > 0)
						return users[0];
					else
						throw new Exception(string.Format("No users found with the provided email address: {0}",email));
				} 
				catch (Exception ex) {
					Debug.WriteLine (string.Format ("The following exception occurred in (ClientDatabaseManager.GetUserSettings) {0}", ex.Message));
					return null;
				}
			});

			return await usertask;
		}

		#endregion


		#region Insert

		public async Task InsertUserDetails(UserModel user){
			string cmd = string.Format("INSERT INTO Users(GlobalID, Email, Password, LoginType_FK, FirstName, LastName, HomeAddress, DateCreated, LastModified)"
				+ "VALUES('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}')",
				user.GlobalID,
				user.UserID,
				user.PasswordHash,
				user.UserLoginType,
				user.FirstName,
				user.LastName,
				"",
				user.CreateDate,
				user.LastModified);

			Task insertusertask = Task.Run (() => {
				int result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);

			});

			await insertusertask;

			return;
		}

		#endregion


		#region Update

		public async Task UpdateUser(UserModel user){
			string cmd = string.Format ("UPDATE Users Set PasswordHash = {0}, FirstName = {1}, Lastname = {2}, " +
			             "LastModified = {3} WHERE UserID = {4}", user.PasswordHash, user.FirstName, user.LastName,
				             DateTime.UtcNow, user.UserID);

			Task updateusertask = Task.Run (() => {
				int result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);
			});

			await updateusertask;

			return;

		}

		#endregion

		#endregion


		#region Application Settings

		#region GET

		public async Task<ApplicationSettingsModel> GetApplicationSettings()
		{
			ApplicationSettingsModel settings = new ApplicationSettingsModel();

			string cmd = "SELECT DeviceID FROM ApplicationSettings";

			try
			{
				List<ApplicationSettingsModel> settingsresult = DatabaseHelper.ExecuteQuery<ApplicationSettingsModel>(_dbpath, cmd);


			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.GetApplicationSettings) {0}", ex.Message));
			}

			return settings;
		}

		#endregion


		#region INSERT

		public async void InsertInitialApplicationSettings(ApplicationSettingsModel settings)
		{
			string cmd = string.Format("INSERT INTO ApplicationSettings(DeviceID) VALUES('{0}');", settings.DeviceID);

			try
			{
				int result = DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);


			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.InsertInitialApplicationSettings) {0}", ex.Message));
			}
		}

		#endregion

		#endregion


		#region Contacts

		#region Sync

		/// <summary>
		/// Gets all contacts created on server after the last contact was created locally
		/// </summary>
		public async Task SyncUserContacts(string useremail, string serverurl){

			try {
				//- Get User's GlobalID
				UserModel user = await GetUserDetails (useremail);

				if (user == null)
					throw new Exception ("User not found while trying to sync user contacts");

				//- Get the last added contact added to local database
				List<ContactModel> contacts = await GetUserContacts (user.GlobalID);

				if(contacts.Count == 0)
					return;
				
				//- Get all contacts created on Server after the last local contact was created
				DateTime lastContactCreated = contacts[contacts.Count - 1].DateCreated;
				RestDataManager restmanager = new RestDataManager(serverurl);

				List<UserModel> recentcontacts = await restmanager.GetUserContacts(useremail, lastContactCreated);

				//- add new contacts to local database
				foreach(UserModel u in recentcontacts)
					await InsertContact(u);
				


			} catch (Exception ex) {
				Debug.WriteLine (string.Format ("The following exception occurred in (ClientDatabaseManager.SyncUserContacts) {0}", ex.Message));
				return;
			}


		}

		#endregion


		#region GET

		public async Task<List<ContactModel>> GetUserContacts(string userGlobalID){
			string cmd = string.Format ("SELECT Contacts.GlobalID, Contacts.Email, Contacts.FirstName, Contacts.LastName, " +
			             "Contacts.DateCreated FROM Contacts INNER JOIN UserContactLinks ucl ON ucl.Contact_FK = Contacts.GlobalID " +
			             "WHERE ucl.User_FK = '{0}' ORDER BY DateCreated", userGlobalID);
			List<ContactModel> contacts = new List<ContactModel> ();
			try{
				contacts = DatabaseHelper.ExecuteQuery<ContactModel>(_dbpath, cmd);
			}
			catch(Exception ex){
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.GetUserContacts) {0}", ex.Message));
			}

			return contacts;
		}

		/// <summary>
		/// Gets the contact details based on Email address
		/// </summary>
		public async Task<ContactModel> GetContactDetails(string email){
			ContactModel contact = null;
			string cmd = string.Format ("SELECT * FROM Contacts WHERE Email = {0}", email);

			try{
				List<ContactModel> contacts = DatabaseHelper.ExecuteQuery<ContactModel>(_dbpath, cmd);

				if(contacts.Count > 0)
					contact = contacts[0];
			}
			catch(Exception ex){
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.GetContactDetails) {0}", ex.Message));
			}

			return contact;
		}

		#endregion


		#region INSERT

		public async Task InsertContact(UserModel user){
			string cmd = string.Format ("INSERT INTO Contacts(GlobalID, Email, FirstName, LastName, DateCreated) " +
			             "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", user.GlobalID, user.UserID, user.FirstName, user.LastName, user.CreateDate);

			try{
				DatabaseHelper.ExecuteNonQuery(_dbpath, cmd);

			}
			catch(Exception ex){
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.InsertLocalContact) {0}", ex.Message));
			}

			return;
		}

		#endregion

		#endregion

		/*

		#region Messages

		#region GET

		/// <summary>
		/// Get all messages for the specified user
		/// </summary>
		/// <param name="userid">Identifier for the user account</param>
		public async Task<List<MessageModel>> GetUserMessages(string userid)
		{
			List<MessageModel> messages = new List<MessageModel>();

			string cmd = string.Format("SELECT * FROM Messages WHERE TargetUser_FK = {0}",userid);

			try
			{
				SQLiteWinRT.Database db = await App.GetDatabaseAsync();
				SQLiteWinRT.Statement statement = await DatabaseHelper.ExecuteQueryStatement(db, cmd, "Getting user messages from database");

				while (await statement.StepAsync())
				{
					MessageModel message = new MessageModel();
					message.EventStartDate = DateTime.Parse(statement.Columns["EventStartDate"]);
					message.GlobalID = statement.Columns["GlobalID"];
					message.IsEvent = bool.Parse(statement.Columns["IsEvent"]);
					message.Latitude = double.Parse(statement.Columns["Latitude"]);
					message.Longitude = double.Parse(statement.Columns["Longitude"]);
					message.Message = statement.Columns["Message"];
					message.SendDate = DateTime.Parse(statement.Columns["SendDate"]);
					message.SourceUserID = statement.Columns["SourceUser_FK"];
					message.TargetUserID = statement.Columns["TargetUser_FK"];
					message.Title = statement.Columns["Title"];

					messages.Add(message);
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.GetUserMessages) {0}", ex.Message));
			}

			return messages;
		}

		public List<MessageModel> GetUserMessages(string userid, int startindex, int endindex)
		{
			List<MessageModel> messages = new List<MessageModel>();

			string cmd = "SELECT * FROM Messages WHERE UserID_FK = @UserID_FK and MessageIndex >= @StartIndex and MessageIndex < @EndIndex";

			return messages;
		}

		/// <summary>
		/// Get the most recent message added to the database
		/// </summary>
		public async Task<MessageModel> GetLastKnownMessage()
		{
			string cmd = "SELECT * FROM Messages ORDER BY SendDate DESC LIMIT 1";
			var availDB = await App.GetDatabaseAsync();

			var statement = await DatabaseHelper.ExecuteQueryStatement(availDB, cmd, "Get latest added message");

			//- Get latest message if one exists
			MessageModel lastKnownMessage = null;
			while (await statement.StepAsync())
			{
				//"INSERT INTO Messages(GlobalID, SourceUser_FK, TargetUser_FK, Title, Message, SendDate, Latitude, Longitude, IsEvent, EventStartDate) "
				lastKnownMessage = new MessageModel();                
				lastKnownMessage.GlobalID = statement.GetTextAt(0);
				lastKnownMessage.IsEvent = bool.Parse(statement.GetTextAt(8));
				lastKnownMessage.Latitude = statement.GetDoubleAt(6);
				lastKnownMessage.Longitude = statement.GetDoubleAt(7);
				lastKnownMessage.Message = statement.GetTextAt(4);
				lastKnownMessage.SendDate = DateTime.Parse(statement.GetTextAt(3));
				lastKnownMessage.SourceUserID = statement.GetTextAt(1);
				lastKnownMessage.TargetUserID = statement.GetTextAt(2);
				lastKnownMessage.Title = statement.GetTextAt(5);

				if (!string.IsNullOrEmpty(statement.GetTextAt(9)))
					lastKnownMessage.EventStartDate = DateTime.Parse(statement.GetTextAt(9));

				break;
			}

			return lastKnownMessage;
		}

		public List<string> GetMessageLinks(string userid, string messageid)
		{
			return new List<string>();
		}

		public List<byte[]> GetMessageImages(string userid, string messageid)
		{
			return new List<byte[]>();
		}

		#endregion


		#region INSERT

		/// <summary>
		/// Retrieves most recent message in local storage and Reqeusts all server messages receieved after
		/// </summary>
		public async Task SyncDatabase(string userid)
		{
			DateTime lastknowndate = DateTime.Parse("05-04-1999");

			//- Get last message received
			MessageModel lastmessage = await GetLastKnownMessage();

			//- Get date from the last message received
			if (lastmessage != null)
			{
				lastknowndate = lastmessage.SendDate;
			}

			string url = string.Format("http://localhost:49697/rest/content/GetUserMessagesByDate?userid={0}&lkd={1}", userid, lastknowndate);


			//- Send async request for server to send unreceived user messages
			List<MessageModel> messages = new List<MessageModel>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("accept", "application/json");
				HttpResponseMessage response = await client.GetAsync(url);

				//- Read response string and remove unnecessary characters
				string data = await response.Content.ReadAsStringAsync();
				data = data.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");

				messages = RealtimeHub.Portable.Utilty.JsonTools.Deserialize<List<MessageModel>>(data);
			}

			//- Add new messages to local storage
			foreach (MessageModel m in messages)
				InsertUserMessage(userid, m);

		}

		/// <summary>
		/// Adds message to client database
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="message"></param>
		public async void InsertUserMessage(string userid, MessageModel message)
		{
			string cmd = string.Format("INSERT INTO Messages(GlobalID, SourceUser_FK, TargetUser_FK, Title, Message, SendDate, Latitude, Longitude, IsEvent, EventStartDate) " +
				"VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, '{8}', '{9}');", message.GlobalID, message.SourceUserID, message.TargetUserID, message.Title, message.Message, 
				message.SendDate, message.Latitude, message.Longitude, message.IsEvent, message.EventStartDate);                

			try
			{
				SQLiteWinRT.Database db = await App.GetDatabaseAsync();
				await DatabaseHelper.ExecuteNonQueryStatement(db, cmd, "Inserting new user message");


			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("The following exception occurred in (ClientDatabaseManager.InsertUserMessage) {0}", ex.Message));
			}
		}

		#endregion


		#region DELETE

		public void DeleteUserMessage(string userid, string messageid)
		{

		}

		#endregion

		#endregion


		#region Connections

		public List<UserModel> GetUserContacts(string userid)
		{
			return new List<UserModel>();
		}

		#endregion
		*/
	}
}

