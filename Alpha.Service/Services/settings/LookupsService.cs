using Alpha.Bo.Bo;
using Alpha.Bo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Services.settings
{
    public class LookupsService : BaseService
    {
        public static List<DropDownBo> Countries()
        {
            var countries = new List<DropDownBo>();
            foreach (Enums.Countries val in Enum.GetValues(typeof(Enums.Countries)))
            {
                if (val == Enums.Countries.Not_Selected)
                {
                    continue;
                }
                countries.Add(new DropDownBo
                {
                    Value = ((int)val).ToString(),
                    Text = val.ToString().Replace('_', ' ')
                });
            }
            countries.Insert(0, new DropDownBo
            {
                Value = "-1",
                Text = "Not mention"
            });
            return countries;
        }
        public static List<DropDownBo> Languages()
        {
            var languages = new List<DropDownBo>();
            foreach (Enums.Languages val in Enum.GetValues(typeof(Enums.Languages)))
            {
                languages.Add(new DropDownBo
                {
                    Value = ((int)val).ToString(),
                    Text = val.ToString()
                });
            }
            return languages;
        }
        public static List<DropDownBo> Months()
        {
            var months = new List<DropDownBo>();
            foreach (Enums.Months val in Enum.GetValues(typeof(Enums.Months)))
            {
                months.Add(new DropDownBo
                {
                    Value = ((int)val).ToString(),
                    Text = val.ToString()
                });
            }
            return months;
        }
        public static List<DropDownBo> MaritalStatus()
        {
            var maritalStatus = new List<DropDownBo>();
            foreach (Enums.MaritalStatus val in Enum.GetValues(typeof(Enums.MaritalStatus)))
            {
                if (val == Enums.MaritalStatus.Not_selected)
                {
                    continue;
                }
                maritalStatus.Add(new DropDownBo
                {
                    Value = ((int)val).ToString(),
                    Text = val.ToString().Replace('_', ' ')
                });
            }
            maritalStatus.Insert(0, new DropDownBo
            {
                Value = "-1",
                Text = "Not mention"
            });
            return maritalStatus;
        }
        public static List<DropDownBo> Gender()
        {
            var gender = new List<DropDownBo>();
            foreach (Enums.Gender val in Enum.GetValues(typeof(Enums.Gender)))
            {
                if (val == Enums.Gender.None)
                {
                    continue;
                }
                gender.Add(new DropDownBo
                {
                    Value = ((int)val).ToString(),
                    Text = val.ToString()
                });
            }
            gender.Insert(0, new DropDownBo
            {
                Value = "-1",
                Text = "Not mention"
            });
            return gender;
        }

        public static List<int> SocialNetworoksShowValues()
        {
            return new List<int>()
            {
                (int)Enums.SocialNetworks.Hotmail,
                (int)Enums.SocialNetworks.Gmail,
                (int)Enums.SocialNetworks.Yandex,
                (int)Enums.SocialNetworks.Skype,
                (int)Enums.SocialNetworks.Phone_Number,
                (int)Enums.SocialNetworks.Ymail
            };
        }
    }
}
