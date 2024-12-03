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

    public class DL_REWORK_ASSY
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_REWORK_ASSY obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                
                param[0] = new SqlParameter("@STATION_NO", SqlDbType.VarChar, 100);
                param[0].Value = obj.Station_No;
                param[1] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 100);
                param[1].Value = obj.Scan_Barcode;

                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_REWORK]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion      

    }
}
