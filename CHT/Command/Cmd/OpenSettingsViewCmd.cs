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
        public OpenSettingsViewCmd(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object parameter)
        {
            SettingsView settingsView = new SettingsView();
            settingsView.DataContext = _mainViewModel.SettingsViewModel;
            SettingsViewModel.Instance.SettingsView = settingsView;
            SettingsViewModel.Instance.SettingsView.ShowDialog();
        }
    }
}
