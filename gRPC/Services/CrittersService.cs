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
        var model = new CrittersModel
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

    public override async Task<DeletePlayersResponse> DeletePlayers(DeletePlayersRequest request, ServerCallContext context)
    {
        DeleteConditions conditions = JsonSerializer.Deserialize<DeleteConditions>(request.SerializedConditions)!;

        await Delete(conditions.From, conditions.To, conditions.Position, request.AllRosters,
            new List<string>(request.CheckboxesRosters));
        await _context.SaveChangesAsync();
        
        return await Task.FromResult(new DeletePlayersResponse
        {
            DeleteResult = "Success"
        });
    }

    public async Task Delete(DateTime fromDate, DateTime toDate, string position, string allRosters,
        List<string> checkboxesRosters)
    {
        IEnumerable<Roster> deletedPlayers = _context.Rosters;

        if (!string.IsNullOrEmpty(allRosters))
        {
            foreach (var player in deletedPlayers)
            {
                _context.Rosters.Remove(player);
                await _context.Temps.AddAsync(player); // implicit operator is optional
            }

            return;
        }

        if (checkboxesRosters.Count != 0)
        {
            foreach (string playerid in checkboxesRosters)
            {
                Roster? player = await _context.Rosters.FindAsync(playerid);
                if (player != null)
                {
                    await _context.Temps.AddAsync(player); // implicit operator is optional
                    _context.Rosters.Remove(player);
                }
            }

            return;
        }

        DateTime defaultDate = new DateTime();

        if (fromDate.Equals(defaultDate) &&
            toDate.Equals(defaultDate) &&
            string.IsNullOrEmpty(position))
            return;

        if (!fromDate.Equals(defaultDate))
            deletedPlayers = deletedPlayers.Where(player => DateTime.Compare(player.Birthday, fromDate) >= 0);

        if (!toDate.Equals(defaultDate))
            deletedPlayers = deletedPlayers.Where(player => DateTime.Compare(player.Birthday, toDate) <= 0);

        if (!String.IsNullOrEmpty(position))
            deletedPlayers = deletedPlayers.Where(player => player.Position == position);


        foreach (var player in deletedPlayers)
        {
            _context.Rosters.Remove(player);
            await _context.Temps.AddAsync(player); // implicit operator is optional
        }
    }
    
    public async Task Recover(string allTemps, List<string> checkboxesTemps)
    {
        IEnumerable<Temp> deletedPlayers = _context.Temps;

        if (!string.IsNullOrEmpty(allTemps))
        {
            foreach (var player in deletedPlayers)
            {
                _context.Temps.Remove(player);
                await _context.Rosters.AddAsync(player); // implicit operator is optional
            }
            
            return;
        }
            
        if (checkboxesTemps.Count != 0)
        {
            foreach (string playerid in checkboxesTemps)
            {
                Temp? player = await _context.Temps.FindAsync(playerid);
                if (player != null)
                {
                    await _context.Rosters.AddAsync(player); // implicit operator is optional
                    _context.Temps.Remove(player);
                }
            }
        }
    }

    public override async Task<RecoverPlayersResponse> RecoverPlayers(RecoverPlayersRequest request,
        ServerCallContext context)
    {
        await Recover(request.AllTemps, new List<string>(request.CheckboxesTemps));
        await _context.SaveChangesAsync();

        return await Task.FromResult(new RecoverPlayersResponse
        {
            RecoverResult = "Success"
        });
    }
}