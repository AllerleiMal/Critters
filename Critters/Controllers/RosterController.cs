using Microsoft.AspNetCore.Mvc;

namespace Critters.Controllers
{
    public class RosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
