using Critters.Context;
using Critters.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Players()
        {
            //ViewBag.Positions = new List<string>{"RW", "D", "LW", "C", "G"};
            // ViewBag.DeleteConditions = new DeleteConditions();
            RosterView model = new RosterView();
            model.Temps = await _context.Temps.ToListAsync();
            model.Rosters = await _context.Rosters.ToListAsync();
            return View(model);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Players(RosterView model, string delete, string recover, string all_rosters,
            List<string> checkboxes_rosters, string all_temps, List<string> checkboxes_temps)
        {
            if (!string.IsNullOrEmpty(delete))
            {
                await Delete(
                    fromDate: model.conditions.From, 
                    toDate: model.conditions.To,
                    position: model.conditions.Position,
                    all_rosters: all_rosters,
                    checkboxes_rosters: checkboxes_rosters);
            }
            else if (!string.IsNullOrEmpty(recover))
            {
                await Recover(
                    fromDate: model.conditions.From,
                    toDate: model.conditions.To,
                    position: model.conditions.Position,
                    all_temps: all_temps,
                    checkboxes_temps: checkboxes_temps);
            }

            await _context.SaveChangesAsync();

            model.Temps = await _context.Temps.ToListAsync();
            model.Rosters = await _context.Rosters.ToListAsync();

            return View(model);
        }

        public async Task Recover(DateTime fromDate, DateTime toDate, string position, string all_temps, List<string> checkboxes_temps)
        {
            IEnumerable<Temp> deletedPlayers = _context.Temps;

            if (!string.IsNullOrEmpty(all_temps))
            {
                foreach (var player in deletedPlayers)
                {
                    _context.Temps.Remove(player);
                    await _context.Rosters.AddAsync(player); // implicit operator is optional
                }

                return;
            }

            if (checkboxes_temps != null)
            {
                foreach (string playerid in checkboxes_temps)
                {
                    Temp? player = await _context.Temps.FindAsync(playerid);
                    if (player != null)
                    {
                        await _context.Rosters.AddAsync(player); // implicit operator is optional
                        _context.Temps.Remove(player);
                    }
                }

                return;
            }
        }

        public async Task Delete(DateTime fromDate, DateTime toDate, string position, string all_rosters, List<string> checkboxes_rosters)
        {
            IEnumerable<Roster> deletedPlayers = _context.Rosters;

            if(!string.IsNullOrEmpty(all_rosters))
            {
                foreach (var player in deletedPlayers)
                {
                    _context.Rosters.Remove(player);
                    await _context.Temps.AddAsync(player); // implicit operator is optional
                }

                return;
            }

            if (checkboxes_rosters != null)
            {
                foreach (string playerid in checkboxes_rosters)
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
                deletedPlayers = deletedPlayers.Where(player => DateTime.Compare(player.birthday, fromDate) >= 0);

            if (!toDate.Equals(defaultDate))
                deletedPlayers = deletedPlayers.Where(player => DateTime.Compare(player.birthday, toDate) <= 0);

            if (!String.IsNullOrEmpty(position))
                deletedPlayers = deletedPlayers.Where(player => player.position == position);


            foreach (var player in deletedPlayers)
            {
                _context.Rosters.Remove(player);
                await _context.Temps.AddAsync(player); // implicit operator is optional
            }
        }

    }
}