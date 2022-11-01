using Critters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace Critters.Controllers
{
    public class RosterController : Controller
    {
        private readonly string _serviceURL;
        private readonly List<SelectListItem> _positions;
        public RosterController()
        {
            _serviceURL = "http://localhost:60975/WCFserviceREST.svc";
            _positions = new List<SelectListItem>
            {
                new("RW", "RW"),
                new("D", "D"),
                new("LW", "LW"),
                new("C", "C"),
                new("G", "G")
            };
        }


        [HttpGet]
        public async Task<IActionResult> Players()
        {
            return View(getCritters());
        }


        private RosterView getCritters()
        {
            ViewBag.Positions = _positions;

            using (WebClient proxy = new WebClient())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(RosterView));
                byte[] data = proxy.DownloadData(_serviceURL + "/GetCritters");
                RosterView model = (ser.ReadObject(new MemoryStream(data)) as RosterView)!;

                return model;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Players(RosterView model, string delete, string recover, string allRosters,
            List<string> checkboxesRosters, string allTemps, List<string> checkboxesTemps)
        {

            if (!string.IsNullOrEmpty(delete))
            {
                DeleteContract contract = new DeleteContract()
                {
                    FromDate = model.Conditions.From,
                    ToDate = model.Conditions.To,
                    Position = model.Conditions.Position ?? "",
                    AllRosters = allRosters ?? "",
                    CheckboxesRosters = checkboxesRosters
                };

                using (WebClient client = new WebClient())
                {
                    client.Headers["Content-type"] = @"application/xml";

                    MemoryStream ms = new MemoryStream();
                    DataContractSerializer ser = new DataContractSerializer(typeof(DeleteContract));
                    ser.WriteObject(ms, contract);
                    client.UploadData(_serviceURL + "/Delete", "DELETE", ms.ToArray());
                }
            }
            else if (!string.IsNullOrEmpty(recover))
            {
                RecoverContract contract = new RecoverContract()
                {
                    AllTemps = allTemps ?? "",
                    CheckboxesTemps = checkboxesTemps
                };

                using (WebClient client = new WebClient())
                {
                    client.Headers["Content-type"] = @"application/xml";

                    MemoryStream ms = new MemoryStream();
                    DataContractSerializer ser = new DataContractSerializer(typeof(RecoverContract));
                    ser.WriteObject(ms, contract);
                    client.UploadData(_serviceURL + "/Recover", "PUT", ms.ToArray());
                }

            }

            return View(getCritters());
        }



    }
}