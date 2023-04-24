using CHT.Command.Cmd;
using CHT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace CHT.ViewModel
{
   
    public class SettingsViewModel : ViewModelBase
    {
        private static Dispatcher _dispatcher;
        public static SettingsViewModel Instance { get; private set; }
        public SettingsView SettingsView { get; set; }
        public WeighViewModel WeighViewModel { get; private set; }
        public PrinterViewModel PrinterViewModel { get; private set; }
        public SettingsViewModel(Dispatcher dispatcher, WeighViewModel weighViewModel, PrinterViewModel printerViewModel)
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else return;

            _dispatcher = dispatcher;
            WeighViewModel = weighViewModel;
            PrinterViewModel = printerViewModel;

            this.SaveParams = new SaveParamsCmd(this);
        }

        public ICommand SaveParams { get; }
    }
}
