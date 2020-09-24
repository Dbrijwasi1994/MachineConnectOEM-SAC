
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
  
        public class MachineInfo
        {
            public string MachineID { get; set; }
            public string PortNo { get; set; }
            public string IPAddress { get; set; }
            public string Interfaceid { get; set; }
            public string Description { get; set; }
            public string PartsCountLocation { get; set; }
            public bool PartsCountMacroEnabled { get; set; }
            public bool TpmTrakDataEnabled { get; set; }
            public bool DAPEnabled { get; set; }
            public bool EthernetEnabled { get; set; }
        }

        public class PlantInfo
        {
            public string PlantID { get; set; }
            public string Description { get; set; }
            public string PlantCode { get; set; }
            public string SLno { get; set; }
        }
   
}
