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

        public async Task<IEnumerable<JiraIssue>> GetIssuesAsync(string responsavel = null)
        {

            var jqlBuilder = new System.Text.StringBuilder("status != Done");

            //LEMBRAR: O operador '~' não é suportado pelo campo assignee (validei no postman, spoiler: odeio o Jira), que é o campo que traz o responsável
            //A busca retorna apenas nomes exatos por enquanto 
            if (!string.IsNullOrWhiteSpace(responsavel))
            {
                jqlBuilder.Append($" AND assignee = '{responsavel}'");
            }

            var jql = jqlBuilder.ToString();
            var fields = "summary,priority,customfield_10043, assignee, created"; 
            var requestUrl = $"/rest/api/3/search?jql={Uri.EscapeDataString(jql)}&fields={fields}";

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
