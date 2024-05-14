using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

namespace LowCostFlights.Server.Converters
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal },
                ParseStringConverter.Singleton
                },
        };
    }
}
