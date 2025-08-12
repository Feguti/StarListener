using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace STARListener.Converters
{
    public class JiraDateTimeConverter : JsonConverter<DateTime>
    {
        //Por isso eu odeio o Jira
        private const string JiraDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffzzzz";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (string.IsNullOrEmpty(dateString))
            {
                return default;
            }

            if (DateTimeOffset.TryParseExact(dateString, JiraDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date.UtcDateTime;
            }

            return DateTime.Parse(dateString);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("o", CultureInfo.InvariantCulture));
        }
    }
}
