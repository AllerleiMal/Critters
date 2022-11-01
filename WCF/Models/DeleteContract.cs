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
        [DataMember(Name = "FromDate")]
        public DateTime FromDate { get; set; }

        [DataMember(Name = "ToDate")]
        public DateTime ToDate { get; set; }

        [DataMember(Name = "PositionDate")]
        public string Position { get; set; }

        [DataMember(Name = "AllRosters")]
        public string AllRosters { get; set; }

        [DataMember(Name = "CheckboxesRosters")]
        public List<string> CheckboxesRosters  { get; set; }
            = new List<string>();

        public DeleteContract()
        {
            CheckboxesRosters = new List<string>();
        }
    }
}