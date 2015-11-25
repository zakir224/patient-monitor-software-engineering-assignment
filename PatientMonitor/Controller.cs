using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PatientMonitor
{
    class Controller
    {
        readonly Window1 _mainWindow = null;
        readonly IPatientFactory _patientFactory = null;
        DispatcherTimer _tickTimer = new DispatcherTimer();
        PatientData _patientData;
        List<PatientDataReader> _listPatientDataReader;
        List<Label> _ListPulseRates;
        List<Label> _ListBreathingRates;
        List<Label> _ListBloodPressure;
        List<Label> _ListTemperatures;
        List<Canvas> _ListModuleBloodCanvas;
        List<Canvas> _ListModuleHeartCanvas;
        List<Canvas> _ListModuleTemparatureCanvas;
        List<Canvas> _ListModulePulseCanvas;
        ModuleSetting moduleSetting;
        PatientAlarmer _alarmer;

        CheckBox _alarmMuter;

        public Controller(Window1 window, IPatientFactory patientFactory)
        {
            _patientFactory = patientFactory;
            _mainWindow = window;
            moduleSetting = ModuleSetting.Instance;
            initModuleSettings();
            DataInit();
            GrabUiControlReferences();
            updateView();
        }

        public void DataInit()
        {
            string fileName;
            _listPatientDataReader = new List<PatientDataReader>();
            _ListPulseRates = new List<Label>();
            _ListBloodPressure = new List<Label>();
            _ListTemperatures = new List<Label>();
            _ListBreathingRates = new List<Label>();
            _ListModuleBloodCanvas = new List<Canvas>();
            _ListModuleHeartCanvas = new List<Canvas>();
            _ListModulePulseCanvas = new List<Canvas>();
            _ListModuleTemparatureCanvas = new List<Canvas>();
            for (int i = 0; i < 8; i++)
            {
                fileName = @"..\..\..\" + "Bed "+(i+1) + ".csv";
                PatientDataReader _dataReader = (PatientDataReader)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientDataReader);
                _dataReader.Connect(fileName);
                _listPatientDataReader.Add(_dataReader);
            }


        }

        private void initModuleSettings()
        {

        }

        public void GrabUiControlReferences()
        {
            _ListPulseRates.Add(_mainWindow.bed_1_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_2_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_3_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_4_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_5_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_6_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_7_heart_rate);
            _ListPulseRates.Add(_mainWindow.bed_8_heart_rate);

            _ListTemperatures.Add(_mainWindow.bed_1_temparature);
            _ListTemperatures.Add(_mainWindow.bed_2_temparature);
            _ListTemperatures.Add(_mainWindow.bed_3_temparature);
            _ListTemperatures.Add(_mainWindow.bed_4_temparature);
            _ListTemperatures.Add(_mainWindow.bed_5_temparature);
            _ListTemperatures.Add(_mainWindow.bed_6_temparature);
            _ListTemperatures.Add(_mainWindow.bed_7_temparature);
            _ListTemperatures.Add(_mainWindow.bed_8_temparature);

            _ListBreathingRates.Add(_mainWindow.bed_1_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_2_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_3_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_4_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_5_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_6_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_7_breathing_rate);
            _ListBreathingRates.Add(_mainWindow.bed_8_breathing_rate);

            _ListBloodPressure.Add(_mainWindow.bed_1_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_2_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_3_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_4_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_5_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_6_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_7_blood_pressure);
            _ListBloodPressure.Add(_mainWindow.bed_8_blood_pressure);

            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_1);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_2);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_3);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_4);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_5);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_6);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_7);
            _ListModuleBloodCanvas.Add(_mainWindow.module_blood_bed_8);


            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_1);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_2);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_3);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_4);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_5);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_6);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_7);
            _ListModulePulseCanvas.Add(_mainWindow.module_breathing_bed_8);


            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_1);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_2);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_3);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_4);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_5);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_6);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_7);
            _ListModuleHeartCanvas.Add(_mainWindow.module_heart_bed_8);


            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_1);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_2);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_3);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_4);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_5);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_6);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_7);
            _ListModuleTemparatureCanvas.Add(_mainWindow.module_temparature_bed_8);
        }

        public void RunMonitor()
        {
            setupComponents();
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
            _alarmer = (PatientAlarmer)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientAlarmer);
            _alarmer.BreathingRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.DiastolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.PulseRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.SystolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.TemperatureAlarm += new EventHandler(soundMutableAlarm);

            _tickTimer.Stop();
            _tickTimer.Interval= TimeSpan.FromMilliseconds(1000);
            _tickTimer.Tick += new EventHandler(updateReadings);
            _tickTimer.Start();
        }

        void updateReadings(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                _patientData.SetPatientData(_listPatientDataReader[i].getData());
                _ListPulseRates[i].Content = _patientData.PulseRate;
                _ListBreathingRates[i].Content = _patientData.BreathingRate;
                _ListBloodPressure[i].Content = _patientData.SystolicBloodPressure;
                _ListTemperatures[i].Content = _patientData.Temperature;
            }
        }

        public void updateView()
        {
            for (int i = 0; i < 8; i++)
            {
                _ListModuleBloodCanvas[i].Visibility = moduleSetting.getBedSetting(i).BloodPressureEnabled ?
                                                        System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                _ListModuleHeartCanvas[i].Visibility = moduleSetting.getBedSetting(i).HeartRateEnabled ?
                                                        System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                _ListModuleTemparatureCanvas[i].Visibility = moduleSetting.getBedSetting(i).TemparatureEnabled ?
                                                        System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                _ListModulePulseCanvas[i].Visibility = moduleSetting.getBedSetting(i).PulseRateEnabled ?
                                                        System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
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
