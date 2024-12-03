using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SANDEN_PL;
using SANDEN_DL;
using System.Data;

namespace SANDEN_BL
{
  public  class BL_GROUP_MASTER
    {
        public DataTable BL_ExecuteTask(PL_GROUP_MASTER objPl)
        {
            DL_GROUP_MASTER objDl = new DL_GROUP_MASTER();
            return objDl.DL_ExecuteTask(objPl);
        }
        public DataSet BL_ExecuteTaskAsDataset(PL_GROUP_MASTER objPl)
        {
            DL_GROUP_MASTER objDl = new DL_GROUP_MASTER();
            return objDl.DL_ExecuteTaskAsDataset(objPl);
        }
    }
}
