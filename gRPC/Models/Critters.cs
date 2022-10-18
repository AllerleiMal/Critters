namespace gRPC.Models;

public class Critters
{
    public List<Roster> Rosters { get; set; }
    public List<Temp> Temps { get; set; }
    public DeleteConditions Conditions { get; set; }
}