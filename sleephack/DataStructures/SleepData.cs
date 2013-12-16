using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SleepHack.DataStructures
{
    [DataContract]
    public class SleepData
    {
        public SleepData(string cycleId, string facebookID)
        {
            FID = facebookID;
            CycleId = cycleId;
            StartTime = DateTime.Now;
            Sleepmeasures = new List<Sleepmeasure>();
        }

        [DataMember(Name = "FID")]
        public string FID { get; set; }

        [DataMember(Name = "CycleId")]
        public string CycleId { get; set; }

        [DataMember(Name = "StartTime")]
        public DateTime StartTime { get; set; }

        [DataMember(Name = "EndTime")]
        public DateTime EndTime { get; set; }

        [DataMember(Name = "Sleepmeasures")]
        public List<Sleepmeasure> Sleepmeasures { get; set; }
    }
}