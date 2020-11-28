using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiof.api.data
{
    public class UserProfileOptions : IUserProfileOptions
    {
        public IEnumerable<IEducationLevel> EducationLevels { get; set; }
        public IEnumerable<IMaritalStatus> MaritalStatuses { get; set; }
        public IEnumerable<IResidentialStatus> ResidentialStatuses { get; set; }
        public IEnumerable<IGender> Genders { get; set; }
        public IEnumerable<IHouseholdAdult> HouseholdAdults { get; set; }
        public IEnumerable<IHouseholdChild> HouseholdChildren { get; set; }
    }
}
