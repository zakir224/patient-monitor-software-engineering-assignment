using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PatientMonitor
{
    public partial class ModulePreferencePopUp : Form
    {
        List<CheckBox> _ListModuleBloodCheckBox;
        List<CheckBox> _ListModuleHeartCheckBox;
        List<CheckBox> _ListModuleTemparatureCheckBox;
        List<CheckBox> _ListModulePulseCheckBox;
        Window1 callbackWindow;

        public ModulePreferencePopUp(Window1 callbackWindow)
        {
            InitializeComponent();
            this.callbackWindow = callbackWindow;
            _ListModuleBloodCheckBox = new List<CheckBox>(8);
            _ListModuleTemparatureCheckBox = new List<CheckBox>(8);
            _ListModulePulseCheckBox = new List<CheckBox>(8);
            _ListModuleHeartCheckBox = new List<CheckBox>(8);

            initReferences();
            setCurrentPreference();
        }

        private void initReferences()
        {
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed1);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed2);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed3);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed4);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed5);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed6);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed7);
            _ListModuleBloodCheckBox.Add(this.enableBloodPressureBed8);


            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed1);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed2);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed3);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed4);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed5);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed6);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed7);
            _ListModuleHeartCheckBox.Add(this.enableHeartRateBed8);

            _ListModulePulseCheckBox.Add(this.enablePulseRateBed1);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed2);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed3);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed4);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed5);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed6);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed7);
            _ListModulePulseCheckBox.Add(this.enablePulseRateBed8);

            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed1);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed2);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed3);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed4);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed5);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed6);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed7);
            _ListModuleTemparatureCheckBox.Add(this.enableTemparatureBed8);
        }

        private void setCurrentPreference()
        {
            for (int i = 0; i < 8; i++)
            {
                _ListModuleTemparatureCheckBox[i].Checked = ModuleSetting.Instance.getBedSetting(i).TemparatureEnabled;
                _ListModuleHeartCheckBox[i].Checked = ModuleSetting.Instance.getBedSetting(i).HeartRateEnabled;
                _ListModulePulseCheckBox[i].Checked = ModuleSetting.Instance.getBedSetting(i).PulseRateEnabled;
                _ListModuleBloodCheckBox[i].Checked = ModuleSetting.Instance.getBedSetting(i).BloodPressureEnabled;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                ModuleSetting.Instance.getBedSetting(i).TemparatureEnabled = _ListModuleTemparatureCheckBox[i].Checked;
                ModuleSetting.Instance.getBedSetting(i).HeartRateEnabled = _ListModuleHeartCheckBox[i].Checked;
                ModuleSetting.Instance.getBedSetting(i).PulseRateEnabled = _ListModulePulseCheckBox[i].Checked;
                ModuleSetting.Instance.getBedSetting(i).BloodPressureEnabled = _ListModuleBloodCheckBox[i].Checked;
            }

            callbackWindow.update();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ModuleSetting.Instance.setDefaultModuleSetting();
            callbackWindow.update();
        }
    }
}
