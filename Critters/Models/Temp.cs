using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("temp")]
    public class Temp
    {
        [Key]
        public string Playerid { get; set; }
        public int? Jersey{ get; set; }
        public string Fname{ get; set; }
        public string Sname{ get; set; }
        public string Position{ get; set; }
        public DateTime Birthday{ get; set; }
        public int? Weight{ get; set; }
        public int? Height{ get; set; }
        public string Birthcity{ get; set; }
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
