using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("roster")]
    public class Roster
    {
        [Key]
        [Required]
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
    }
}
