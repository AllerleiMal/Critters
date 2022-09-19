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
        public async Task<IActionResult> Index()
        {
            ViewBag.Positions = new List<string>{"RW", "D", "LW", "C", "G"};
            // ViewBag.DeleteConditions = new DeleteConditions();
            RosterView model = new RosterView();
            model.Temp = await _context.Temps.ToListAsync();
            model.Roster = await _context.Rosters.ToListAsync();
            return View(model);
        }
        
        
        [HttpPost]
        public async Task<ViewResult> Index(RosterView model)
        {
            await MoveRowsToTemp(fromDate: model.conditions.From, toDate: model.conditions.To,
                position: model.conditions.Position);
            model.Temp = await _context.Temps.ToListAsync();
            model.Roster = await _context.Rosters.ToListAsync();
            return View(model);
        }

        public async Task MoveRowsToTemp(DateTime fromDate, DateTime toDate, string position)
        {
            List<Roster> deletedPlayers = await (from players in _context.Rosters
                where DateTime.Compare(players.birthday, fromDate) > 0 &&
                      DateTime.Compare(players.birthday, toDate) < 0 && players.position == position
                select players).ToListAsync();
            foreach (var player in deletedPlayers)
            {
                _context.Rosters.Remove(player);
                Console.WriteLine($"{player.fname}, {player.sname}");
                await _context.Temps.AddAsync(Temp.GetFromRoster(player));
            }

            await _context.SaveChangesAsync();
        }
    }
}
