using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class GoalTrip : Goal, 
        IGoalTrip
    {
        [Required]
        [MaxLength(300)]
        public string Destination { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public GoalTripType TripType { get; set; }

        [Required]
        public double Duration { get; set; } = 14;

        [Required]
        public int Travelers { get; set; } = 1;

        public decimal? Flight { get; set; }
        public decimal? Hotel { get; set; }
        public decimal? Car { get; set; }
        public decimal? Food { get; set; }
        public decimal? Activities { get; set; }
        public decimal? Other { get; set; }
    }

    public class GoalTripDto : GoalDto
    {
        public string Destination { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public GoalTripType TripType { get; set; }

        public double? Duration { get; set; }
        public int? Travelers { get; set; }
        public decimal? Flight { get; set; }
        public decimal? Hotel { get; set; }
        public decimal? Car { get; set; }
        public decimal? Food { get; set; }
        public decimal? Activities { get; set; }
        public decimal? Other { get; set; }
    }
}
