using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IHouseholdChild
    {
        [Required]
        [MaxLength(100)]
        string Name { get; set; }

        [Required]
        Guid PublicKey { get; set; }
    }
}