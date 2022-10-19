using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Critters.Models
{
    [DataContract]
    public class RecoverContract
    {
        [DataMember]
        public string AllTemps { get; set; }

        [DataMember]
        public List<string> CheckboxesTemps { get; set; }
    }
}