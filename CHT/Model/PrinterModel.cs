﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CHT.Commons;
using Kis.Toolkit;
using log4net.Repository.Hierarchy;
using Npc.NNetSocket;

namespace CHT.Model
{
    public class PrinterModel : BaseModel
    {

        XmlManagement xmlManagement;
        public NClientSocket NClientSocket;
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public PrinterModel()
        {
            xmlManagement = new XmlManagement();
            xmlManagement.Load(CommonPaths.PrinterXmlPath);

            _ip = xmlManagement.SelectSingleNode("//Ip").InnerText.Trim();
            _port = xmlManagement.SelectSingleNode("//Port").InnerText.Trim();
            _fieldName = xmlManagement.SelectSingleNode("//Field").InnerText.Trim();
            LengthCounter = xmlManagement.SelectSingleNode("//LenghtCounter").InnerText.Trim();
            StartIndex = Convert.ToInt32(xmlManagement.GetAttributeValueFromXPath("//Printer//SubStringSplit", "startIndex").Trim());
            EndIndex = Convert.ToInt32(xmlManagement.GetAttributeValueFromXPath("//Printer//SubStringSplit", "endIndex").Trim());
            CircleRequest = xmlManagement.SelectSingleNode("//CircleRequest").InnerText.Trim();
            _requestCode = xmlManagement.GetAttributeValueFromXPath("//Printer//Commands//Command[@id='3']", "code").Trim();
            _updateFieldCode = xmlManagement.GetAttributeValueFromXPath("//Printer//Commands//Command[@id='4']", "code").Trim();

        }
        private string _ip;
        public string IP
        {
            get { return _ip; }
            set
            {
                SetProperty(ref _ip, value);
            }
        }
        private string _port;
        public string Port
        {
            get { return _port; }
            set
            {
                SetProperty(ref _port, value);
            }
        }
        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                SetProperty(ref _fieldName, value);
            }
        }
        private string _requestStatusCmd;
        public string RequestStatusCmd
        {
            get { return _requestStatusCmd; }
            set
            {
                SetProperty(ref _requestStatusCmd, value);
            }
        }
        private string _updateFieldCmd;
        public string UpdateFieldCmd
        {
            get { return _updateFieldCmd; }
            set
            {
                SetProperty(ref _updateFieldCmd, value);
            }
        }

        private int _checkHeartbeat;
        public int CheckHeartbeat
        {
            get { return _checkHeartbeat; }
            set
            {
                SetProperty(ref _checkHeartbeat, value);
            }
        }
        private SysStates.EPrinterState _printerState = SysStates.EPrinterState.NONE;
        public SysStates.EPrinterState PrinterState
        {
            get { return _printerState; }
            set
            {
                SetProperty(ref _printerState, value);
            }
        }
        private SysStates.EMessageState _messageState = SysStates.EMessageState.NORMAL;
        public SysStates.EMessageState MessageState
        {
            get { return _messageState; }
            set
            {
                SetProperty(ref _messageState, value);
            }
        }
        private string _requestCode;
        private string _updateFieldCode;
        public string LengthCounter { get; set; }
        public string CircleRequest { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        private int _counterPrinter = 0;
        public int CounterPrinter
        {
            get { return _counterPrinter; }
            set
            {
                SetProperty(ref _counterPrinter, value);
            }
        }


        #region Cmds for control printer
        public string GetRequestCode()
        {
            return (char)0x2 + _requestCode + (char)0x3;
        }
        public string UpdateFieldCode(string data)
        {
            // Data send to: \u0002UTenTienich\n82\u0003
            return (char)0x2 + _updateFieldCode + _fieldName + (char)0xA + data + (char)0x3;
        }
        #endregion
        #region Method

        private CancellationTokenSource _cancellationTokenSource;
        private Task _taskRun;
        public void Start(int delay)
        {
            if (_taskRun != null && !_taskRun.IsCompleted)
                return;
            SemaphoreSlim initializationSemaphore = new SemaphoreSlim(0, 1);
            _cancellationTokenSource = new CancellationTokenSource();
            _taskRun = Task.Run(async () =>
            {
                try
                {
                    while (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        if (this.NClientSocket != null)
                        {
                            this.NClientSocket.SendMsg(GetRequestCode());
                        }
                        if (initializationSemaphore != null)
                            initializationSemaphore.Release();
                        await Task.Delay(delay);
                    }
                }
                finally
                {
                    if (initializationSemaphore != null)
                        initializationSemaphore.Release();
                }
            }, _cancellationTokenSource.Token);

            //await initializationSemaphore.WaitAsync();
            initializationSemaphore.Dispose();
            initializationSemaphore = null;

            if (_taskRun.IsFaulted)
            {
                //await _taskRun;
            }
        }
        public void Stop()
        {
            try
            {
                if (_cancellationTokenSource == null) return;
                if (_cancellationTokenSource.IsCancellationRequested)
                    return;
                if (!_taskRun.IsCompleted)
                {
                    _cancellationTokenSource.Cancel();
                    //await _taskRun;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
