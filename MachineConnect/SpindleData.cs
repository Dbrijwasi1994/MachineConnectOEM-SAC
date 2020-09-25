using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MachineConnectApplication
{
    [Serializable]
    public class SpindleData
    {
        public DateTime ts { get; set; }
        public double ss { get; set; }
        public double sl { get; set; }
        public double st { get; set; }
        public string AxisNo { get; set; }
    }

    public class CycleInfo
    {
        public object _id { get; set; }
        public DateTime CycleStart { get; set; }
        public DateTime CycleEnd { get; set; }
    }

    public class ParameterCycleInfo : ICloneable
    {
        public object _id { get; set; }
        public int IDD { get; set; }
        public string MachineID { get; set; }
        public string ParameterID { get; set; }
        public double ParameterValue { get; set; }
        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedtimeStamp { get; set; }
        public string Part { get; set; }
        public string Opn { get; set; }
        public string ProgramNo { get; set; }
        public string Qualifier { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class StartEndTimes
    {
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
    }

    public class EventStartEndTimes
    {
        public List<StartEndTimes> aprFeedRateStartEndTimes { get; set; }
        public List<StartEndTimes> rufFeedRateStartEndTimes { get; set; }
        public List<StartEndTimes> semiFinFeedRateStartEndTimes { get; set; }
        public List<StartEndTimes> finFeedRateStartEndTimes { get; set; }
        public List<StartEndTimes> dreFeedRateStartEndTimes { get; set; }
        public List<StartEndTimes> sparkOutStartEndTimes { get; set; }
    }
}
