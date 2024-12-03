using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SANDEN_COMMON;
using SANDEN_PL;

using SatoLib;


namespace SANDEN_DL
{
    public class DL_STATION_MASTER
    {

        
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_STATION_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[5];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@STATION_NO", SqlDbType.VarChar, 50);
                param[1].Value = obj.StationNo;
                param[2] = new SqlParameter("@STATION_NAME", SqlDbType.VarChar, 50);
                param[2].Value = obj.SationName;
                param[3] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[3].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_STATION_MASTER]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion      
    }
}
