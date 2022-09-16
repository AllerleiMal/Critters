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
            return View(await _context.Rosters.ToListAsync());
        }
    }
}
