using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("temp")]
    public class Temp
    {
        [Key]
        public string playerid { get; set; }
        public int? jersey{ get; set; }
        public string fname{ get; set; }
        public string sname{ get; set; }
        public string position{ get; set; }
        public DateTime birthday{ get; set; }
        public int? weight{ get; set; }
        public int? height{ get; set; }
        public string birthcity{ get; set; }
        public string birthstate{ get; set; }

        public static implicit operator Temp(Roster roster)
        {
            return new Temp()
            {
                playerid = roster.playerid,
                birthcity = roster.birthcity,
                birthday = roster.birthday,
                birthstate = roster.birthstate,
                fname = roster.fname,
                height = roster.height,
                weight = roster.weight,
                jersey = roster.jersey,
                position = roster.position,
                sname = roster.sname
            };
        }
    }
}
