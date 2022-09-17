namespace Critters.Models;

public class RosterView
{
    public List<Roster> Roster { get; set; }
    public List<Temp> Temp { get; set; }
    public DeleteConditions conditions { get; set; }
}