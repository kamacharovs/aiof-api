using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IAccountTypeMap
    {
        [Required]
        [MaxLength(100)]
        string AccountName { get; set; }
        
        [Required]
        [MaxLength(100)]
        string AccountTypeName { get; set; }
        
        [Required]
        AccountType AccountType { get; set; }
    }
}