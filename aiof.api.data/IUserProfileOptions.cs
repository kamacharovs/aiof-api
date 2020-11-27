using System;
using System.Collections.Generic;

namespace aiof.api.data
{
    public interface IUserProfileOptions
    {
        IEnumerable<IEducationLevel> EducationLevels { get; set; }
        IEnumerable<IGender> Genders { get; set; }
        IEnumerable<IHouseholdAdult> HouseholdAdults { get; set; }
        IEnumerable<IHouseholdChild> HouseholdChildren { get; set; }
        IEnumerable<IMaritalStatus> MaritalStatuses { get; set; }
        IEnumerable<IResidentialStatus> ResidentialStatuses { get; set; }
    }
}