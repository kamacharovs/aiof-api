using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IGoalType
    {
        [Required]
        [MaxLength(100)]
        string Name { get; set; }

        [Required]
        Guid PublicKey { get; set; }
    }
}