using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SANDEN_COMMON;

namespace SANDEN_PL
{
    public class PL_PART_MASTER : Common
    {
        public string Part_No { get; set; }
        public string Description { get; set; }
        public int? PackSize { get; set; }
        public string PartType { get; set; }

        public string Model_No { get; set; }
        public string Model_Char { get; set; }
        public string BOP_No { get; set; }
        public string Cust_Part_No { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Sub_Parent { get; set; }
        public bool Is_Sub_Child { get; set; }

    }
}
