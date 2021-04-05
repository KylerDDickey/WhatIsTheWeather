using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WhatIsTheWeather.Components
{
    /// <summary>
    /// Input form component that stores user's desired query arguments for the OpenWeatherMap Current Weather API.
    /// </summary>
    public class FormData
    {
        [Required]
        [StringLength(128, ErrorMessage = "Zipcode too long (128 character limit).")]
        public string CityOrZip { get; set; } // City name or zip code.

        [Required]
        public string CountryCode { get; set; } // Alpha-2 country code.

        public string StateCode { get; set; } // US state abbreviation.

        [Required]
        public string UnitType { get; set; } = "metric"; // Unit type specification.
    }
}
