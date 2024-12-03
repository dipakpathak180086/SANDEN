using ClientCommunicationApp.Common;
using SatoLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommunicationApp.Model
{
    public class PlcIpPortInfo : INotifyPropertyChanged
    {
        SqlHelper _SqlHelper = new SqlHelper();
        public string IPAdress { get; set; }
        public string StationNo { get; set; }
        public int PortNo { get; set; }
        public string IPAdressPortString { get; set; }
        private string _reciveMessage;
        public string ReceiveMessage
        {
            get { return this._reciveMessage; }
            set
            {
                if (this._reciveMessage != value)
                {
                    this._reciveMessage = value;
                    this.NotifyPropertyChanged("ReceiveMessage");
                }
            }
        }
        private string _sendMessage;
        public string SendMessage
        {
            get { return this._sendMessage; }
            set
            {
                if (this._sendMessage != value)
                {
                    this._sendMessage = value;
                    this.NotifyPropertyChanged("SendMessage");
                }
            }
        }
        private string _status;
        public string Status
        {
            get { return this._status; }
            set
            {
                if (this._status != value)
                {
                    this._status = value;
                    this.NotifyPropertyChanged("Status");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        internal List<PlcIpPortInfo> GetAllPlcIP()
        {
            List<PlcIpPortInfo> listInfo = new List<PlcIpPortInfo>();
            _SqlHelper = new SqlHelper();
            try
            {
                DataTable dt = _SqlHelper.ExecuteDataset(GlobalVar.mMainSqlConString, CommandType.StoredProcedure, "[PRC_GetPlcInfo]").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string IPAddress = dt.Rows[i]["PLC_IP_Adress"].ToString();
                    int PortNo = Convert.ToInt32(dt.Rows[i]["PortNo"]);
                    string StationNo = dt.Rows[i]["Station_No"].ToString();
                    listInfo.Add(new PlcIpPortInfo { IPAdress = IPAddress, PortNo = PortNo, IPAdressPortString = $"{IPAddress}:{PortNo}", StationNo = StationNo });
                }
                return listInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
