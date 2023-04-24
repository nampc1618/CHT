using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHT.Command.Cmd
{
    public class OpenCOMCmd : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        public OpenCOMCmd(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object parameter)
        {
            _mainViewModel.WeighViewModel.Rs232.OpenCOM();
        }
    }
}
