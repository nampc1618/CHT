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
        private NClientSocket NClientSocket;
        private static Dispatcher _dispatcher;
        public static PrinterViewModel Instance { get; private set; }
        public PrinterModel PrinterModel { get; private set; }
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

            NClientSocket= new NClientSocket(PrinterModel.IP, Convert.ToInt32(PrinterModel.Port));
            NClientSocket.ClientConnect();
            NClientSocket.ConnectionEventCallback += NClientSocket_ConnectionEventCallback;
            NClientSocket.ClientErrorEventCallback += NClientSocket_ClientErrorEventCallback;
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
                    break;
                case NClientSocket.EConnectionEventClient.CLIENTCONNECTED:
                    break;
                case NClientSocket.EConnectionEventClient.CLIENTDISCONNECTED:
                    break;
                default:
                    break;
            }
        }
    }
}
