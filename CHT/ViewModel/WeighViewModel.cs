using CHT.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CHT.ViewModel
{
    public class WeighViewModel : ViewModelBase
    {
        private static Dispatcher _dispatcher;
        public static WeighViewModel Instance { get; private set; }
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Rs232 Rs232 { get; private set; }
        public WeighViewModel(Dispatcher dispatcher, Rs232 rs232) 
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else return;

            _dispatcher= dispatcher;
            Rs232 = rs232;
        }

    }
}
