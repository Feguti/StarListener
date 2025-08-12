using STARListener.Converters;
using System.Text.Json.Serialization;

namespace STARListener.Models.Jira
{
    public class JiraIssueFields
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("priority")]
        public JiraPriority Priority { get; set; }

        [JsonPropertyName("customfield_10043")]
        public JiraCustomFieldCliente ? Cliente { get; set; }

        [JsonPropertyName("assignee")]
        public JiraCustomFieldResponsavel ? Responsavel { get; set; }

        [JsonPropertyName("created")]
        [JsonConverter(typeof(JiraDateTimeConverter))]
        public DateTime Created { get; set; }

    }

    public class JiraPriority
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class JiraCustomFieldCliente
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class JiraCustomFieldResponsavel
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
