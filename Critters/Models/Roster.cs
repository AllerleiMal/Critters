using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("dbo.roster")]
    public class Roster
    {
        [Key]
        public string playerid;
        public string jersey;
        public string fname;
        public string sname;
        public string position;
        public DateTime birthday;
        public double weight;
        public double height;
        public string birthcity;
        public string birthstate;
    }
}
