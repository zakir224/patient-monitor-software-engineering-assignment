using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientMonitor
{
    class Bed
    {
        public Boolean HeartRateEnabled { get ; set; }
        public Boolean PulseRateEnabled { get; set; }
        public Boolean BloodPressureEnabled { get; set; }
        public Boolean TemparatureEnabled { get; set; }

        public Bed()
        {

        }
    }
}
