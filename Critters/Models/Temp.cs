using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("temp")]
    public class Temp
    {
        [Key]
        [Required]
        public string id { get; set; }
        [Required]
        public int? jersey{ get; set; }
        [Required]
        public string fname{ get; set; }
        [Required]
        public string sname{ get; set; }
        [Required]
        public string position{ get; set; }
        [Required]
        public DateTime birthday{ get; set; }
        [Required]
        public int? weight{ get; set; }
        [Required]
        public int? height{ get; set; }
        [Required]
        public string birthcity{ get; set; }
        [Required]
        public string birthstate{ get; set; }
        
        public static Temp GetFromRoster(Roster roster)
        {
            Temp temp = new Temp();
            temp.id = roster.playerid;
            temp.birthcity = roster.birthcity;
            temp.birthday = roster.birthday;
            temp.birthstate = roster.birthstate;
            temp.fname = roster.fname;
            temp.height = roster.height;
            temp.weight = roster.weight;
            temp.jersey = roster.jersey;
            temp.position = roster.position;
            temp.sname = roster.sname;
            return temp;
        }
    }
}
