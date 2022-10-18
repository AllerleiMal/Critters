using Critters.Context;
using Critters.Context;
using Critters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Critters.Controllers
{
    public class RosterController : Controller
    {
        private readonly RosterDbContext _context = new RosterDbContext();

        public RosterController(RosterDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GetAllPositions()
        {
            List<SelectListItem> positions = new List<SelectListItem>
            {
                new("RW", "RW"),
                new("D", "D"),
                new("LW", "LW"),
                new("C", "C"),
                new("G", "G")
            };
            return positions;
        }

        [HttpGet]
        public async Task<IActionResult> Players()
        {
            ViewBag.Positions = GetAllPositions();
            RosterView model = new RosterView();
            model.Temps = await _context.Temps.ToListAsync();
            model.Rosters = await _context.Rosters.ToListAsync();
            model.Temps.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));
            model.Rosters.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));
            return View(model);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Players(RosterView model, string delete, string recover, string allRosters,
            List<string> checkboxesRosters, string allTemps, List<string> checkboxesTemps)
        {
            if (!string.IsNullOrEmpty(delete))
            {
                await Delete(
                    fromDate: model.Conditions.From, 
                    toDate: model.Conditions.To,
                    position: model.Conditions.Position,
                    allRosters: allRosters,
                    checkboxesRosters: checkboxesRosters);
            }
            else if (!string.IsNullOrEmpty(recover))
            {
                await Recover(
                    allTemps: allTemps,
                    checkboxesTemps: checkboxesTemps);
            }

            await _context.SaveChangesAsync();

            model.Temps = await _context.Temps.ToListAsync();
            model.Rosters = await _context.Rosters.ToListAsync();
            model.Temps.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));
            model.Rosters.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));

            ViewBag.Positions = GetAllPositions();
            
            return View(model);
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

        public async Task Delete(DateTime fromDate, DateTime toDate, string position, string allRosters, List<string> checkboxesRosters)
        {
            IEnumerable<Roster> deletedPlayers = _context.Rosters;

            if(!string.IsNullOrEmpty(allRosters))
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

    }
}