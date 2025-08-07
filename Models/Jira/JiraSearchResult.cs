using System.Text.Json.Serialization;

namespace STARListener.Models.Jira
{
    public class JiraSearchResult
    {
            [JsonPropertyName("issues")]
            public List<JiraIssue> Issues { get; set; }

    }
}
