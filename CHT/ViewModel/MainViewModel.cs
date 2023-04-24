﻿using CHT.Command.Cmd;
using CHT.Commons;
using CHT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Kis.Toolkit;
using System.Windows.Controls;
using System.Windows;

namespace CHT.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static Dispatcher _dispatcher;
        public MainView MainView { get; private set; }
        //private static MainViewModel _instance = null;
        //public static MainViewModel GetInstance
        //{
        //    get
        //    {
        //        if(_instance == null)
        //        {
        //            _instance = new MainViewModel(_dispatcher);
        //        }
        //        return _instance;
        //    }
        //}
        public static MainViewModel Instance { get; private set; }
        
        #region ViewModels
        public WeighViewModel WeighViewModel { get; private set; }
        public PrinterViewModel PrinterViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        #endregion
        public MainViewModel(Dispatcher dispatcher, MainView mainView, WeighViewModel weighViewModel, SettingsViewModel settingsViewModel,
                            PrinterViewModel printerViewModel) 
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
                return;
            _dispatcher = dispatcher;
            MainView = mainView;
            WeighViewModel = weighViewModel;
            SettingsViewModel = settingsViewModel;
            PrinterViewModel = printerViewModel;
         
            this.OpenCOM = new OpenCOMCmd(this);
            this.PressEnterKey = new PressEnterKeyCmd(this);
            this.OpenSettingsView = new OpenSettingsViewCmd(this);
        }
        public void WirteTest(Paragraph para, string text)
        {
            para.Dispatcher.BeginInvoke(new Action(() =>
            {
                para.Inlines.Add(text);
            }));
        }
        public void ShowData(string data)
        {
            MainView.tbWeigh.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainView.tbWeigh.Text = data;
            }));
        }
        public void ShowView(Window wd)
        {
            wd.ShowDialog();
        }
       
        private double _opacityMain = 1.0;
        public double OpacityMain
        {
            get { return _opacityMain; }
            set
            {
                SetProperty(ref _opacityMain, value);
            }
        }
        public ICommand Refesh { get; }
        public ICommand OpenCOM { get; }
        public ICommand CloseCOM { get; }
        public ICommand PressEnterKey { get; }
        public ICommand OpenSettingsView { get; }
    }
}
