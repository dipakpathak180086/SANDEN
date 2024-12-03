using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommunicationApp.Model
{
   public  class PL_ASSEY_TRANSCATION
    {
        public string DbType { get; set; }
        public string StationNo { get; set; }
        public string ParentBarcode { get; set; }
        public string ChildBarcode { get; set; }
        public string TestingStatus { get; set; }
        public string MachineParam { get; set; }
        public string Is_Manual { get; set; }
        public string FG_Barcode { get; set; }
        public string CreatedBy { get; set; }
    }
}
