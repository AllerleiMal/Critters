using Critters.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Critters.Controllers
{
    public class RosterController : Controller
    {
        private readonly RosterDbContext _context;

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
