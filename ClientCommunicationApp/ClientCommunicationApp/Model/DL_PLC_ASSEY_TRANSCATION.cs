using ClientCommunicationApp.Common;
using SatoLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ClientCommunicationApp.Model
{
    public class DL_PLC_ASSEY_TRANSCATION
    {
        public string GetLasserCode()
        {
            string sLasserCode = string.Empty;
            try
            {
                if (!Directory.Exists(GlobalVar.mLasserMachineFilePath + "\\BCK"))
                {
                    Directory.CreateDirectory(GlobalVar.mLasserMachineFilePath + "\\BCK");
                }
                string[] fileInfo = Directory.GetFiles(GlobalVar.mLasserMachineFilePath);
                List<string> lst = fileInfo.ToList();
                if (lst.Count > 0)
                {
                    foreach (string item in lst)
                    {
                        string sFileName = Path.GetFileName(item);
                        string[] strArr = File.ReadAllText(item.ToString()).Split(';');
                        sLasserCode = "";
                        sLasserCode = strArr[1].ToString().Split('\r')[0].Replace("\r\n", "").Trim();
                        if (!sLasserCode.StartsWith("U155"))
                        {
                            sLasserCode = "Invalid Laser Barcode Code!!!";
                            break;
                        }
                        
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"Read Lasser Code:", $"Received data from Lasser File ::{sLasserCode}");
                        File.Copy(item.ToString(), GlobalVar.mLasserMachineFilePath + "\\BCK\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + sLasserCode.Replace(":", "_") + ".txt");
                        File.Delete(item.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                sLasserCode = "";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetLasserCode:", $"Received data for Exception ::{ex.Message}");
            }
            return sLasserCode;
        }
        public string SendToServer(string receiveString, string IP)
        {
            string sReturnToServer = string.Empty;
            PL_ASSEY_TRANSCATION plObj = new PL_ASSEY_TRANSCATION();
            try
            {
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"SendToServer:{IP}", $"Received data for db::{receiveString}");
                if (receiveString.Trim().EndsWith("^"))
                {
                    string removeDataDelimeter = receiveString.TrimEnd('^');
                    string[] arrData = removeDataDelimeter.Split('~');
                    plObj.DbType = arrData[0];
                    plObj.StationNo = arrData[1];

                    plObj.CreatedBy = IP;
                    DataTable dataTable = null;
                    if (plObj.DbType == "P1")
                    {
                        plObj.StationNo = arrData[1];
                        plObj.ParentBarcode = arrData[2];
                    }
                    else if (plObj.DbType == "P2")
                    {
                        plObj.StationNo = arrData[1];
                        plObj.ParentBarcode = arrData[2];
                        plObj.MachineParam = arrData[3];
                        plObj.TestingStatus = arrData[4];

                    }
                    dataTable = DL_ExecuteTask(plObj);




                    if (dataTable.Rows.Count > 0)
                    {
                        sReturnToServer = dataTable.Rows[0][0].ToString();
                    }
                    else
                    {
                        sReturnToServer = "02"  + "~No Data Found!!";
                    }
                }
                else
                {
                    sReturnToServer = "02"  + "~Request data is not correct!!";
                }
            }
            catch (Exception ex)
            {

                sReturnToServer = "02~"  + ex.Message;
            }
            return sReturnToServer;
        }

        private SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_ASSEY_TRANSCATION obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@STATION_NO", SqlDbType.VarChar, 100);
                param[1].Value = obj.StationNo;
                param[2] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 100);
                param[2].Value = obj.ParentBarcode;
                param[3] = new SqlParameter("@TESTING_STATUS", SqlDbType.VarChar, 100);
                param[3].Value = obj.TestingStatus;
                param[4] = new SqlParameter("@MACHINE_PARAM", SqlDbType.VarChar, 3000);
                param[4].Value = obj.MachineParam;
                param[5] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100);
                param[5].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(GlobalVar.mMainSqlConString, CommandType.StoredProcedure, "[PRC_ASSEMBLY_SCANNING]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DL_ExecuteTask_For_Bottom(PL_ASSEY_TRANSCATION obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@STATION_NO", SqlDbType.VarChar, 100);
                param[1].Value = obj.StationNo;
                param[2] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 100);
                param[2].Value = obj.ParentBarcode;
                param[3] = new SqlParameter("@TESTING_STATUS", SqlDbType.VarChar, 100);
                param[3].Value = obj.TestingStatus;
                param[4] = new SqlParameter("@MACHINE_PARAM", SqlDbType.VarChar, 3000);
                param[4].Value = obj.MachineParam;
                param[5] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100);
                param[5].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(GlobalVar.mMainSqlConString, CommandType.StoredProcedure, "[PRC_CNG_ASSEMBLY_SCANNING_P2]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable DL_ExecuteTask_For_FG_CNG(PL_ASSEY_TRANSCATION obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@FG_BARCODE", SqlDbType.VarChar, 100);
                param[1].Value = obj.FG_Barcode;
                param[2] = new SqlParameter("@IS_MANUAL", SqlDbType.VarChar, 50);
                param[2].Value = obj.Is_Manual;
                param[3] = new SqlParameter("@TESTING_STATUS", SqlDbType.VarChar, 100);
                param[3].Value = obj.TestingStatus;
                param[4] = new SqlParameter("@MACHINE_PARAM", SqlDbType.VarChar, 3000);
                param[4].Value = obj.MachineParam;
                param[5] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[5].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(GlobalVar.mMainSqlConString, CommandType.StoredProcedure, "[PRC_FG_LABEL_PRINTING]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
