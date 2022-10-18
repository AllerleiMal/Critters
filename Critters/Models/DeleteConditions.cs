using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Critters.Models;

[DataContract]
public class DeleteConditions
{
    [DataMember]
    private IEnumerable<SelectListItem> _chosenPosition;

    [DataMember]
    [DataType(DataType.Date)] public DateTime From { get; set; }

    [DataMember]
    [DataType(DataType.Date)] public DateTime To { get; set; }

    [DataMember]
    public string Position { get; set; }
}