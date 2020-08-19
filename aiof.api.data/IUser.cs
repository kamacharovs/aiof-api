using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IUser
    {
        
        [JsonIgnore]
        [Required]
        int Id { get; set; }

        [JsonIgnore]
        [Required]
        Guid PublicKey { get; set; }
        
        [Required]
        string FirstName { get; set; }
        
        [Required]
        string LastName { get; set; }
        
        [Required]
        string Email { get; set; }
        
        [Required]
        string Username { get; set; }

        [Required]
        UserProfile Profile { get; set; }

        ICollection<Asset> Assets { get; set; }
        ICollection<Goal> Goals { get; set; }
        ICollection<Liability> Liabilities { get; set; }
    }
}