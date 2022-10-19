using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Critters.Models
{
    [DataContract]
    public class DeleteContract
    {
        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public string AllRosters { get; set; }

        [DataMember]
        public List<string> CheckboxesRosters  { get; set; }
    }
}