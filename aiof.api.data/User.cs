using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class User : IUser, 
        IPublicKeyId
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public UserProfile Profile { get; set; }

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
