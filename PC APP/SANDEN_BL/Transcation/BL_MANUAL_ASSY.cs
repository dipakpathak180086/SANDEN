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
    public class BL_MANUAL_ASSY
    {
        public DataTable BL_ExecuteTask(PL_MANUAL_ASSY objPl)
        {
            DL_MANUAL_ASSY objDl = new DL_MANUAL_ASSY();
            return objDl.DL_ExecuteTask(objPl);
        }


    }
}
