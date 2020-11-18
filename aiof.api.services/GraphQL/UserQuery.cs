using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Types;

using aiof.api.data.graphql;

namespace aiof.api.services
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(
            IAssetRepository _assetRepo,
            ILiabilityRepository _liabilityRepo,
            IGoalRepository _goalRepo,
            IUserRepository _userRepo)
        {
            Name = "UserQuery";

            FieldAsync<ListGraphType<AssetTypeGraphType>>(
                "asset_types",
                resolve: async context => await _assetRepo.GetTypesAsync()
            );

            FieldAsync<AssetGraphType>(
                "asset",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<int?>("id");

                    return await _assetRepo.GetAsync((int)id);
                });


            FieldAsync<ListGraphType<LiabilityTypeGraphType>>(
                "liability_types",
                resolve: async context => await _liabilityRepo.GetTypesAsync()
            );
            FieldAsync<LiabilityGraphType>(
                "liability",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<int?>("id");

                    return await _liabilityRepo.GetAsync((int)id);
                });
            FieldAsync<ListGraphType<LiabilityGraphType>>(
                "liabilities",
                resolve: async context => await _liabilityRepo.GetAllAsync()
            );


            FieldAsync<ListGraphType<GoalTypeGraphType>>(
                "goal_types",
                resolve: async context => await _goalRepo.GetTypesAsync()
            );

            FieldAsync<GoalGraphType>(
                "goal",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<int?>("id");

                    return await _goalRepo.GetAsync((int)id);
                });


            FieldAsync<UserGraphType>(
                "user",
                resolve: async context => await _userRepo.GetAsync()
            );
        }
    }
}
