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
    }
}
