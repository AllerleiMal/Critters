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
    
    public override Task<GetPlayersResponse> GetPlayers(GetPlayersRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetPlayersResponse
            {
                SerializedPlayers = JsonSerializer.Serialize(new RosterView()
                {
                    Conditions = new DeleteConditions(),
                    Rosters = _context.Rosters.ToListAsync().Result,
                    Temps = _context.Temps.ToListAsync().Result
                })
            }
        );
    }
}