using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.Context;
using WCF.Models;
using System.Threading.Tasks;
using System.Configuration;

namespace WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "WCFserviceREST" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы WCFserviceREST.svc или WCFserviceREST.svc.cs в обозревателе решений и начните отладку.
    public class WCFserviceREST : IWCFserviceREST
    {

        public async Task Delete(DateTime fromDate, DateTime toDate, string position, string allRosters, List<string> checkboxesRosters)
        {
            using (RosterDbContext context = new RosterDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                IEnumerable<Roster> deletedPlayers = context.Rosters;

                if (!string.IsNullOrEmpty(allRosters))
                {
                    foreach (var player in deletedPlayers)
                    {
                        context.Rosters.Remove(player);
                        context.Temps.Add(player);
                    }

                    await context.SaveChangesAsync();

                    return;
                }

                if (checkboxesRosters.Count != 0)
                {
                    foreach (string playerid in checkboxesRosters)
                    {
                        Roster player = await context.Rosters.FindAsync(playerid);
                        if (player != null)
                        {
                            context.Temps.Add(player);
                            context.Rosters.Remove(player);
                        }
                    }

                    await context.SaveChangesAsync();

                    return;
                }
            }
        }

        public async Task Recover(string allTemps, List<string> checkboxesTemps)
        {
            using (RosterDbContext context = new RosterDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                IEnumerable<Temp> deletedPlayers = context.Temps;

                if (!string.IsNullOrEmpty(allTemps))
                {
                    foreach (var player in deletedPlayers)
                    {
                        context.Temps.Remove(player);
                        context.Rosters.Add(player); // implicit operator is optional
                    }

                    await context.SaveChangesAsync();

                    return;
                }

                if (checkboxesTemps.Count != 0)
                {
                    foreach (string playerid in checkboxesTemps)
                    {
                        Temp player = await context.Temps.FindAsync(playerid);
                        if (player != null)
                        {
                            context.Rosters.Add(player); // implicit operator is optional
                            context.Temps.Remove(player);
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<RosterView> getCritters()
        {
            using (RosterDbContext context = new RosterDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
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
