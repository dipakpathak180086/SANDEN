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
    public class BL_PART_MASTER
    {
        public DataTable BL_ExecuteTask(PL_PART_MASTER objPl)
        {
            DL_PART_MASTER objDl = new DL_PART_MASTER();
            return objDl.DL_ExecuteTask(objPl);
        }


    }
}
