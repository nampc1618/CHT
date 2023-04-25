using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CHT.Command.Cmd
{
    public class RefreshCmd : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        public RefreshCmd(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object parameter)
        {
            MessageBox.Show("RefreshCmd");
        }
    }
}
