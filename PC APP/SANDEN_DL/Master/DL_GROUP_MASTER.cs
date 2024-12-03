using SatoLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SANDEN_PL;
using SANDEN_COMMON;

namespace SANDEN_DL
{
    public class DL_GROUP_MASTER
    {
        SqlHelper _SqlHelper = new SqlHelper();
        SqlTransaction _SqlTransaction = null;
        SqlConnection _SqlConnection = null;
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_GROUP_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            _SqlConnection = new SqlConnection(GlobalVariable.mMainSqlConString);
            _SqlConnection.Open();
            _SqlTransaction = _SqlConnection.BeginTransaction();
          
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@Type", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@GroupName", SqlDbType.VarChar, 50);
                param[1].Value = obj.GroupName;
                param[2] = new SqlParameter("@ModuleId", SqlDbType.VarChar, 50);
                param[2].Value = obj.ModuleId;
                param[3] = new SqlParameter("@Add", SqlDbType.Bit, 50);
                param[3].Value = obj.Add;
                param[4] = new SqlParameter("@Update", SqlDbType.Bit, 50);
                param[4].Value = obj.Update;
                param[5] = new SqlParameter("@Delete", SqlDbType.Bit, 50);
                param[5].Value = obj.Delete;
                param[6] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
                param[6].Value = obj.CreatedBy;
                DataTable dataTable= _SqlHelper.ExecuteDataset(_SqlTransaction, "[PRC_GroupMaster]", param).Tables[0];
                _SqlTransaction.Commit();
                _SqlTransaction = null;
                _SqlConnection.Close();
                _SqlConnection.Dispose();
                _SqlConnection = null;
                return dataTable;
            }
            catch (Exception ex)
            {
                if (_SqlTransaction != null)
                {
                    _SqlTransaction.Rollback();
                    _SqlTransaction = null;
                }
                throw ex;
            }
        }
        public DataSet DL_ExecuteTaskAsDataset(PL_GROUP_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            _SqlConnection = new SqlConnection(GlobalVariable.mMainSqlConString);
            _SqlConnection.Open();
            _SqlTransaction = _SqlConnection.BeginTransaction();
          
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@Type", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@GroupName", SqlDbType.VarChar, 50);
                param[1].Value = obj.GroupName;
                param[2] = new SqlParameter("@ModuleId", SqlDbType.VarChar, 50);
                param[2].Value = obj.ModuleId;
                param[3] = new SqlParameter("@Add", SqlDbType.Bit, 50);
                param[3].Value = obj.Add;
                param[4] = new SqlParameter("@Update", SqlDbType.Bit, 50);
                param[4].Value = obj.Update;
                param[5] = new SqlParameter("@Delete", SqlDbType.Bit, 50);
                param[5].Value = obj.Delete;
                param[6] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
                param[6].Value = obj.CreatedBy;
                DataSet dataSet= _SqlHelper.ExecuteDataset(_SqlTransaction, "[PRC_GroupMaster]", param);
                _SqlTransaction.Commit();
                _SqlTransaction = null;
                _SqlConnection.Close();
                _SqlConnection.Dispose();
                _SqlConnection = null;
                return dataSet;
            }
            catch (Exception ex)
            {
                if (_SqlTransaction != null)
                {
                    _SqlTransaction.Rollback();
                    _SqlTransaction = null;
                }
                throw ex;
            }
        }
        #endregion
    }
}
