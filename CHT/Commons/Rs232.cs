﻿#define NO_TEST

using CHT.Model;
using CHT.ViewModel;
using Kis.Toolkit;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CHT.Commons
{
    public class Rs232 : BaseModel
    {
        private readonly Dispatcher _dispatcher;
        public WeightModel WeightModel { get; private set; }
        
        XmlManagement xmlManagement;
        public Rs232(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _portList = SerialPort.GetPortNames().ToList();
            _baudRateList = new List<string>()
            {
                "4800",
                "9600",
                "19200",
                "38400"
            };
            WeightModel = new WeightModel();

            xmlManagement = new XmlManagement();
            xmlManagement.Load(XmlPath.WeighXmlPath);
            PortSelected = xmlManagement.SelectSingleNode("//PortName").InnerText;
            BaudRateSelected = xmlManagement.SelectSingleNode("//BaudRate").InnerText;
            Init();
#if !NO_TEST
            _portList = SerialPort.GetPortNames().ToList();
            PortSelected = _portList[2];
            _baudRateSelected = 9600;

            Init();
#endif
        }
        private SerialPort _rs232COM;
        public SerialPort Rs232COM
        {
            set
            {
                _rs232COM = value;
            }
            get
            {
                return _rs232COM;
            }
        }

        private List<string> _portList;
        public List<string> PortList
        {
            get { return _portList; }
            set
            {
                SetProperty(ref _portList, value);
            }
        }
        private List<string> _baudRateList;
        public List<string> BaudRateList
        {
            get { return _baudRateList; }
            set
            {
                SetProperty(ref _baudRateList, value);
            }
        }
        private string _portSelected;
        public string PortSelected
        {
            get
            {
                return _portSelected;
            }
            set
            {
                SetProperty(ref _portSelected, value);
            }
        }
        private string _baudRateSelected;
        public string BaudRateSelected
        {
            get { return _baudRateSelected; }
            set
            {
                SetProperty(ref _baudRateSelected, value);
            }
        }
        private string _recieveData;
        public string RecieveData
        {
            get { return _recieveData; }
            set
            {
                _recieveData = value;
            }
        }
        private string dataForShow;
        public string DataForShow
        {
            get { return dataForShow; }
            set
            {
                SetProperty(ref dataForShow, value);
            }
        }
        //private List<int> _baudRateList;
        //public List<int> BaudRateList
        //{
        //    get { return _baudRateList; }
        //    set
        //    {
        //        SetProperty(ref _baudRateList, value);
        //    }
        //}
        private void Init()
        {
            // Create a object COM
            _rs232COM = new SerialPort();
            _rs232COM.PortName = PortSelected;
            _rs232COM.ReadTimeout = 5000;
            _rs232COM.BaudRate = Convert.ToInt32(BaudRateSelected);
            _rs232COM.DataBits = 8;
            _rs232COM.Parity = Parity.None;
            _rs232COM.StopBits = StopBits.One;
            _rs232COM.DataReceived += new SerialDataReceivedEventHandler(_rs232COM_DataReceived);
        }
        private async void _rs232COM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            await this._dispatcher.BeginInvoke(new Action(async () =>
            {
                await DataReceived(sender, e);
            }));
        }
        private async Task DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            await Task.Factory.StartNew(async () =>
            {
                // Code
                RecieveData = _rs232COM.ReadExisting();
                string s = RecieveData.Substring(RecieveData.IndexOf("0"), 5);
                float f = 0.000f;
                if (!float.TryParse(s, out f))
                    return;
                if (f == 1.000f)
                {
                    f = 0.000f;
                }
                DataForShow = (f * 1000).ToString("0.");
                MainViewModel.Instance.ShowData(DataForShow);
                await Task.Delay(1000);
            });
        }

        //private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    this._dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        RecieveData = _rs232COM.ReadLine();
        //        string s = RecieveData.Substring(RecieveData.IndexOf("0"), 5);
        //        float f = 0.0f;
        //        float.TryParse(s, out f);
        //        if(f == 1.0f)
        //        {
        //            f = 0.0f;
        //        }
        //        DataForShow = (f * 1000).ToString("0.000");
        //        MainViewModel.Instance.ShowData(DataForShow);
        //        Thread.Sleep(3000);
        //    }));
        //}
        public bool OpenCOM()
        {
            try
            {
                _rs232COM.Open();
                return _rs232COM.IsOpen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool CloseCOM()
        {
            try
            {
                _rs232COM.Close();
                return _rs232COM.IsOpen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
