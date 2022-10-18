using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCF.Models
{
    [DataContract]
    [Table("temp")]
    public class Temp
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

        public static implicit operator Temp(Roster roster)
        {
            return new Temp()
            {
                Playerid = roster.Playerid,
                Birthcity = roster.Birthcity,
                Birthday = roster.Birthday,
                Birthstate = roster.Birthstate,
                Fname = roster.Fname,
                Height = roster.Height,
                Weight = roster.Weight,
                Jersey = roster.Jersey,
                Position = roster.Position,
                Sname = roster.Sname
            };
        }
    }
}
