using SANDEN_PL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SANDEN_DL;


namespace SANDEN_BL
{
    public class BL_STATION_MASTER
    {
        public DataTable BL_ExecuteTask(PL_STATION_MASTER objPl)
        {
            DL_STATION_MASTER objDl = new DL_STATION_MASTER();
            return objDl.DL_ExecuteTask(objPl);
        }


    }
}
