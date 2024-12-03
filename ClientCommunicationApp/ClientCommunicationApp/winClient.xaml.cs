using ClientCommunicationApp.Common;
using ClientCommunicationApp.Model;
using SatoLib;
using SatoLib.Communication;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ClientCommunicationApp
{
    public partial class winClient : Window
    {
        private List<PlcIpPortInfo> _listPlcInfo = new List<PlcIpPortInfo>();
        private LabelMessage ClsMessage = new LabelMessage();
        private DL_PLC_ASSEY_TRANSCATION dlObj = new DL_PLC_ASSEY_TRANSCATION();
        /* Plc class object for all the plc statiion-starting with 0 */
        clsPLC_Update _plc0 = null, _plc1 = null, _plc2 = null, _plc3 = null, _plc4 = null, _plc5 = null, _plc6 = null, _plc7 = null, _plc8 = null, _plc9 = null, _plc10 = null, _plc11 = null;
        /* bool variable to check whether plc job is done or not-so that same job can not be started in another thread */
        bool _IsPlc0Complete = true, _IsPlc1Complete = true, _IsPlc2Complete = true, _IsPlc3Complete = true, _IsPlc4Complete = true, _IsPlc5Complete = true, _IsPlc6Complete = true, _IsPlc7Complete = true, _IsPlc8Complete = true, _IsPlc9Complete = true, _IsPlc10Complete = true, _IsPlc11Complete = true;
        /* Timer to check plc job after every 1 second */
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        bool IsWaitingForPLC = false; /* if timer is already waiting for previous  plc call */
        /* Dictionary to maintain station name, rather than hard coded value,dic will be used to fetch station value*/
        Dictionary<EnumStation, string> _dicStation = new Dictionary<EnumStation, string>();
        public winClient()
        {
            InitializeComponent();
            /* Enable Logging */
            GlobalVar.SetLoggerSetting();
        }
        public static System.Windows.Controls.ContentPresenter FindVisualChild(DependencyObject obj, int position) //where childItem : DependencyObject
        {
            #region FindVisualChild Code
            int occurence = 0;

            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                //it gets the control based on childindex.
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is System.Windows.Controls.ContentPresenter)
                {
                    occurence = occurence + 1;
                    //it checks the position of the control and returns ContentPresenter.
                    if (occurence == position)
                        return (System.Windows.Controls.ContentPresenter)child;
                }
                else
                {
                    //it keep on validates until controls r done.
                    System.Windows.Controls.ContentPresenter childOfChild = FindVisualChild(child, position);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
            #endregion
        }
        #region Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (GlobalVar.mMacAddress != GlobalVar.GetMacAddress())
                //{
                //    MessageBox.Show("Invalid License Contact to SATO Engineer!!!!");
                //    //GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, "Window_Loaded", "File LIC: "+ GlobalVar.mMacAddress+"  System LIC:"+GlobalVar.GetMacAddress());
                //    return;
                //}
                this.Title += "- App Verson : " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _listPlcInfo = new Model.PlcIpPortInfo().GetAllPlcIP();
                dg.ItemsSource = _listPlcInfo;
                LabelStackPanel.DataContext = ClsMessage;
                SetOnLoadData();
               
                /* start timer */
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Start();
                BtnConnect_Click(btnConnectStation0, null);
                BtnConnect_Click(btnConnectStation1, null);
                BtnConnect_Click(btnConnectStation2, null);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, "Window_Loaded", ex.ToString()); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                dispatcherTimer.Stop();
                if (_plc0 != null)
                    _plc0.Dispose();
                if (_plc1 != null)
                    _plc1.Dispose();
                if (_plc2 != null)
                    _plc2.Dispose();
                if (_plc3 != null)
                    _plc3.Dispose();
                if (_plc4 != null)
                    _plc4.Dispose();
                if (_plc5 != null)
                    _plc5.Dispose();
                if (_plc6 != null)
                    _plc6.Dispose();
                if (_plc7 != null)
                    _plc7.Dispose();
                if (_plc8 != null)
                    _plc8.Dispose();
                if (_plc9 != null)
                    _plc9.Dispose();
                if (_plc10 != null)
                    _plc10.Dispose();
                if (_plc11 != null)
                    _plc11.Dispose();
                dispatcherTimer = null;
                GC.Collect();
            }
            catch (Exception ex) { GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, "Window_Closing", ex.ToString()); }
        }
        #endregion

        #region Button Events

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            try
            {
                ClsMessage.Message = "";
                btn.IsEnabled = false;
                /* station0 button */
                if (btn.Name == EnumBtnName.btnConnectStation0.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station0]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_0.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_0;
                            /* dispose previous object if having any reference*/
                            if (_plc0 != null)
                            {
                                _plc0.Dispose();
                                _plc0 = null;
                            }
                            _plc0 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc0.Dispose();
                            _plc0 = null;
                            btn.Content = EnumBtnConnect.Connect_0;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station0]}";
                }
                /* station1 button */
                else if (btn.Name == EnumBtnName.btnConnectStation1.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station1]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_1.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_1;
                            /* dispose previous object if having any reference*/
                            if (_plc1 != null)
                            {
                                _plc1.Dispose();
                                _plc1 = null;
                            }
                            _plc1 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc1.Dispose();
                            _plc1 = null;
                            btn.Content = EnumBtnConnect.Connect_1;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station1]}";
                }
                /* station2 button */
                else if (btn.Name == EnumBtnName.btnConnectStation2.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station2]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_2.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_2;
                            /* dispose previous object if having any reference*/
                            if (_plc2 != null)
                            {
                                _plc2.Dispose();
                                _plc2 = null;
                            }
                            _plc2 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc2.Dispose();
                            _plc2 = null;
                            btn.Content = EnumBtnConnect.Connect_2;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station2]}";
                }
                /* station3 button */
                else if (btn.Name == EnumBtnName.btnConnectStation3.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station3]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_3.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_3;
                            /* dispose previous object if having any reference*/
                            if (_plc3 != null)
                            {
                                _plc3.Dispose();
                                _plc3 = null;
                            }
                            _plc3 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc3.Dispose();
                            _plc3 = null;
                            btn.Content = EnumBtnConnect.Connect_3;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station3]}";
                }
                /* station4 button */
                else if (btn.Name == EnumBtnName.btnConnectStation4.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station4]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_4.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_4;
                            /* dispose previous object if having any reference*/
                            if (_plc4 != null)
                            {
                                _plc4.Dispose();
                                _plc4 = null;
                            }
                            _plc4 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc4.Dispose();
                            _plc4 = null;
                            btn.Content = EnumBtnConnect.Connect_4;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station4]}";
                }
                /* station5 button */
                else if (btn.Name == EnumBtnName.btnConnectStation5.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station5]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_5.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_5;
                            /* dispose previous object if having any reference*/
                            if (_plc5 != null)
                            {
                                _plc5.Dispose();
                                _plc5 = null;
                            }
                            _plc5 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc5.Dispose();
                            _plc5 = null;
                            btn.Content = EnumBtnConnect.Connect_5;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station5]}";
                }
                /* station6 button */
                else if (btn.Name == EnumBtnName.btnConnectStation6.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station6]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_6.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_6;
                            /* dispose previous object if having any reference*/
                            if (_plc6 != null)
                            {
                                _plc6.Dispose();
                                _plc6 = null;
                            }
                            _plc6 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc6.Dispose();
                            _plc6 = null;
                            btn.Content = EnumBtnConnect.Connect_6;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station6]}";
                }
                /* station7 button */
                else if (btn.Name == EnumBtnName.btnConnectStation7.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station7]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_7.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_7;
                            /* dispose previous object if having any reference*/
                            if (_plc7 != null)
                            {
                                _plc7.Dispose();
                                _plc7 = null;
                            }
                            _plc7 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc7.Dispose();
                            _plc7 = null;
                            btn.Content = EnumBtnConnect.Connect_7;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station7]}";
                }
                /* station8 button */
                else if (btn.Name == EnumBtnName.btnConnectStation8.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station8]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_8.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_8;
                            /* dispose previous object if having any reference*/
                            if (_plc8 != null)
                            {
                                _plc8.Dispose();
                                _plc8 = null;
                            }
                            _plc8 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc8.Dispose();
                            _plc8 = null;
                            btn.Content = EnumBtnConnect.Connect_8;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station8]}";
                }
                /* station9 button */
                else if (btn.Name == EnumBtnName.btnConnectStation9.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station9]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_9.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_9;
                            /* dispose previous object if having any reference*/
                            if (_plc9 != null)
                            {
                                _plc9.Dispose();
                                _plc9 = null;
                            }
                            _plc9 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc9.Dispose();
                            _plc9 = null;
                            btn.Content = EnumBtnConnect.Connect_9;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station9]}";
                }
                /* station10 button */
                else if (btn.Name == EnumBtnName.btnConnectStation10.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station10]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_10.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_10;
                            /* dispose previous object if having any reference*/
                            if (_plc10 != null)
                            {
                                _plc10.Dispose();
                                _plc10 = null;
                            }
                            _plc10 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc10.Dispose();
                            _plc10 = null;
                            btn.Content = EnumBtnConnect.Connect_10;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station10]}";
                }
                /* station11 button */
                else if (btn.Name == EnumBtnName.btnConnectStation11.ToString())
                {
                    var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station11]);
                    if (plc != null)
                    {
                        /* If plc is disconnected then content will be Connect, after connection it will be disconnect */
                        if (btn.Content.ToString() == EnumBtnConnect.Connect_11.ToString())
                        {
                            btn.Content = EnumBtnDisConnect.DisConnect_11;
                            /* dispose previous object if having any reference*/
                            if (_plc11 != null)
                            {
                                _plc11.Dispose();
                                _plc11 = null;
                            }
                            _plc11 = new clsPLC_Update(plc.IPAdress, plc.PortNo);
                            plc.Status = "Connection trying";
                        }
                        else // If disconnecting
                        {
                            _plc11.Dispose();
                            _plc11 = null;
                            btn.Content = EnumBtnConnect.Connect_11;
                            plc.Status = "Connection close";
                        }
                    }
                    else
                        ClsMessage.Message = $"PlC info not found for station {_dicStation[EnumStation.Station11]}";
                }
            }
            catch (Exception ex) { GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, "BtnConnect_Click", ex.ToString()); ClsMessage.Message = $"Error(BtnConnect_Click): {ex.Message}"; }
            finally { btn.IsEnabled = true; }
        }

        #endregion

        #region Dispatcher Timer
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //ClsMessage.Message = "";
                if (!IsWaitingForPLC)
                {
                    IsWaitingForPLC = true;
                    /* for plc0 */
                    if (btnConnectStation0.Content.ToString() == EnumBtnDisConnect.DisConnect_0.ToString() && _IsPlc0Complete == true)
                        Task.Run(() => GetPLCInput0());
                    /* for plc1 */
                    if (btnConnectStation1.Content.ToString() == EnumBtnDisConnect.DisConnect_1.ToString() && _IsPlc1Complete == true)
                        Task.Run(() => GetPLCInput1());
                    /* for plc2 */
                    if (btnConnectStation2.Content.ToString() == EnumBtnDisConnect.DisConnect_2.ToString() && _IsPlc2Complete == true)
                        Task.Run(() => GetPLCInput2());
                   
                    


                    IsWaitingForPLC = false;
                }
            }
            catch (Exception ex) { ClsMessage.Message = "Error(DispatcherTimer_Tick):" + ex.Message; ; GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, "DispatcherTimer_Tick", ex.ToString()); IsWaitingForPLC = false; }
        }
        #endregion

        #region PLC Communication

        /* communicate to plc0 */
        private void GetPLCInput0()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station0]);
            try
            {
                plc.Status = _plc0.GetPLCStatus();
                if (_IsPlc0Complete)
                {
                    _IsPlc0Complete = false;
                    string data = _plc0.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput0:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput0:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc0.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput0:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc0Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput0:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc0Complete = true;
            }
        }
        /* communicate to plc1 */
        private void GetPLCInput1()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station1]);
            try
            {
                plc.Status = _plc1.GetPLCStatus();
                if (_IsPlc1Complete)
                {
                    _IsPlc1Complete = false;
                    string data = _plc1.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput1:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput1:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc1.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput1:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc1Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput1:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc1Complete = true;
            }
        }
        /* communicate to plc2 */
        private void GetPLCInput2()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station2]);
            try
            {
                plc.Status = _plc2.GetPLCStatus();
                if (_IsPlc2Complete)
                {
                    _IsPlc2Complete = false;
                    string data = _plc2.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput2:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        // string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput2:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc2.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput2:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc2Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput2:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc2Complete = true;
            }
        }
        /* communicate to plc3 */
        private void GetPLCInput3()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station3]);
            try
            {
                plc.Status = _plc3.GetPLCStatus();
                if (_IsPlc3Complete)
                {
                    _IsPlc3Complete = false;
                    string data = _plc3.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput3:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        // string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput3:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc3.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput3:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc3Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput3:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc3Complete = true;
            }
        }
        /* communicate to plc4 */
        private void GetPLCInput4()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station4]);
            try
            {
                plc.Status = _plc4.GetPLCStatus();
                if (_IsPlc4Complete)
                {
                    _IsPlc4Complete = false;
                    string data = _plc4.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput4:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput4:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc4.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput4:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc4Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput4:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc4Complete = true;
            }
        }
        /* communicate to plc5 */
        private void GetPLCInput5()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station5]);
            try
            {
                plc.Status = _plc5.GetPLCStatus();
                if (_IsPlc5Complete)
                {
                    _IsPlc5Complete = false;
                    string data = _plc5.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput5:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput5:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc5.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput5:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc5Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput5:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc5Complete = true;
            }
        }
        /* communicate to plc6 */
        private void GetPLCInput6()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station6]);
            try
            {
                plc.Status = _plc6.GetPLCStatus();
                if (_IsPlc6Complete)
                {
                    _IsPlc6Complete = false;
                    string data = _plc6.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput6:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput6:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc6.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput6:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc6Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput6:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc6Complete = true;
            }
        }
        /* communicate to plc7 */
        private void GetPLCInput7()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station7]);
            try
            {
                plc.Status = _plc7.GetPLCStatus();
                if (_IsPlc7Complete)
                {
                    _IsPlc7Complete = false;
                    string data = _plc7.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput7:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput7:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc7.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput7:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc7Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput7:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc7Complete = true;
            }
        }
        /* communicate to plc8 */
        private void GetPLCInput8()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station8]);
            try
            {
                plc.Status = _plc8.GetPLCStatus();
                if (_IsPlc8Complete)
                {
                    _IsPlc8Complete = false;
                    string data = _plc8.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput8:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        //string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput8:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc8.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput8:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc8Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput8:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc8Complete = true;
            }
        }
        /* communicate to plc9 */
        private void GetPLCInput9()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station9]);
            try
            {
                plc.Status = _plc9.GetPLCStatus();
                if (_IsPlc9Complete)
                {
                    _IsPlc9Complete = false;
                    string data = _plc9.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput9:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        // string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput9:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc9.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput9:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc9Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput9:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc9Complete = true;
            }
        }
        /* communicate to plc10 */
        private void GetPLCInput10()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station10]);
            try
            {
                plc.Status = _plc10.GetPLCStatus();
                if (_IsPlc10Complete)
                {
                    _IsPlc10Complete = false;
                    string data = _plc10.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput10:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        // string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput10:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc10.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput10:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc10Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput10:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc10Complete = true;
            }
        }
        /* communicate to plc11 */
        private void GetPLCInput11()
        {
            var plc = _listPlcInfo.Find(x => x.StationNo == _dicStation[EnumStation.Station11]);
            try
            {
                plc.Status = _plc11.GetPLCStatus();
                if (_IsPlc11Complete)
                {
                    _IsPlc11Complete = false;
                    string data = _plc11.GetPLCInput();
                    if (data != "")
                    {
                        plc.ReceiveMessage = data;
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput11:{plc.IPAdress}:{plc.PortNo}", $"Received data::{data}");

                        string response = dlObj.SendToServer(data.Replace("\0", "").Replace("!", "").TrimEnd(), $"{plc.IPAdress}:{plc.PortNo}");
                        // string response = $"Thanks {plc.IPAdress}:{plc.PortNo} for data-{data}";

                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput11:{plc.IPAdress}:{plc.PortNo}", $"Prepare Response data::{response}");
                        _plc11.WriteToPLC(response);
                        GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"GetPLCInput11:{plc.IPAdress}:{plc.PortNo}", "Data sent successfully!!");
                        plc.SendMessage = response;
                    }
                    _IsPlc11Complete = true;
                }
            }
            catch (Exception ex)
            {
                plc.Status = "Error";
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtError, $"GetPLCInput11:{plc.IPAdress}:{plc.PortNo}", $"Error:" + ex.ToString());
                _IsPlc11Complete = true;
            }
        }

        #endregion

        #region Methods

        private void SetOnLoadData()
        {
            try
            {
                /* set station value */
                _dicStation.Add(EnumStation.Station0, "00");
                _dicStation.Add(EnumStation.Station1, "01");
                _dicStation.Add(EnumStation.Station2, "02");
             
                /* Set button content */
                btnConnectStation0.Content = EnumBtnConnect.Connect_0.ToString();
                btnConnectStation1.Content = EnumBtnConnect.Connect_1.ToString();
                btnConnectStation2.Content = EnumBtnConnect.Connect_2.ToString();
               
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
