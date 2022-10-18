using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using WCF.Models;

namespace WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IWCFserviceREST" в коде и файле конфигурации.
    [ServiceContract]
    public interface IWCFserviceREST
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
    ResponseFormat = WebMessageFormat.Json,
    UriTemplate = "/Foo",
    RequestFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Wrapped)]
        string Foo();

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/Delete",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task Delete(DateTime fromDate, DateTime toDate, string position, string allRosters, List<string> checkboxesRosters);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/Recover",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task Recover(string allTemps, List<string> checkboxesTemps);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/GetCritters",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task<RosterView> GetCritters();
    }
}
