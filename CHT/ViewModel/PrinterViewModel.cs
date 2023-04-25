using CHT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Npc.NNetSocket;
using System.Windows;

namespace CHT.ViewModel
{
    public class PrinterViewModel : ViewModelBase
    {
        public NClientSocket NClientSocket;
        private static Dispatcher _dispatcher;
        public static PrinterViewModel Instance { get; private set; }
        public PrinterModel PrinterModel { get; private set; }
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public PrinterViewModel(Dispatcher dispatcher)
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
                return;
            _dispatcher= dispatcher;
            PrinterModel = new PrinterModel();
            Logger.Info("Create a PrinterModel object is success.");

            NClientSocket = new NClientSocket(PrinterModel.IP, Convert.ToInt32(PrinterModel.Port));
            NClientSocket.ConnectionEventCallback += NClientSocket_ConnectionEventCallback;
            NClientSocket.ClientErrorEventCallback += NClientSocket_ClientErrorEventCallback;
            NClientSocket.ClientConnect();
            Logger.Info("Create a NClientSocket object is success.");
        }

        private void NClientSocket_ClientErrorEventCallback(string errorMsg)
        {
            MessageBox.Show(errorMsg);
        }

        private void NClientSocket_ConnectionEventCallback(NClientSocket.EConnectionEventClient e, object obj)
        {
            switch (e)
            {
                case NClientSocket.EConnectionEventClient.RECEIVEDATA:
                    string s = NClientSocket.ReceiveString;
                    if (!string.IsNullOrEmpty(s))
                    {
                        PrinterModel.MessageState = Commons.SysStates.EMessageState.UPDATE_FIELD_SUCCESS;
                        Logger.Info("Updated the field is success.");
                    }
                    break;
                case NClientSocket.EConnectionEventClient.CLIENTCONNECTED:
                    PrinterModel.PrinterState = Commons.SysStates.EPrinterState.CONNECTED;
                    PrinterModel.MessageState = Commons.SysStates.EMessageState.NORMAL;
                    Logger.Info("Connect to the printer is success.");
                    break;
                case NClientSocket.EConnectionEventClient.CLIENTDISCONNECTED:
                    PrinterModel.PrinterState = Commons.SysStates.EPrinterState.DISCONECTED;
                    PrinterModel.MessageState = Commons.SysStates.EMessageState.NORMAL;
                    Logger.Info("Disconnect to the printer.");
                    break;
                default:
                    break;
            }
        }
    }
}
