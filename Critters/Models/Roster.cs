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
    }
}
