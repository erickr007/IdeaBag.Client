using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IdeaBag.Portable.Data.Models
{
    public class ApplicationSettingsModel : INotifyPropertyChanged
    {
        #region Private Properties

        private string _deviceID;

        #endregion


        #region Public Properties

        public string DeviceID
        {
            get { return _deviceID; }
            set
            {
                _deviceID = value;
                OnPropertyChanged("DeviceID");
            }
        }

        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Constructor

        public ApplicationSettingsModel() { }

        #endregion


        #region INotifyPropertyChanged Method

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

    }
}
