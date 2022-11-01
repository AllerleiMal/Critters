using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Critters.Models
{
    public class DeleteContract
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Position { get; set; }
        public string AllRosters { get; set; }
        public List<string> CheckboxesRosters  { get; set; }
    }
}