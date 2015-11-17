﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PatientMonitor
{
    class PatientMonitoringController
    {
        readonly MainWindow _mainWindow = null;
        readonly IPatientFactory _patientFactory = null;
        DispatcherTimer _tickTimer = new DispatcherTimer();
        PatientDataReader _dataReader;
        PatientDataReader _dataReader1;
        PatientData _patientData;
        PatientData _patientData1;
        List<PatientData> _listPatientData;
        List<PatientDataReader> _listPatientDataReader;
        List<String> _ListDataFileNames;
        List<Label> _ListPulseRates;
        List<Label> _ListBreathingRates;
        List<Label> _ListBloodPressure;
        List<Label> _ListTemperatures;

        List<PatientData> _listPatient;
        PatientAlarmer _alarmer;

        CheckBox _alarmMuter;
        Label _pulseRate;
        Label _pulseRate1;
        Label _breathingRate;
        Label _systolicPressure;
        Label _diastolicPressure;
        Label _temperature;

        public PatientMonitoringController(MainWindow window, IPatientFactory patientFactory)
        {
            _patientFactory = patientFactory;
            _mainWindow = window;
            _pulseRate = _mainWindow.pulseRate;
            _breathingRate = _mainWindow.breathingRate;
            _systolicPressure = _mainWindow.systolic;
            _diastolicPressure = _mainWindow.diastolic;
            _temperature = _mainWindow.temperature;
            _alarmMuter = _mainWindow.AlarmMute;    
        }

        public void RunMonitor()
        {
            setupComponents();
            setupUI();
        }

        void setupUI()
        {
            _mainWindow.patientSelector.SelectionChanged
                += new System.Windows.Controls.SelectionChangedEventHandler(newPatientSelected);

            _mainWindow.heartRateLower.AlarmValue = (int)DefaultSettings.LOWER_PULSE_RATE;
            _mainWindow.breathingRateLower.AlarmValue = (int)DefaultSettings.LOWER_BREATHING_RATE;
            _mainWindow.temperatureLower.AlarmValue = (int)DefaultSettings.LOWER_TEMPERATURE;
            _mainWindow.systolicLower.AlarmValue = (int)DefaultSettings.LOWER_SYSTOLIC;
            _mainWindow.diastolicLower.AlarmValue = (int)DefaultSettings.LOWER_DIASTOLIC;

            _mainWindow.heartRateUpper.AlarmValue = (int)DefaultSettings.UPPER_PULSE_RATE;
            _mainWindow.breathingRateUpper.AlarmValue = (int)DefaultSettings.UPPER_BREATHING_RATE;
            _mainWindow.temperatureUpper.AlarmValue = (int)DefaultSettings.UPPER_TEMPERATURE;
            _mainWindow.systolicUpper.AlarmValue = (int)DefaultSettings.UPPER_SYSTOLIC;
            _mainWindow.diastolicUpper.AlarmValue = (int)DefaultSettings.UPPER_DIASTOLIC;

            _mainWindow.heartRateLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.breathingRateLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.temperatureLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.systolicLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.diastolicLower.ValueChanged += new EventHandler(limitsChanged);

            _mainWindow.heartRateUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.breathingRateUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.temperatureUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.systolicUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.diastolicUpper.ValueChanged += new EventHandler(limitsChanged);
        }

        void limitsChanged(object sender, EventArgs e)
        {
           /* _alarmer.PulseRateTester.LowerLimit = _mainWindow.heartRateLower.AlarmValue;
            _alarmer.BreathingRateTester.LowerLimit = _mainWindow.breathingRateLower.AlarmValue;
            _alarmer.TemperatureTester.LowerLimit = _mainWindow.temperatureLower.AlarmValue;
            _alarmer.SystolicBpTester.LowerLimit = _mainWindow.systolicLower.AlarmValue;
            _alarmer.DiastolicBpTester.LowerLimit = _mainWindow.diastolicLower.AlarmValue;

            _alarmer.PulseRateTester.UpperLimit = _mainWindow.heartRateUpper.AlarmValue;
            _alarmer.BreathingRateTester.UpperLimit = _mainWindow.breathingRateUpper.AlarmValue;
            _alarmer.TemperatureTester.UpperLimit = _mainWindow.temperatureUpper.AlarmValue;
            _alarmer.SystolicBpTester.UpperLimit = _mainWindow.systolicUpper.AlarmValue;
            _alarmer.DiastolicBpTester.UpperLimit = _mainWindow.diastolicUpper.AlarmValue;*/
        }

        void setupComponents()
        {
            _patientData = (PatientData)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientData);
            _patientData1 = (PatientData)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientData);
            _dataReader = (PatientDataReader)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientDataReader);
            _dataReader1 = (PatientDataReader)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientDataReader);
            _alarmer = (PatientAlarmer)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientAlarmer);
            _alarmer.BreathingRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.DiastolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.PulseRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.SystolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.TemperatureAlarm += new EventHandler(soundMutableAlarm);

            /*string fileName = @"..\..\..\" + "Bed 1" + ".csv";
            _dataReader.Connect(fileName);

            string fileName2 = @"..\..\..\" + "Bed 2" + ".csv";
            _dataReader1.Connect(fileName2);*/

            _tickTimer.Stop();
            _tickTimer.Interval= TimeSpan.FromMilliseconds(1000);
            _tickTimer.Tick += new EventHandler(updateReadings);
            _tickTimer.Start();
        }

        void updateReadings(object sender, EventArgs e)
        {

            /*_patientData.SetPatientData(_dataReader.getData());
            _patientData1.SetPatientData(_dataReader1.getData());
            _pulseRate.Content = _patientData.PulseRate;
            _pulseRate1.Content = _patientData1.PulseRate;
            _breathingRate.Content = _patientData.BreathingRate;
            _systolicPressure.Content = _patientData.SystolicBloodPressure;
            //_diastolicPressure.Content = _patientData.DiastolicBloodPressure;
            _temperature.Content = _patientData.Temperature;
            //_alarmer.ReadingsTest(_patientData);*/
            for (int i = 0; i < 8; i++)
            {
                _patientData.SetPatientData(_listPatientDataReader[i].getData());
                _ListPulseRates[i].Content = _patientData.PulseRate;
                _ListBreathingRates[i].Content = _patientData.BreathingRate;
                _ListBloodPressure[i].Content = _patientData.SystolicBloodPressure;
                _ListTemperatures[i].Content = _patientData.Temperature;
            }
        }

        void newPatientSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _tickTimer.Stop();
           // string fileName = @"..\..\..\" + _mainWindow.patientSelector.SelectedValue + ".csv";
           // _dataReader.Connect(fileName);
            _tickTimer.Start();
        }

        void soundMutableAlarm(object sender, EventArgs e)
        {
            if(_alarmMuter.IsChecked == false)
            {
                //_mainWindow.soundMutableAlarm();
            }
        }
    }
}
