﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class AssetTypeGraphType : ObjectGraphType<AssetType>, IGraphType
    {
        public AssetTypeGraphType()
        {
            Field(x => x.Name);
            Field(x => x.PublicKey);
        }
    }
}
