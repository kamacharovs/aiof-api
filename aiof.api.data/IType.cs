using System;

namespace aiof.api.data
{
    public interface IType
    {
        string Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}