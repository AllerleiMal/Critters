using Critters.Context;
using Critters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace tempProject
{
    [AspNetCompatibilityRequirements(RequirementsMode =
       AspNetCompatibilityRequirementsMode.Allowed)]
    public class WCFserviceREST : IWCFserviceREST
    {

        public async Task<bool> Delete(DeleteContract contract)
        {
            using (RosterDbContext context = new RosterDbContext("DefaultConnection"))
            {
                IEnumerable<Roster> deletedPlayers = context.Rosters;

                if (!string.IsNullOrEmpty(contract.AllRosters))
                {
                    foreach (var player in deletedPlayers)
                    {
                        context.Rosters.Remove(player);
                        context.Temps.Add(player);
                    }

                    await context.SaveChangesAsync();

                    return true;
                }

                if (contract.CheckboxesRosters.Count != 0)
                {
                    foreach (string playerid in contract.CheckboxesRosters)
                    {
                        Roster player = await context.Rosters.FindAsync(playerid);
                        if (player != null)
                        {
                            context.Temps.Add(player);
                            context.Rosters.Remove(player);
                        }
                    }

                    await context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }

        public async Task<bool> Recover(RecoverContract contract)
        {
            using (RosterDbContext context = new RosterDbContext("DefaultConnection"))
            {
                IEnumerable<Temp> deletedPlayers = context.Temps;

                if (!string.IsNullOrEmpty(contract.AllTemps))
                {
                    foreach (var player in deletedPlayers)
                    {
                        context.Temps.Remove(player);
                        context.Rosters.Add(player);
                    }

                    await context.SaveChangesAsync();

                    return true;
                }

                if (contract.CheckboxesTemps.Count != 0)
                {
                    foreach (string playerid in contract.CheckboxesTemps)
                    {
                        Temp player = await context.Temps.FindAsync(playerid);
                        if (player != null)
                        {
                            context.Rosters.Add(player);
                            context.Temps.Remove(player);
                        }
                    }

                    await context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }
        
        public RosterView GetCritters()
        {
            using (RosterDbContext context = new RosterDbContext("DefaultConnection"))
            {

                RosterView model = new RosterView();
                model.Temps = context.Temps.ToList();
                model.Rosters = context.Rosters.ToList();
                model.Temps.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));
                model.Rosters.Sort((t1, t2) => (t1.Jersey ?? 0).CompareTo(t2.Jersey ?? 0));

                return model;
            }
        }
    }
}
