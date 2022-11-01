using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Critters.Models
{
    public class RecoverContract
    {
        public string AllTemps { get; set; }
        public List<string> CheckboxesTemps { get; set; }
    }
}