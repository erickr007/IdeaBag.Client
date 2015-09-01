using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IdeaBag.Portable.Data.Models
{
    [DataContract]
    public class LogMessageModel
    {
        #region Private Properties

        private Guid _globalID;
        private LogStatus _status;
        private string _message;
        private int _code;
        private DateTime _dateCreated;

        #endregion


        #region Public Properties

        public Guid GlobalID
        {
            get { return _globalID; }
        }

        public LogStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        #endregion


        #region Constructors

        public LogMessageModel(LogStatus status, string message, int code)
        {
            this._globalID = Guid.NewGuid();
            this._status = status;
            this._message = message;
            this._code = code;
            this._dateCreated = DateTime.UtcNow;
        }

        #endregion
    }

    public enum LogStatus
    {
        Error = 0,
        Normal = 1,
        Warning = 2,
        Debug = 3
    }
}
