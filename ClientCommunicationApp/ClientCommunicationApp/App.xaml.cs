using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ClientCommunicationApp
{
    
    public partial class App : Application
    {
        public bool DoHandle { get; set; }
        private void Application_DispatcherUnhandledException(object sender,
                               System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (this.DoHandle)
            {
                //Handling the exception within the UnhandledException handler.
                MessageBox.Show(e.Exception.Message, "Exception Caught",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            else
            {
                //If you do not set e.Handled to true, the application will close due to crash.
                MessageBox.Show("Application is going to close! "+e.Exception.Message, "Uncaught Exception");
                MessageBox.Show("Application is going to close! " + e.Exception.ToString(), "Uncaught Exception");
                e.Handled = true;
            }
        }
        // give the mutex a  unique name
        private const string MutexName = "CNGClientCommunicationApp";

        // declare the mutex
        private readonly Mutex _mutex;

        bool createdNew;

        // overload the constructor
        public App()
        {
            _mutex = new Mutex(true, MutexName, out createdNew);

            if (!createdNew)
            {
                // if the mutex already exists, notify and quit
                MessageBox.Show("Comm Client already running", "ClientCommunicationApp");
                Application.Current.Shutdown(0);
            }
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                if (!createdNew) return;


            }
            catch (Exception ex) { MessageBox.Show("App startup error:"+ex.ToString()); }
        }
    }
}

