using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Critters.Models;

public class DeleteConditions
{
    private IEnumerable<SelectListItem> _chosenPosition;
    
    [DataType(DataType.Date)] 
    public DateTime From { get; set; }
    
    [DataType(DataType.Date)] 
    public DateTime To { get; set; }
    
    public string Position { get; set; }
}