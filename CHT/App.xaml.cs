using CHT.Commons;
using CHT.View;
using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CHT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainView mainView = new MainView();
           
            Rs232 rs232 = new Rs232(mainView.Dispatcher);
            WeighViewModel weighViewModel = new WeighViewModel(mainView.Dispatcher, rs232);
            PrinterViewModel printerViewModel = new PrinterViewModel(mainView.Dispatcher);
            SettingsViewModel settingsViewModel = new SettingsViewModel(mainView.Dispatcher, weighViewModel, printerViewModel);

            MainViewModel mainViewModel = new MainViewModel(mainView.Dispatcher, mainView, weighViewModel, settingsViewModel, printerViewModel);
            mainView.DataContext = mainViewModel;

            mainView.Show();
        }
    }
}
