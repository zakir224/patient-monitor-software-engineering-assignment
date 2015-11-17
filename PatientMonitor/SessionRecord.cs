using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientMonitor
{
    class SessionRecord
    {
        //attributes
        private int staffId;
        private string startTime;
        private string endTime;

        //constructor 
        public SessionRecord(int staffId, string startTime, string endTime)
        {
            this.staffId = staffId;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        //properties
        public int StaffId
        {   
            get {return staffId;}
            set { staffId = value; }
        }

        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
    }
}
