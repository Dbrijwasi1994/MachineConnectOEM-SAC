using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MachineConnectOEM.SAC
{
    public class DTO
    {
        private string _ParameterId;
        private string _parameterName;
        private string _minValue;
        private string _maxValue;
        private string _unit;
        private int _templateType;
        private string _backgroundColor;
        private string _parameterValue;


        public string ParameterId
        {
            get { return _ParameterId; }
            set { _ParameterId = value; }
        }

        public string ParameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }

        public string MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public string MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public int TemplateType
        {
            get { return _templateType; }
            set { _templateType = value; }
        }

        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public string ParameterValue
        {
            get { return _parameterValue; }
            set { _parameterValue = value; }
        }
    }

    public class Frequency : INotifyPropertyChanged
    {
        public Frequency()
        {
            color = "Black";
        }
        public string freqID { get; set; }
        public string frequency { get; set; }
        public string freqValue { get; set; }
        public string freqType { get; set; }

        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                NotifyPropertyChanged("Color");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class DateAndVals
    {
        private DateTime _date;
        private string _val;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string Val
        {
            get { return _val; }
            set { _val = value; }
        }
    }

    public class IRScheduleDto
    {
        private int _slNo;
        private string _item;
        private string _act;
        private string _freq;
        private DateTime _lastUpdated;
        private DateTime _date;
        private string _val;

        public int SlNo
        {
            get { return _slNo; }
            set { _slNo = value; }
        }
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public string Act
        {
            get { return _act; }
            set { _act = value; }
        }
        public string Freq
        {
            get { return _freq; }
            set { _freq = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set { _lastUpdated = value; }
        }
        public string Val
        {
            get { return _val; }
            set { _val = value; }
        }

    }

    public class NotificationData
    {
        public string MachineID { get; set; }
        public string Activity { get; set; }
        public string Frequency { get; set; }
        public string DueDate { get; set; }
        public string NotificationDataTitle { get; set; }
    }

    public class NotificationDetails
    {
        public ObservableCollection<NotificationData> WarningData { get; set; }
        public ObservableCollection<NotificationData> PendingData { get; set; }
    }
}
