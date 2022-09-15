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
            List<Roster> rosters = await _context.Rosters.Select(r => new Roster{
                playerid = r.playerid,
                fname = r.fname,
                }).ToListAsync();
            ViewBag.rosters = rosters;
            return View(rosters);
        }
    }
}
