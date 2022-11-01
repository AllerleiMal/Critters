using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Critters.Models;

namespace WCF
{
   
    [ServiceContract]
    public interface IWCFserviceREST
    {
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/Delete")]
        Task<bool> Delete(DeleteContract contract);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "/Recover",
            RequestFormat = WebMessageFormat.Xml)]
        Task<bool> Recover(RecoverContract contract);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "/GetCritters",
            RequestFormat = WebMessageFormat.Xml)]
        RosterView GetCritters();
    }
}
