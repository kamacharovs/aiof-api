using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class AccountTypeMap : IAccountTypeMap
    {
        [Required]
        [MaxLength(100)]
        public string AccountName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AccountTypeName { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
    }
}