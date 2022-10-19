using System.Runtime.Serialization;

namespace Critters.Models
{
    public class Roster
    {
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
