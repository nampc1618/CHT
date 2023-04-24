using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CHT.Commons
{
    public class CommonPaths
    {
        public static string WeighXmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Weight.xml");
        public static string CHTConfigurationXmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CHTConfiguration.xml");
        public static string PrinterXmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Printer.xml");
        public static string ScriptlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\script.ps1");

    }
}
