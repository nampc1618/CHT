#define NO_TEST

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
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            xmlManagement.Load(CommonPaths.WeighXmlPath);
            PortSelected = xmlManagement.SelectSingleNode("//PortName").InnerText.Trim();
            BaudRateSelected = xmlManagement.SelectSingleNode("//BaudRate").InnerText.Trim();
            CircleReceiveData = Convert.ToInt32((xmlManagement.SelectSingleNode("//CircleReceiveData").InnerText.Trim()));
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
                if (SetProperty(ref _portSelected, value))
                {

                }
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
        private SysStates.EComState _comState = SysStates.EComState.NONE;
        public SysStates.EComState ComState
        {
            get { return _comState; }
            set
            {
                SetProperty(ref _comState, value);
            }
        }

        public int CircleReceiveData { get; set; }

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
            this.OpenCOM();
        }
        private async void _rs232COM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            await this._dispatcher.BeginInvoke(new Action(async () =>
            {
                _comState = SysStates.EComState.RECEIVING_DATA;
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
                ComState = SysStates.EComState.RECEIVING_DATA;
                if (f == 1.000f || f == 0.000f)
                {
                    f = 0.000f;
                    return;
                }
                if (WeightModel.UnitId.Equals("g"))
                {
                    DataForShow = (f * 1000).ToString("0.");
                }
                else if (WeightModel.UnitId.Equals("kg"))
                {
                    DataForShow = f.ToString();
                }
                MainViewModel.Instance.ShowData(DataForShow);
                await Task.Delay(CircleReceiveData);
            });
        }
        public bool OpenCOM()
        {
            try
            {
                _rs232COM.Open();
                if (_rs232COM.IsOpen)
                {
                    ComState = SysStates.EComState.OPENED;
                    Logger.InfoFormat("Opened {0} port", PortSelected);
                }
                else
                {
                    ComState = SysStates.EComState.CLOSED;
                    Logger.InfoFormat("Closed {0} port", PortSelected);
                }
                return _rs232COM.IsOpen;
            }
            catch (Exception ex)
            {
                Logger.FatalFormat("Occur a exception with {0} port", PortSelected);
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool CloseCOM()
        {
            try
            {
                _rs232COM.Close();
                if (_rs232COM.IsOpen)
                {
                    ComState = SysStates.EComState.OPENED;
                }
                else
                {
                    ComState = SysStates.EComState.CLOSED;
                    Logger.InfoFormat("Closed {0} port", PortSelected);
                }
                return _rs232COM.IsOpen;
            }
            catch (Exception ex)
            {
                Logger.FatalFormat("Occur a exception with {0} port", PortSelected);
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
