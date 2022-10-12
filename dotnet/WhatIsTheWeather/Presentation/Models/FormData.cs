using System.ComponentModel.DataAnnotations;

namespace WhatIsTheWeather.Presentation.Models
{
    /// <summary>
    /// Input form component that stores user's desired query arguments for the OpenWeatherMap Current Weather API.
    /// </summary>
    public class FormData
    {
        [Required]
        [StringLength(128, ErrorMessage = "City name or Zip Code too long (128 character limit).")]
        public string CityOrZip { get; set; } // City name or zip code.

        [Required]
        public string CountryCode { get; set; } // Alpha-2 country code.

        public string StateCode { get; set; } // US state abbreviation.

        [Required]
        public string UnitType { get; set; } = "metric"; // Unit type specification.
    }
}
