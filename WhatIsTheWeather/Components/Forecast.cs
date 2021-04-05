using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIsTheWeather.Components
{
    /// <summary>
    /// Forecast data capable of accepting deserialized json responses from the OpenWeatherMap Current Weather API.
    /// </summary>
    public class Forecast
    {
        /// <summary>
        /// Private macro for creating a Unix epoch DateTime component.
        /// </summary>
        /// <returns>The Unix epoch.</returns>
        private DateTime UnixEpoch() => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts unix time property, dt + timezone, to DateTime component.
        /// </summary>
        /// <returns>DateTime for weather measurement update.</returns>
        public DateTime GetTime() => UnixEpoch().AddSeconds(dt + timezone);

        /// <summary>
        /// Converts unix time property for sunrise time to DateTime component.
        /// </summary>
        /// <returns>DateTime for sunrise.</returns>
        public DateTime GetSunrise() => UnixEpoch().AddSeconds(sys.sunrise + timezone);

        /// <summary>
        /// Converts unix time property for sunset time to DateTime component.
        /// </summary>
        /// <returns>DateTime for sunset.</returns>
        public DateTime GetSunset() => UnixEpoch().AddSeconds(sys.sunset + timezone);

        /// <summary>
        /// Maps the wind direction property (in degrees) to cardinal direction based on a 16-point compass rose.
        /// </summary>
        /// <returns>Cardinal direction abbreviation.</returns>
        public string WindCardinalDirection()
        {
            string[] direction_map = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            double degrees_transformed = wind["deg"] + 11.25;
            return direction_map[(int)Math.Floor((degrees_transformed - (degrees_transformed >= 360 ? 360 : 0)) / 22.5)];
        }

        /// <summary>
        /// Macro for accessing the first element in the weather property.
        /// </summary>
        /// <returns>First element in the weather array, which is always good is object contains deserialized data.</returns>
        public WeatherDesc WeatherPrimary() => weather[0];


        public Dictionary<string, double> coord { get; set; } // Geographic coordinates
        public WeatherDesc[] weather { get; set; } // Weather description
        public string _base { get; set; } // Internal parameter for API
        public Dictionary<string, double> main { get; set; } // Atmospheric data
        public double visibility { get; set; } // Visibility in meters
        public Dictionary<string, double> wind { get; set; } // Wind speed/direction data
        public Dictionary<string, double> clouds { get; set; } // Cloud cover data
        public Dictionary<string, double> rain { get; set; } // Rainfall data
        public Dictionary<string, double> snow { get; set; } // Snowfall data
        public int dt { get; set; } // Unix time of data calculation (UTC)
        public Sys sys { get; set; } // System information
        public int timezone { get; set; } // Shift in seconds from UTC
        public int id { get; set; } // City ID
        public string name { get; set; } // City name
        public int cod { get; set; } // Internal parameter for API


        /// <summary>
        /// Nested support class that allows Forecast to pass values of different type into the sys property during deserialization.
        /// Contains descriptions of the weather, the weather condition ID, and the weather condition icon ID.
        /// </summary>
        public class WeatherDesc
        {
            public int id { get; set; } // Weather condition ID
            public string main { get; set; } // Group of weather parameters
            public string description { get; set; } // Weather condition within the group
            public string icon { get; set; } // Weather icon id
        }

        /// <summary>
        /// Nested support class that allows Forecast to pass values of different type into the sys property during deserialization.
        /// Contains internal API parameters, the country code, and the sunrise and sunset times at the location.
        /// </summary>
        public class Sys
        {
            public int type { get; set; } // Internal parameter for API
            public int id { get; set; } // Internal parameter for API
            public string country { get; set; } // Country code
            public int sunrise { get; set; } // Sunrise time (Unix time, UTC)
            public int sunset { get; set; } // Sunset time (Unix time, UTC)
        }
    }
}
