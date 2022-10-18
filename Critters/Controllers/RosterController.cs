using Critters.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Critters.Controllers
{
    public class RosterController : Controller
    {
        private Critters.CrittersClient _client;
        

        public RosterController()
        {
            _client = new Critters.CrittersClient(GrpcChannel.ForAddress("https://localhost:7201/"));
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
            var reply = await _client.GetPlayersAsync(new GetPlayersRequest { Name = "get" });
            CrittersModel model = JsonSerializer.Deserialize<CrittersModel>(reply.SerializedPlayers)!;
            return View(model);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Players(CrittersModel model, string delete, string recover, string allRosters,
            List<string> checkboxesRosters, string allTemps, List<string> checkboxesTemps)
        {
            if (!string.IsNullOrEmpty(delete))
            {
                await _client.DeletePlayersAsync(new DeletePlayersRequest
                {

                    SerializedConditions = JsonSerializer.Serialize(model.Conditions),
                    AllRosters = string.IsNullOrEmpty(allRosters) ? "" : allRosters,
                    CheckboxesRosters = { checkboxesRosters }
                });
            }
            else if (!string.IsNullOrEmpty(recover))
            {
                await _client.RecoverPlayersAsync(new RecoverPlayersRequest
                {
                    AllTemps = string.IsNullOrEmpty(allTemps) ? "" : allTemps,
                    CheckboxesTemps = { checkboxesTemps }
                });
            }

            ViewBag.Positions = GetAllPositions();
            var reply = await _client.GetPlayersAsync(new GetPlayersRequest { Name = "get" });
            model = JsonSerializer.Deserialize<CrittersModel>(reply.SerializedPlayers)!;
            
            return View(model);
        }

    }
}