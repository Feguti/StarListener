using System.Text.Json.Serialization;

namespace STARListener.Models.Jira
{
    public class JiraIssue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("fields")]
        public JiraIssueFields Fields { get; set; }
    }
}
