using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IdeaBag.Portable.Data.Models
{
    [DataContract]
    public class SignupResultModel
    {

        #region Private Properties

        private SignupResultType _resultStatus;
        private string _message;

        #endregion


        #region Public Properties

        [DataMember]
        public SignupResultType ResultStatus
        {
            get { return _resultStatus; }
            set { _resultStatus = value; }
        }

        [DataMember]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
            }
        }

        #endregion

    }

    public enum SignupResultType
    {
        Success = 0,
        UserExists = 1,
        UnexpectedException = 2
    }

}
