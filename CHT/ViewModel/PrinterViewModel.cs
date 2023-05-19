using CHT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Npc.NNetSocket;
using System.Windows;
using static CHT.Commons.SysStates;

namespace CHT.ViewModel
{
    public class PrinterViewModel : ViewModelBase
    {

        private Dispatcher _dispatcher;
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
            _dispatcher = dispatcher;
            PrinterModel = new PrinterModel();
            Logger.Info("Create a PrinterModel object is success.");

            PrinterModel.NClientSocket = new NClientSocket(PrinterModel.IP, Convert.ToInt32(PrinterModel.Port));
            PrinterModel.NClientSocket.ConnectionEventCallback += NClientSocket_ConnectionEventCallback;
            PrinterModel.NClientSocket.ClientErrorEventCallback += NClientSocket_ClientErrorEventCallback;
            PrinterModel.NClientSocket.ClientConnect();
            Logger.Info("Create a NClientSocket object is success.");
        }

        private void NClientSocket_ClientErrorEventCallback(string errorMsg)
        {
            MessageBox.Show(errorMsg);
        }
        private void NClientSocket_ConnectionEventCallback(NClientSocket.EConnectionEventClient e, object obj)
        {
            this._dispatcher.BeginInvoke(new Action(async () =>
            {
                await ConnectionEventCallbackAsync(e, obj);
            }));
        }
        private async Task ConnectionEventCallbackAsync(NClientSocket.EConnectionEventClient e, object obj)
        {

            switch (e)
            {
                case NClientSocket.EConnectionEventClient.RECEIVEDATA:
                    await Task.Factory.StartNew(() =>
                    {
                        string sReceive = PrinterModel.NClientSocket.ReceiveString;

                        if (!string.IsNullOrEmpty(sReceive))
                        {
                            if (sReceive.Length >= Convert.ToInt32(PrinterModel.LengthCounter)
                            && !sReceive.Contains("$") && !sReceive.Contains("!"))
                            {
                                if (!int.TryParse(sReceive.Substring(PrinterModel.StartIndex, PrinterModel.EndIndex), out int counter))
                                {
                                    return;
                                }

                                //int counter = i;
                                //Logger.InfoFormat("Counter: {0}", counter);
                                if (PrinterModel.CounterPrinter < counter)
                                {
                                    try
                                    {
                                        string data = "";
                                        if (WeighViewModel.Instance.Rs232.QueueFields.Count == 0)
                                        {
                                            return;
                                        }
                                        data = WeighViewModel.Instance.Rs232.QueueFields.Peek().ToString();
                                        WeighViewModel.Instance.Rs232.QueueFields.Dequeue();
                                        PrinterModel.NClientSocket.SendMsg(PrinterModel.UpdateFieldCode(data));
                                        Logger.InfoFormat("Weight data send to field is: {0}", data);

                                        PrinterModel.CounterPrinter = counter;
                                        PrinterModel.MessageState = Commons.SysStates.EMessageState.UPDATE_FIELD_SUCCESS;
                                        Logger.Info("Updated the field is success.");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }
                                else
                                {
                                    PrinterModel.MessageState = Commons.SysStates.EMessageState.NORMAL;
                                    return;
                                }
                            }

                        }
                    });
                    break;
                case NClientSocket.EConnectionEventClient.CLIENTCONNECTED:
                    PrinterModel.PrinterState = Commons.SysStates.EPrinterState.CONNECTED;
                    PrinterModel.MessageState = Commons.SysStates.EMessageState.NORMAL;
                    PrinterModel.Start(Convert.ToInt32(PrinterModel.CircleRequest));
                    Logger.Info("Connect to the printer is success.");
                    break;
                case NClientSocket.EConnectionEventClient.CLIENTDISCONNECTED:
                    PrinterModel.PrinterState = Commons.SysStates.EPrinterState.DISCONECTED;
                    PrinterModel.MessageState = Commons.SysStates.EMessageState.NORMAL;
                    PrinterModel.Stop();
                    Logger.Info("Disconnect to the printer.");
                    break;
                default:
                    break;

            }
        }
        //private async Task<int> ReadDataFromPrinterAsync()
        //{
        //    try
        //    {
        //        string sReceive = PrinterModel.NClientSocket.ReceiveString;
        //        if (!string.IsNullOrEmpty(sReceive))
        //        {
        //            if (sReceive.Length >= Convert.ToInt32(PrinterModel.LengthCounter)
        //                    && !sReceive.Contains("$") && !sReceive.Contains("!"))
        //            {
        //                int i = 0;
        //                int.TryParse(sReceive.Substring(PrinterModel.StartIndex, PrinterModel.EndIndex), out i);
        //                return await Task.FromResult(i);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.CompletedTask;
        //    }
        //}
    }
}
