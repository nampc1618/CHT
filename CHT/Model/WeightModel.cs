﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHT.Commons;
using CHT.View;
using Kis.Toolkit;

namespace CHT.Model
{
    public class WeightModel : BaseModel
    {
        XmlManagement xmlManagement;
        public log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public WeightModel()
        {
            _unitIdList = new List<string>() { "g", "kg" };
            xmlManagement = new XmlManagement();
            xmlManagement.Load(CommonPaths.WeighXmlPath);
            _unitId = xmlManagement.GetAttributeValueFromXPath("//Weight", "UnitId").Trim();
            _delay = xmlManagement.SelectSingleNode("//Delay").InnerText.Trim();
            Logger.Info("Create WeightModel success.");
        }
        private string _unitId;
        public string UnitId

        {
            get { return _unitId; }
            set
            {
                SetProperty(ref _unitId, value);
            }
        }
        private List<string> _unitIdList;
        public List<string> UnitIdList
        {
            get { return _unitIdList; }
            set
            {
                SetProperty(ref _unitIdList, value);
            }
        }
        private string _delay;
        public string Delay

        {
            get { return _delay; }
            set
            {
                SetProperty(ref _delay, value);
            }
        }
        private bool _triggerWeight;
        public bool TriggerWeight
        {
            get { return _triggerWeight; }
            set
            {
                if(SetProperty(ref _triggerWeight, value))
                {
                    if (_triggerWeight == true)
                    {
                        CountdownView countdownView = new CountdownView();
                        countdownView.ShowDialog();
                    }
                }
            }
        }
    }
}
