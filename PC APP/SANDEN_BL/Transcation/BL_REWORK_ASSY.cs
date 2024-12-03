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
    public class BL_REWORK_ASSY
    {
        public DataTable BL_ExecuteTask(PL_REWORK_ASSY objPl)
        {
            DL_REWORK_ASSY objDl = new DL_REWORK_ASSY();
            return objDl.DL_ExecuteTask(objPl);
        }


    }
}
