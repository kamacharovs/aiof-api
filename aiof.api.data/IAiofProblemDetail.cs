using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IAiofProblemDetail
    {
        [JsonPropertyName("code")]
        [Required]
        int? Code { get; set; }
        
        [JsonPropertyName("message")]
        [Required]
        string Message { get; set; }

        [JsonPropertyName("traceId")]
        [Required]
        string TraceId { get; set; }

        [JsonPropertyName("errors")]
        IEnumerable<string> Errors { get; set; }
    }

    public interface IAiofProblemDetailBase
    {
        [JsonPropertyName("code")]
        [Required]
        int? Code { get; set; }
        
        [JsonPropertyName("message")]
        [Required]
        string Message { get; set; }
    }
}