using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace WCF.Models
{
    [DataContract]
    public class DeleteConditions
    {
        [DataMember]
        IEnumerable<SelectListItem> _chosenPosition;

        [DataMember]
        [DataType(DataType.Date)] public DateTime From { get; set; }

        [DataMember]
        [DataType(DataType.Date)] public DateTime To { get; set; }

        [DataMember]
        public string Position { get; set; }
    }
}