﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Types;

using aiof.api.data;

namespace aiof.api.services
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IAssetRepository _repo)
        {
            Name = "User";

            FieldAsync<ListGraphType<AssetTypeGraphType>>(
                "asset_type",
                resolve: async context => await _repo.GetTypesAsync()
            );
        }
    }
}
