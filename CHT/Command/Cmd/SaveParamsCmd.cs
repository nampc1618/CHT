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
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                        xmlManagement.Load(CommonPaths.WeighXmlPath);
                        XmlNode nodePortName = xmlManagement.SelectSingleNode("//PortName");
                        XmlNode nodeBaud = xmlManagement.SelectSingleNode("//BaudRate");
                        XmlNode nodeDelay = xmlManagement.SelectSingleNode("//Delay");
                        XmlNode nodeCircleReceiveData = xmlManagement.SelectSingleNode("//CircleReceiveData");
                        xmlManagement.SetNodeValueFromNode(nodePortName, WeighViewModel.Instance.Rs232.PortSelected);
                        xmlManagement.SetNodeValueFromNode(nodeBaud, WeighViewModel.Instance.Rs232.BaudRateSelected);
                        xmlManagement.SetNodeValueFromNode(nodeDelay, WeighViewModel.Instance.Rs232.WeightModel.Delay);
                        xmlManagement.SetNodeValueFromNode(nodeCircleReceiveData, WeighViewModel.Instance.Rs232.CircleReceiveData.ToString());
                        xmlManagement.SetAttributeValueFromXPath("//Weight", "UnitId", WeighViewModel.Instance.Rs232.WeightModel.UnitId);
                        if(xmlManagement.Save(CommonPaths.WeighXmlPath))
                        {
                            Logger.Info("Save the para for Weight success.");
                            MessageBox.Show("Đã lưu thông số cân!");
                        }
                        SettingsViewModel.Instance.SettingsView.Close();
                        SettingsViewModel.Instance.SettingsView = null;
                        Logger.Info("Run the script for restart program. (after when save the para weight)");
                        MainViewModel.Instance.RunScript(CommonPaths.ScriptlPath);
                        break;
                    case "2":
                        xmlManagement.Load(CommonPaths.PrinterXmlPath);
                        XmlNode nodeIp = xmlManagement.SelectSingleNode("//Ip");
                        XmlNode nodePort = xmlManagement.SelectSingleNode("//Port");
                        XmlNode nodeField = xmlManagement.SelectSingleNode("//Field");
                        xmlManagement.SetNodeValueFromNode(nodeIp, PrinterViewModel.Instance.PrinterModel.IP);
                        xmlManagement.SetNodeValueFromNode(nodePort, PrinterViewModel.Instance.PrinterModel.Port);
                        xmlManagement.SetNodeValueFromNode(nodeField, PrinterViewModel.Instance.PrinterModel.FieldName);
                        if (xmlManagement.Save(CommonPaths.PrinterXmlPath))
                        {
                            Logger.Info("Save the para for Printer success.");
                            MessageBox.Show("Đã lưu thông số máy in!");
                        }
                        SettingsViewModel.Instance.SettingsView.Close();
                        SettingsViewModel.Instance.SettingsView = null;
                        Logger.Info("Run the script for restart program. (after when save the para printer)");
                        MainViewModel.Instance.RunScript(CommonPaths.ScriptlPath);
                        break;
                }
            }
        }
    }
}
