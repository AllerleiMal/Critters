using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCF.Models
{
    [DataContract]
    [KnownType(typeof(Roster))]
    [KnownType(typeof(Temp))]
    [KnownType(typeof(DeleteConditions))]
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