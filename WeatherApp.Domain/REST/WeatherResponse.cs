using System.Collections.Generic;
using System;

namespace WeatherApp.Domain.REST
{
    public class WeatherResponse
    {
        public List<DailyForecast> DailyForecasts { get; set; }
    }

    public class DailyForecast
    {
        public Temperature Temperature { get; set; }
        public DayPart Day { get; set; }
        public DayPart Night { get; set; }
    }

    public class Temperature
    {
        public TemperatureDetail Minimum { get; set; }
        public TemperatureDetail Maximum { get; set; }
    }

    public class TemperatureDetail
    {
        public string Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class DayPart
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
    }
}