using System.Text.Json;
using gRPC.Context;
using Grpc.Core;
using gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Services;

public class CrittersService : Critters.CrittersBase
{
    private readonly RosterDbContext _context = new RosterDbContext();

    public CrittersService(RosterDbContext options)
    {
        _context = options;
    }
    
    public override async Task<GetPlayersResponse> GetPlayers(GetPlayersRequest request, ServerCallContext context)
    {
        var model = new RosterView
        {
            Temps = await _context.Temps.ToListAsync(),
            Rosters = await _context.Rosters.ToListAsync(),
            Conditions = new DeleteConditions()
        };
        model.Temps.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));
        model.Rosters.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));
        return await Task.FromResult(new GetPlayersResponse
            {
                SerializedPlayers = JsonSerializer.Serialize(model)
            }
        );
    }
}