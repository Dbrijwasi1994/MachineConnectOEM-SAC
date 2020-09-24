using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    [Serializable]
    public class LiveDTO : DTOBase, IEquatable<LiveDTO>
    {
        private string machine_id;       
        private double cut_time;       
        private long power_on_time;        
        private DateTime _cncTimeStamp;       
        private double _operatingTime;       
	
        public double OperatingTime
        {
            get { return _operatingTime; }
            set { _operatingTime = value; }
        }

        public DateTime CNCTimeStamp
        {
            get { return _cncTimeStamp; }
            set { _cncTimeStamp = value; }
        }

        public long POWER_ON_TIME
        {
            get { return power_on_time; }
            set { power_on_time = value; }
        }       

        public double CUT_TIME
        {
            get { return cut_time; }
            set { cut_time = value; }
        }

        public string MID
        {
            get { return machine_id; }
            set { machine_id = value; }
        }     
          

        public void Clear()
        {
            machine_id = string.Empty;            
            cut_time = 0;           
            power_on_time = 0;
            this._operatingTime = 0;
            _cncTimeStamp = DateTime.MinValue;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LiveDTO);
        }

        public bool Equals(LiveDTO obj)
        {
            if ((object)obj == null)
            {
                return false;
            }
            return false;
        }
        //TODO do it in a correct way
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
