using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class MachineSetting
    {
        public ushort SpeedLocationStart { get; set; }
        public ushort SpeedLocationEnd { get; set; }
        public ushort LoadLocationStart { get; set; }
        public ushort LoadLocationEnd { get; set; }
        public ushort CoolantOilLocationStart { get; set; }
        public ushort CoolantOilLocationEnd { get; set; }
        public short LocationTargetStart { get; set; }
        public short LocationTargetEnd { get; set; }
        public short LocationActualStart { get; set; }
        public short LocationActualEnd { get; set; }

        public short LocationTargetStartSubSpindle { get; set; }
        public short LocationTargetEndSubSpindle { get; set; }
        public short LocationActualStartSubSpindle { get; set; }
        public short LocationActualEndSubSpindle { get; set; }

        public bool IsAlarmHistoryEnabled { get; set; }
        public bool IsOperationHistoryEnabled { get; set; }
        public bool IsExternalOperatorHistoryEnabled { get; set; }
        public bool IsSpindleLoadSpeedEnabled { get; set; }
        public bool IsToolLifeEnabled { get; set; }
        public bool IsCoolentLubOilLevelEnabled { get; set; }
        public short ComponentLocation { get; set; }
        public short OperationLocation { get; set; }
    }
}

