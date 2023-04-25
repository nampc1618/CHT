using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHT.Commons;
using Kis.Toolkit;

namespace CHT.Model
{
    public class PrinterModel : BaseModel
    {

        XmlManagement xmlManagement;
        public PrinterModel()
        {
            xmlManagement = new XmlManagement();
            xmlManagement.Load(CommonPaths.PrinterXmlPath);

            _ip = xmlManagement.SelectSingleNode("//Ip").InnerText;
            _port = xmlManagement.SelectSingleNode("//Port").InnerText;
            _fieldName = xmlManagement.SelectSingleNode("//Field").InnerText;
            _requestCode = xmlManagement.GetAttributeValueFromXPath("//Printer//Commands//Command[@id='3']", "code");
            _updateFieldCode = xmlManagement.GetAttributeValueFromXPath("//Printer//Commands//Command[@id='4']", "code");
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
    }
}
