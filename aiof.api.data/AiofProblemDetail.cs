using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class AiofProblemDetail : IAiofProblemDetail
    {
        [JsonPropertyName("code")]
        [Required]
        public int? Code { get; set; }
        
        [JsonPropertyName("message")]
        [Required]
        public string Message { get; set; }

        [JsonPropertyName("traceId")]
        [Required]
        public string TraceId { get; set; }

        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; set; }
    }

    public class AiofProblemDetailBase : IAiofProblemDetailBase
    {
        [JsonPropertyName("code")]
        [Required]
        public int? Code { get; set; }
        
        [JsonPropertyName("message")]
        [Required]
        public string Message { get; set; }
    }
}