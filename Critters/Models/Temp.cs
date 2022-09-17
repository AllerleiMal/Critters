using System.ComponentModel.DataAnnotations.Schema;

namespace Critters.Models
{
    [Table("temp")]
    public class Temp : Roster
    {
        public static Temp GetFromRoster(Roster roster)
        {
            Temp temp = new Temp();
            temp.playerid = roster.playerid;
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
