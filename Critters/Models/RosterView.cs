namespace Critters.Models;

public class RosterView
{
    public List<Roster> Rosters { get; set; }
    public List<Temp> Temps { get; set; }
    public DeleteConditions Conditions { get; set; }
}