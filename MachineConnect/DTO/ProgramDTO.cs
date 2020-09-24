using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ProgramDTO
    {
        public bool Isselected { get; set; }
        public string ProgramNo { get; set; }
        public int ProgramLenght { get; set; }
        public string Comment { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsSupportFolder { get; set; }
    }
}
