using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public class User : IUser, 
        IPublicKeyId
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public Guid PublicKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public ICollection<Asset> Assets { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Liability> Liabilities { get; set; }
    }

    public class UserDto
    {
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Liability> Liabilities { get; set; }
    }
}
