using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IdeaBag.Portable.Data.Models
{
    [DataContract]
    public class MessageModel
    {
        #region Private Properties

        /// <summary>
        /// Unique identifier for this messsage
        /// </summary>
        private string _globalID;

        /// <summary>
        /// UserID of the user this message will be delivered to
        /// </summary>
        private string _targetUserID;

        /// <summary>
        /// UserID of the user sending this message
        /// </summary>
        private string _sourceUserID;

        /// <summary>
        /// Date this message was sent
        /// </summary>
        private DateTime _sendDate;

        /// <summary>
        /// Content of the message
        /// </summary>
        private string _message;

        /// <summary>
        /// Title of the message
        /// </summary>
        private string _title;

        /// <summary>
        /// Latitude coordinate of the message's associated location
        /// </summary>
        private double _latitude;

        /// <summary>
        /// Longitude coordinate of the message's associated location
        /// </summary>
        private double _longitude;

        /// <summary>
        /// Determines if this message is associated with a specific event
        /// </summary>
        private bool _isEvent;

        /// <summary>
        /// Date of the event (If this message is associated with one)
        /// </summary>
        private DateTime _eventStartDate;

        #endregion


        #region Public Properties

        /// <summary>
        /// UserID of the user this message will be delivered to
        /// </summary>
        [DataMember]
        public string GlobalID
        {
            get { return _globalID; }
            set { _globalID = value; }
        }

        /// <summary>
        /// UserID of the user this message will be delivered to
        /// </summary>
        [DataMember]
        public string TargetUserID
        {
            get { return _targetUserID; }
            set { _targetUserID = value; }
        }

        /// <summary>
        /// UserID of the user sending this message
        /// </summary>
        [DataMember]
        public string SourceUserID
        {
            get { return _sourceUserID; }
            set { _sourceUserID = value; }
        }

        /// <summary>
        /// Date this message was sent
        /// </summary>
        [DataMember]
        public DateTime SendDate
        {
            get { return _sendDate; }
            set { _sendDate = value; }
        }

        /// <summary>
        /// Content of the message
        /// </summary>
        [DataMember]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Title of the message
        /// </summary>
        [DataMember]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Latitude coordinate of the message's associated location
        /// </summary>
        [DataMember]
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        /// <summary>
        /// Longitude coordinate of the message's associated location
        /// </summary>
        [DataMember]
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        /// <summary>
        /// Determines if this message is associated with a specific event
        /// </summary>
        [DataMember]
        public bool IsEvent
        {
            get { return _isEvent; }
            set { _isEvent = value; }
        }

        /// <summary>
        /// Date of the event (If this message is associated with one)
        /// </summary>
        [DataMember]
        public DateTime EventStartDate
        {
            get { return _eventStartDate; }
            set { _eventStartDate = value; }
        }
        #endregion


        #region Constructor

        public MessageModel()
        {
            this._globalID = Guid.NewGuid().ToString();
            this._isEvent = false;
            this._message = "";
            this._title = "";
            this._sourceUserID = "";
            this._targetUserID = "";
            this._sendDate = DateTime.UtcNow;
        }

        #endregion

    }
}
