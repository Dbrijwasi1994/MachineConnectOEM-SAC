using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MachineConnectOEM.DTO
{
    class ProcessParamConfigModel : INotifyPropertyChanged
    {
        public int SerialNum { get; set; }
        public int IDD { get; set; }
        internal bool IsRowChanged = false;
        public ProcessParamConfigModel()
        {
            IsVisible = false;
            SortOrder = 0;
        }

        private int _ParameterId;
        public int ParameterId
        {
            get { return _ParameterId; }
            set { if (_ParameterId != value) { _ParameterId = value; RaisePropertyChanged("_ParameterId"); } }
        }

        private string _ParameterName;
        public string ParameterName
        {
            get { return _ParameterName; }
            set { if (_ParameterName != value) { _ParameterName = value; RaisePropertyChanged("_ParameterName"); } }
        }

        private string _MinValue;
        public string MinValue
        {
            get { return _MinValue; }
            set { if (_MinValue != value) { _MinValue = value; RaisePropertyChanged("_MinValue"); } }
        }

        private string _MaxValue;
        public string MaxValue
        {
            get { return _MaxValue; }
            set { if (_MaxValue != value) { _MaxValue = value; RaisePropertyChanged("_MaxValue"); } }
        }

        private string _WarningValue;
        public string WarningValue
        {
            get { return _WarningValue; }
            set { if (_WarningValue != value) { _WarningValue = value; RaisePropertyChanged("_WarningValue"); } }
        }

        private string _RedBit;
        public string RedBit
        {
            get { return _RedBit; }
            set { if (_RedBit != value) { _RedBit = value; RaisePropertyChanged("_RedBit"); } }
        }

        private string _RedValue;
        public string RedValue
        {
            get { return _RedValue; }
            set { if (_RedValue != value) { _RedValue = value; RaisePropertyChanged("_RedValue"); } }
        }

        private string _GreenBit;
        public string GreenBit
        {
            get { return _GreenBit; }
            set { if (_GreenBit != value) { _GreenBit = value; RaisePropertyChanged("_GreenBit"); } }
        }

        private string _GreenValue;
        public string GreenValue
        {
            get { return _GreenValue; }
            set { if (_GreenValue != value) { _GreenValue = value; RaisePropertyChanged("_GreenValue"); } }
        }

        private string _YellowBit;
        public string YellowBit
        {
            get { return _YellowBit; }
            set { if (_YellowBit != value) { _YellowBit = value; RaisePropertyChanged("_YellowBit"); } }
        }

        private string _YellowValue;
        public string YellowValue
        {
            get { return _YellowValue; }
            set { if (_YellowValue != value) { _YellowValue = value; RaisePropertyChanged("_YellowValue");} }
        }

        private string _Red1Bit;
        public string Red1Bit
        {
            get { return _Red1Bit; }
            set { if (_Red1Bit != value) { _Red1Bit = value; RaisePropertyChanged("_Red1Bit"); } }
        }

        private string _Red1HValue;
        public string Red1HValue
        {
            get { return _Red1HValue; }
            set { if (_Red1HValue != value) { _Red1HValue = value; RaisePropertyChanged("_Red1HValue"); } }
        }

        private string _Red1LValue;
        public string Red1LValue
        {
            get { return _Red1LValue; }
            set { if (_Red1LValue != value) { _Red1LValue = value; RaisePropertyChanged("_Red1LValue"); } }
        }

        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set { if (_Unit != value) { _Unit = value; RaisePropertyChanged("_Unit"); } }
        }

        private string _TemplateType;
        public string TemplateType
        {
            get { return _TemplateType; }
            set { if (_TemplateType != value) { _TemplateType = value; RaisePropertyChanged("_TemplateType"); } }
        }

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { if (_IsVisible != value) { _IsVisible = value; RaisePropertyChanged("_IsVisible"); } }
        }

        private int _SortOrder;
        public int SortOrder
        {
            get { return _SortOrder; }
            set { if (_SortOrder != value) { _SortOrder = value; RaisePropertyChanged("_SortOrder"); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
                IsRowChanged = true;
            }
        }
    }
    class ProcessParamConfigModelNew : INotifyPropertyChanged
    {
        public int SerialNum { get; set; }
        public int IDD { get; set; }
        internal bool IsRowChanged = false;
        public ProcessParamConfigModelNew()
        {
            IsVisible = false;
            SortOrder = 0;
        }

        private string _ParameterId;
        public string ParameterId
        {
            get { return _ParameterId; }
            set { if (_ParameterId != value) { _ParameterId = value; RaisePropertyChanged("_ParameterId"); } }
        }

        private string _ParameterName;
        public string ParameterName
        {
            get { return _ParameterName; }
            set { if (_ParameterName != value) { _ParameterName = value; RaisePropertyChanged("_ParameterName"); } }
        }

        private string _DisplayText;
        public string DisplayText
        {
            get { return _DisplayText; }
            set { if (_DisplayText != value) { _DisplayText = value; RaisePropertyChanged("_DisplayText"); } }
        }


        private string _LowerValue;
        public string LowerValue
        {
            get { return _LowerValue; }
            set { if (_LowerValue != value) { _LowerValue = value; RaisePropertyChanged("_LowerValue"); } }
        }

        private string _HigherValue;
        public string HigherValue
        {
            get { return _HigherValue; }
            set { if (_HigherValue != value) { _HigherValue = value; RaisePropertyChanged("_HigherValue"); } }
        }

        private string _GroupId;
        public string GroupId
        {
            get { return _GroupId; }
            set { if (_GroupId != value) { _GroupId = value; RaisePropertyChanged("_GroupId"); } }
        }

        private double _Freqency;
        public double Freqency
        {
            get { return _Freqency; }
            set { if (_Freqency != value) { _Freqency = value; RaisePropertyChanged("_Freqency"); } }
        }

        private string _TemplateType;
        public string TemplateType
        {
            get { return _TemplateType; }
            set { if (_TemplateType != value) { _TemplateType = value; RaisePropertyChanged("_TemplateType"); } }
        }

        private string _DBDataType;
        public string DBDataType
        {
            get { return _DBDataType; }
            set { if (_DBDataType != value) { _DBDataType = value; RaisePropertyChanged("_DBDataType"); } }
        }

        private string _Register;
        public string Register
        {
            get { return _Register; }
            set { if (_Register != value) { _Register = value; RaisePropertyChanged("_Register"); } }
        }

        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set { if (_Unit != value) { _Unit = value; RaisePropertyChanged("_Unit"); } }
        }

        private string _HighRedLimit;
        public string HighRedLimit
        {
            get { return _HighRedLimit; }
            set { if (_HighRedLimit != value) { _HighRedLimit = value; RaisePropertyChanged("_HighRedLimit"); } }
        }

        private string _LowRedLimit;
        public string LowRedLimit
        {
            get { return _LowRedLimit; }
            set { if (_LowRedLimit != value) { _LowRedLimit = value; RaisePropertyChanged("_LowRedLimit"); } }
        }

        private string _HighGreenLimit;
        public string HighGreenLimit
        {
            get { return _HighGreenLimit; }
            set { if (_HighGreenLimit != value) { _HighGreenLimit = value; RaisePropertyChanged("_HighGreenLimit"); } }
        }

        private string _LowGreenLimit;
        public string LowGreenLimit
        {
            get { return _LowGreenLimit; }
            set { if (_LowGreenLimit != value) { _LowGreenLimit = value; RaisePropertyChanged("_LowGreenLimit"); } }
        }

        private string _HighYellowLimit;
        public string HighYellowLimit
        {
            get { return _HighYellowLimit; }
            set { if (_HighYellowLimit != value) { _HighYellowLimit = value; RaisePropertyChanged("_HighYellowLimit"); } }
        }

        private string _LowYellowLimit;
        public string LowYellowLimit
        {
            get { return _LowYellowLimit; }
            set { if (_LowYellowLimit != value) { _LowYellowLimit = value; RaisePropertyChanged("_LowYellowLimit"); } }
        }

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { if (_IsVisible != value) { _IsVisible = value; RaisePropertyChanged("_IsVisible"); } }
        }

        private int _SortOrder;
        public int SortOrder
        {
            get { return _SortOrder; }
            set { if (_SortOrder != value) { _SortOrder = value; RaisePropertyChanged("_SortOrder"); } }
        }

        private int? _DivideBy;
        public int? DivideBy
        {
            get { return _DivideBy; }
            set { if (_DivideBy != value) { _DivideBy = value; RaisePropertyChanged("_DivideBy"); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
                IsRowChanged = true;
            }
        }
    }
}
