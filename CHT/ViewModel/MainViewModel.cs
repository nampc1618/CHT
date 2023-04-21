using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHT.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static MainViewModel _instance = null;
        public static MainViewModel GetInstance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MainViewModel();
                }
                return _instance;
            }
        }
        public MainViewModel() 
        { 
        }
        ~MainViewModel() { }
    }
}
