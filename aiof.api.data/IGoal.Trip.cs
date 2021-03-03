using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IGoalTrip
    {
        [Required]
        [MaxLength(300)]
        string Destination { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        GoalTripType TripType { get; set; }

        [Required]
        double Duration { get; set; }

        [Required]
        int Travelers { get; set; }

        decimal? Flight { get; set; }
        decimal? Hotel { get; set; }
        decimal? Car { get; set; }
        decimal? Food { get; set; }
        decimal? Activities { get; set; }
        decimal? Other { get; set; }
    }
}