using SatoLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommunicationApp.Common
{
    enum EnumStation { Station0, Station1, Station2, Station3, Station4, Station5, Station6, Station7, Station8, Station9, Station10, Station11 }
    enum EnumBtnName { btnConnectStation0, btnConnectStation1, btnConnectStation2, btnConnectStation3, btnConnectStation4, btnConnectStation5, btnConnectStation6, btnConnectStation7, btnConnectStation8, btnConnectStation9, btnConnectStation10, btnConnectStation11 }
    enum EnumBtnConnect { Connect_0, Connect_1, Connect_2, Connect_3, Connect_4, Connect_5, Connect_6, Connect_7, Connect_8, Connect_9, Connect_10, Connect_11 }
    enum EnumBtnDisConnect { DisConnect_0, DisConnect_1, DisConnect_2, DisConnect_3, DisConnect_4, DisConnect_5, DisConnect_6, DisConnect_7, DisConnect_8, DisConnect_9, DisConnect_10, DisConnect_11 }
    public class GlobalVar
    {
        //Commented by dipak 27_aug_20 for encyption
        //public static string mMainSqlConString = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString; 
        public static string mMainSqlConString =  ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        public static string mMacAddress = GlobalVar.Decrypt_data(ConfigurationManager.ConnectionStrings["Lic_Info"].ConnectionString);
        public static string mLasserMachineFilePath = ConfigurationManager.ConnectionStrings["LasserFilePath"].ConnectionString;
        public static SatoLogger Logger = new SatoLogger();
        /// <summary>
        /// its used for encript the given string 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt_data(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            str = new string(arr);
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(str));
        }
        /// <summary>
        /// its used for decrypt the given string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt_data(string str)
        {
            char[] arr = Encoding.Unicode.GetString(Convert.FromBase64String(str)).ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //log.Debug(
                //    "Found MAC Address: " + nic.GetPhysicalAddress() +
                //    " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    // log.Debug("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }
        public static void SetLoggerSetting()
        {
            Logger.ChangeInterval = SatoLogger.ChangeIntervals.ciHourly;
            Logger.EnableLogFiles = true;
            Logger.LogDays = 5;
            Logger.LogFilesExt = "log";
            Logger.LogFilesPrefix = "Log_";
            Logger.LogFilesPath = Directory.GetCurrentDirectory();
            Logger.StartLogging();
        }
    }

    public class LabelMessage : INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get { return this._message; }
            set
            {
                if (this._message != value)
                {
                    this._message = value;
                    this.NotifyPropertyChanged("Message");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
