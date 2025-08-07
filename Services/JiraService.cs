using System.Text.Json;
using STARListener.Models.Jira;

namespace STARListener.Services
{
    public class JiraService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JiraService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<JiraIssue>> GetIssuesAsync()
        {
            var jql = "status != Done";
            var fields = "summary,priority,customfield_10043, assignee"; 
            var requestUrl = $"/rest/api/3/search?jql={jql}&fields={fields}";

            var httpClient = _httpClientFactory.CreateClient("Jira");
            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var searchResult = JsonSerializer.Deserialize<JiraSearchResult>(jsonResponse);
                return searchResult?.Issues ?? Enumerable.Empty<JiraIssue>();
            }

            return Enumerable.Empty<JiraIssue>();
        }


    }
}
