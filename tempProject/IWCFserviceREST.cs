using Critters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace tempProject
{

    [ServiceContract]
    public interface IWCFserviceREST
    {
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/Delete",
            BodyStyle = WebMessageBodyStyle.Bare)]
        Task<bool> Delete(DeleteContract contract);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "/Recover",
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Task<bool> Recover(RecoverContract contract);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "/GetCritters",
            RequestFormat = WebMessageFormat.Xml)]
        RosterView GetCritters();
    }
}
