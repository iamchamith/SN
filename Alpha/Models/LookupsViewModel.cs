using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Models
{
    public class LookupsViewModel
    {
        public List<DropdownViewModel> Countries { get; set; }
        public List<DropdownViewModel> Genders { get; set; }
        public List<DropdownViewModel> Status { get; set; }
        public int Country { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
    }
}