using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ClientCommunicationApp.Common;
using SatoLib;

class clsPLC : IDisposable
{
    string _IP = "";
    int _Port = 0;
    TcpClient client = default(TcpClient);
    NetworkStream stream = null;
    public clsPLC(string IP, int Port)
    {
        _IP = IP;
        _Port = Port;
        Connect();
    }

    private bool Connect()
    {
        bool flag = false;
        try
        {

            client = new TcpClient();
            client.ReceiveTimeout = 10000;
            if (CheckPinging())
            {
                flag = true;
                client.Connect(IPAddress.Parse(_IP), _Port);
                stream = client.GetStream();
            }
            else
            {
                flag = false;
            }
        }
        catch (Exception ex)
        {
            GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-Connect()", $"Error:{ex.ToString()}");
        }
        return flag;
    }

    private bool CheckPinging()
    {
        try
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(_IP, 30);
            return reply.Status == IPStatus.Success;
        }
        catch (Exception ex)
        {
            GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-CheckPinging()", $"Error:{ex.ToString()}");
            return false;
        }
    }

    internal string GetPLCStatus()
    {
        try
        {
            if (client.Connected)
            {
                return "Connected";
            }
            else
            {
                return "Not Connected";
            }
        }
        catch (Exception ex)
        {
            GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-GetPLCStatus()", $"Error:{ex.ToString()}");
            return "Error";
        }
    }

    public async Task<string> GetPLCInput()
    {
        string strData = "";
        await Task.Run(() =>
        {
            try
            {
                byte[] data = new byte[1024];
                int size = stream.Read(data, 0, data.Length);
                if (size == 0)
                {
                    throw new Exception("Re-Connecting...");
                }
                strData = Encoding.ASCII.GetString(data);
            }
            catch (Exception ex)
            {
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-GetPLCInput()", $"Error:{ex.ToString()}");
                try
                {
                    client.Close();
                    Connect();
                }
                catch (NullReferenceException e)
                {
                    GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-GetPLCInput()-NullReferenceException", $"Error:{e.ToString()}");
                }
            }
        });

        return strData.Replace("\0", "");
    }
    public void WriteToPLC(string ErrorCode)
    {

        if (ErrorCode == "")
            return;
        try
        {
            byte[] data = Encoding.ASCII.GetBytes(ErrorCode);
            stream.Write(data, 0, data.Length);

        }
        catch (Exception ex)
        {
            GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-GetPLCInput()", $"Error:{ex.ToString()}");
            try
            {
                client.Close();
                Connect();
            }
            catch (NullReferenceException e)
            {
                GlobalVar.Logger.LogMessage(EventNotice.EventTypes.evtInfo, $"clsPCL-GetPLCInput()-NullReferenceException", $"Error:{e.ToString()}");
            }
        }
    }



    public void Dispose()
    {
        if (client.Connected)
        {
            stream.Close();
            client.Close();
        }
        stream = null;
        client = null;
        _IP = "";
        _Port = 0;
    }
}

