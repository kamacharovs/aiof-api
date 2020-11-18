using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class UserProfileGraphType : ObjectGraphType<UserProfile>, IGraphType
    {
        public UserProfileGraphType()
        {
            Field(x => x.Gender);
            Field(x => x.DateOfBirth, nullable: true);
            Field(x => x.Age, nullable: true);
            Field(x => x.Occupation);
            Field(x => x.OccupationIndustry);
            Field(x => x.GrossSalary, nullable: true);
            Field(x => x.MaritalStatus);
            Field(x => x.EducationLevel);
            Field(x => x.ResidentialStatus);
            Field(x => x.HouseholdIncome, nullable: true);
            Field(x => x.HouseholdAdults, nullable: true);
            Field(x => x.HouseholdChildren, nullable: true);
            Field(x => x.RetirementContributionsPreTax, nullable: true);
        }
    }
}
