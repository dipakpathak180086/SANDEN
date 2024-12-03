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

    public class DL_PART_MASTER
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_PART_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[15];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@PART_NO", SqlDbType.VarChar, 50);
                param[1].Value = obj.Part_No;
                param[2] = new SqlParameter("@PART_DIS", SqlDbType.VarChar, 50);
                param[2].Value = obj.Description;
                param[3] = new SqlParameter("@PACK_SIZE", SqlDbType.Int, 50);
                param[3].Value = obj.PackSize;
                param[4] = new SqlParameter("@MODEL_NO", SqlDbType.VarChar, 50);
                param[4].Value = obj.Model_No;
                param[5] = new SqlParameter("@MODEL_CHAR", SqlDbType.VarChar, 50);
                param[5].Value = obj.Model_Char;
                param[6] = new SqlParameter("@BOP_NO", SqlDbType.VarChar, 50);
                param[6].Value = obj.BOP_No;
                param[7] = new SqlParameter("@CUST_PART_NO", SqlDbType.VarChar, 50);
                param[7].Value = obj.Cust_Part_No;
                param[8] = new SqlParameter("@ACTIVE", SqlDbType.VarChar, 50);
                param[8].Value = obj.Is_Active;
                param[9] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[9].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_PART_MASTER]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion      

    }
}
