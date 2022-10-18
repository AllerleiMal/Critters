using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCF.Models
{
    [DataContract]
    public class RosterView
    {
        [DataMember]
        public List<Roster> Rosters { get; set; }

        [DataMember]
        public List<Temp> Temps { get; set; }

        [DataMember]
        public DeleteConditions Conditions { get; set; }
    }
}