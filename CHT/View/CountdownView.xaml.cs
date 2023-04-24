using CHT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CHT.View
{
    /// <summary>
    /// Interaction logic for CountdownView.xaml
    /// </summary>
    public partial class CountdownView : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public CountdownView()
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(Convert.ToInt32(MainViewModel.Instance.WeighViewModel.Rs232.WeightModel.Delay));

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("ss").Split('0')[1];
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    MainViewModel.Instance.OpacityMain = 1.0;
                    MainViewModel.Instance.WeighViewModel.Rs232.CloseCOM();
                    this.Close();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.ActualWidth - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.ActualHeight - this.ActualHeight) / 2;
        }
    }
}
