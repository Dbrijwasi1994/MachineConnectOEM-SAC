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
}
