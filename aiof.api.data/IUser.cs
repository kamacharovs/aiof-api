using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public interface IUser
    {
        [JsonIgnore] int Id { get; set; }
        [JsonIgnore] Guid PublicKey { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        ICollection<Asset> Assets { get; set; }
        ICollection<Goal> Goals { get; set; }
        ICollection<Liability> Liabilities { get; set; }
    }
}