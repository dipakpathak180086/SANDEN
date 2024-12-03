using SANDEN_COMMON;
using SANDEN_PL;
using SatoLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANDEN_DL
{

    public class DL_MANUAL_ASSY
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_MANUAL_ASSY obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@STATION_NO", SqlDbType.VarChar, 100);
                param[1].Value = obj.Station_No;
                param[2] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 100);
                param[2].Value = obj.Scan_Barcode;
                param[3] = new SqlParameter("@TESTING_STATUS", SqlDbType.VarChar, 100);
                param[3].Value = obj.MachineStatus;

                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_ASSEMBLY_SCANNING]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion      

    }
}
