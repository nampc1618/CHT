using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Kis.Toolkit;
using CHT.Commons;
using System.Xml;

namespace CHT.Command.Cmd
{
    public class SaveParamsCmd : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        XmlManagement xmlManagement;
        public SaveParamsCmd(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
            xmlManagement = new XmlManagement();
        }
        public override void Execute(object parameter)
        {
            string para = parameter as string;
            if (para != null)
            {
                switch (para)
                {
                    case "1":
                        xmlManagement.Load(XmlPath.WeighXmlPath);
                        XmlNode nodePortName = xmlManagement.SelectSingleNode("//PortName");
                        XmlNode nodeBaud = xmlManagement.SelectSingleNode("//BaudRate");
                        XmlNode nodeDelay = xmlManagement.SelectSingleNode("//Delay");
                        xmlManagement.SetNodeValueFromNode(nodePortName, WeighViewModel.Instance.Rs232.PortSelected);
                        xmlManagement.SetNodeValueFromNode(nodeBaud, WeighViewModel.Instance.Rs232.BaudRateSelected);
                        xmlManagement.SetNodeValueFromNode(nodeDelay, WeighViewModel.Instance.Rs232.WeightModel.Delay);
                        xmlManagement.SetAttributeValueFromXPath("//Weight", "//UnitId", WeighViewModel.Instance.Rs232.WeightModel.UnitId);
                        if(xmlManagement.Save(XmlPath.WeighXmlPath))
                        {
                            MessageBox.Show("Đã lưu thông số cân!");
                        }
                        break;
                    case "2":
                        xmlManagement.Load(XmlPath.PrinterXmlPath);
                        XmlNode nodeIp = xmlManagement.SelectSingleNode("//Ip");
                        XmlNode nodePort = xmlManagement.SelectSingleNode("//Port");
                        XmlNode nodeField = xmlManagement.SelectSingleNode("//Field");
                        xmlManagement.SetNodeValueFromNode(nodeIp, PrinterViewModel.Instance.PrinterModel.IP);
                        xmlManagement.SetNodeValueFromNode(nodePort, PrinterViewModel.Instance.PrinterModel.Port);
                        xmlManagement.SetNodeValueFromNode(nodeField, PrinterViewModel.Instance.PrinterModel.FieldName);
                        if (xmlManagement.Save(XmlPath.PrinterXmlPath))
                        {
                            MessageBox.Show("Đã lưu thông số máy in!");
                        }
                        break;
                }
            }
        }
    }
}
