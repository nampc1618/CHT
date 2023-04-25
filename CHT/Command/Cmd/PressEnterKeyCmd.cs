using CHT.View;
using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHT.Command.Cmd
{
    public class PressEnterKeyCmd : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        public PressEnterKeyCmd(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
        public override void Execute(object parameter)
        {
            _mainViewModel.OpacityMain = 0.5;
            _mainViewModel.WeighViewModel.Rs232.OpenCOM();
            CountdownView countdownView = new CountdownView();
            countdownView.ShowDialog();
            PrinterViewModel.Instance.PrinterModel.MessageState = Commons.SysStates.EMessageState.NORMAL;
        }
    }
}
