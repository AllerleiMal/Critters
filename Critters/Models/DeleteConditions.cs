using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Critters.Models;

public class DeleteConditions
{
    private IEnumerable<SelectListItem> _chosenPosition;
    [DataType(DataType.Date)] public DateTime From { get; set; }
    [DataType(DataType.Date)] public DateTime To { get; set; }
    public string Position { get; set; }

    // public IEnumerable<SelectListItem> ChosenPosition
    // {
    //     get => _chosenPosition;
    //     set
    //     {
    //         _chosenPosition = value;
    //         Position = value.GetEnumerator().Current.Value;
    //     }
    // }
}