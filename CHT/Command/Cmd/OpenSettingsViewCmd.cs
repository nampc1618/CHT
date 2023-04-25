using CHT.View;
using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHT.Command.Cmd
{
    public class OpenSettingsViewCmd : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public OpenSettingsViewCmd(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object parameter)
        {
            if (SettingsViewModel.Instance.SettingsView == null)
            {
                SettingsView settingsView = new SettingsView();
                settingsView.DataContext = _mainViewModel.SettingsViewModel;
                SettingsViewModel.Instance.SettingsView = settingsView;
                SettingsViewModel.Instance.SettingsView.ShowDialog();
                Logger.Info("Open a SettingsView");
            }
            else
            {
                SettingsViewModel.Instance.SettingsView.Close();
            }
        }
    }
}
