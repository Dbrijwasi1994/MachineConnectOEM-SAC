using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MachineConnectOEM
{
    public class ActivityInfoEntity : INotifyPropertyChanged
    {
        internal bool IsRowChanged = false;
        public ActivityInfoEntity()
        {
            SerialNum = 0;
        }

        private int serialNum;
        public int SerialNum
        {
            get { return serialNum; }
            set { serialNum = value; }
        }

        private int activityID;
        public int ActivityID
        {
            get { return activityID; }
            set
            {
                activityID = value;
                NotifyPropertyChanged("ActivityID");
            }
        }

        private string activity;
        public string Activity
        {
            get { return activity; }
            set
            {
                activity = value;
                NotifyPropertyChanged("Activity");
            }
        }

        private string frequency;
        public string Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                NotifyPropertyChanged("Frequency");
            }
        }

        private int freqID;
        public int FreqID
        {
            get { return freqID; }
            set
            {
                freqID = value;
                NotifyPropertyChanged("FreqID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                IsRowChanged = true;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
