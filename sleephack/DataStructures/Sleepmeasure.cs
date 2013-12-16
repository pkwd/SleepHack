using System;
using System.Runtime.Serialization;

namespace SleepHack.DataStructures
{
    [DataContract]
    public class Sleepmeasure
    {
        [DataMember(Name = "MeasureDate")]
        public DateTime MeasureDate { get; set; }

        [DataMember(Name = "Movement")]
        public Movement Movement { get; set; }

        [DataMember(Name = "ActivityType")]
        public string ActivityType { get; set; }

    }
}