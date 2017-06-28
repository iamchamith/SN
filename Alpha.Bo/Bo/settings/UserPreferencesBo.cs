using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Bo
{
    public class UserPreferencesBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public bool State { get; set; }
        public Alpha.Bo.Enums.Enums.UserPreferencesInfo UserPreference { get; set; }
    }
}
