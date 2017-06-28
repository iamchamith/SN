using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpha.Bo.Enums;

namespace Alpha.Poco
{
    [Table("UserPreferences")]
    public class UserPreferences : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Enums.UserPreferencesInfo UserPreference { get; set; }
        public bool State { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
