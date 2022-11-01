using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Critters.Models
{
    [DataContract]
    [Table("roster")]
    public class Roster
    {
        [DataMember]
        [Key]
        public string Playerid { get; set; }

        [DataMember]
        public int? Jersey{ get; set; }

        [DataMember]
        public string Fname{ get; set; }

        [DataMember]
        public string Sname{ get; set; }

        [DataMember]
        public string Position{ get; set; }

        [DataMember]
        public DateTime Birthday{ get; set; }

        [DataMember]
        public int? Weight{ get; set; }

        [DataMember]
        public int? Height{ get; set; }

        [DataMember]
        public string Birthcity{ get; set; }

        [DataMember]
        public string Birthstate{ get; set; }

        public static implicit operator Roster(Temp temp)
        {
            return new Roster()
            {
                Playerid = temp.Playerid,
                Birthcity = temp.Birthcity,
                Birthday = temp.Birthday,
                Birthstate = temp.Birthstate,
                Fname = temp.Fname,
                Height = temp.Height,
                Weight = temp.Weight,
                Jersey = temp.Jersey,
                Position = temp.Position,
                Sname = temp.Sname
            };
        }
    }
}
