﻿using SANDEN_PL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SANDEN_DL;


namespace SANDEN_BL
{
    public class BL_USER_MASTER
    {
        public DataTable BL_ExecuteTask(PL_USER_MASTER objPl)
        {
            DL_USER_MASTER objDl = new DL_USER_MASTER();
            return objDl.DL_ExecuteTask(objPl);
        }


    }
}
