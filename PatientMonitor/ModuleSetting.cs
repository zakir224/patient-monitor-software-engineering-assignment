using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientMonitor
{
    class ModuleSetting
    {
        private static ModuleSetting instance;
        public List<Bed> _ListBedModules { get; private set;}

        private ModuleSetting() 
        {
            _ListBedModules = new List<Bed>(8);
            setDefaultModuleSetting();
        }

        public static ModuleSetting Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ModuleSetting();
                }
                return instance;
            }
        }

        public void setDefaultModuleSetting() 
        {
            Bed bed;

            if (_ListBedModules.Count > 0 && _ListBedModules != null)
                _ListBedModules = new List<Bed>();

            for (int i = 0 ; i < 8 ; i++)
            {
                bed = new Bed();
                bed.BloodPressureEnabled = true;
                bed.HeartRateEnabled = true;
                bed.TemparatureEnabled = true;
                bed.PulseRateEnabled = true;
                _ListBedModules.Add(bed);
            }
        }

        public void changeSetting( int bedId, Bed bed )
        {
            if( bedId >= 0 && bedId < 8 )
            {
                _ListBedModules[bedId].HeartRateEnabled = bed.HeartRateEnabled;
                _ListBedModules[bedId].PulseRateEnabled = bed.PulseRateEnabled;
                _ListBedModules[bedId].TemparatureEnabled = bed.TemparatureEnabled;
                _ListBedModules[bedId].BloodPressureEnabled = bed.BloodPressureEnabled;
            }
        }

        public Bed getBedSetting(int bedId)
        {
            return _ListBedModules[bedId];
        }

    }
}
