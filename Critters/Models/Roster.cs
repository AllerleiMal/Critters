using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("roster")]
    public class Roster
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

        public static implicit operator Roster(Temp temp)
        {
            return new Roster()
            {
                playerid = temp.playerid,
                birthcity = temp.birthcity,
                birthday = temp.birthday,
                birthstate = temp.birthstate,
                fname = temp.fname,
                height = temp.height,
                weight = temp.weight,
                jersey = temp.jersey,
                position = temp.position,
                sname = temp.sname
            };
        }
    }
}
