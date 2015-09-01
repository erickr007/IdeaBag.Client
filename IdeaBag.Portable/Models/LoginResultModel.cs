using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IdeaBag.Portable.Data.Models
{
    [DataContract]
    public class LoginResultModel
    {
        #region Private Properties

        private LoginResultType _resultStatus;
        private string _message;
        private List<string> _connectionIDs;

        #endregion


        #region Public Properties

        [DataMember]
        public LoginResultType ResultStatus
        {
            get { return _resultStatus; }
            set { _resultStatus = value; }
        }

        [DataMember]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [DataMember]
        public List<string> ConnectionIDs
        {
            get { return _connectionIDs; }
            set { _connectionIDs = value; }
        }


        #endregion
    }

    public enum LoginResultType
    {
        Success = 0,
        UserNotFound = 1,
        UserDisabled = 2,
        UnexpectedException = 3
    }
}
