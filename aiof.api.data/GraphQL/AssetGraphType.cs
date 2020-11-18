using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;
using GraphQL.Authorization;

namespace aiof.api.data
{
    public class AssetGraphType : ObjectGraphType<Asset>, IGraphType
    {
        public AssetGraphType()
        {
            Field(x => x.Id);
            Field(x => x.PublicKey);
            Field(x => x.Name);
            Field(x => x.TypeName);
            Field(x => x.Value);
            Field(x => x.UserId);
            Field(x => x.IsDeleted);

            Field<AssetTypeGraphType>("type", resolve: context => context.Source.Type);
        }
    }
}
