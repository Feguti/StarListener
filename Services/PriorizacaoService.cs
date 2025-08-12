using Microsoft.EntityFrameworkCore;
using STARListener.Api.Data;
using STARListener.Models;

namespace STARListener.Services
{
    public class PriorizacaoService
    {
        private readonly JiraService _jiraService;
        private readonly ApplicationDbContext _context;

        public PriorizacaoService(JiraService jiraService, ApplicationDbContext context)
        {
            _jiraService = jiraService;
            _context = context;
        }

        public async Task<IEnumerable<IssuePriorizada>> CalcularPrioridadesAsync(string responsavel = null)
        {

            var issuesDoJira = await _jiraService.GetIssuesAsync(responsavel);
            var clientesDb = await _context.Clientes.ToListAsync();
            var pontuacoesPrioridade = await _context.PontuacoesPrioridade.ToDictionaryAsync(p => p.Nivel, p => p.Pontos);
            var pontuacoesCriticidade = await _context.PontuacoesCriticidade.ToDictionaryAsync(p => p.Nivel, p => p.Pontos);

            var pontosUrgenciaJira = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                //Prioridades do meu projeto de teste StarListener
                { "Highest", 5 },
                { "High", 4 },
                { "Medium", 3 },
                { "Low", 2 },
                { "Lowest", 1 },

                //Prioridades utilizadas na DDS (Só são utilizadas se a URL registrada for do projeto que a DDS utiliza no Jira)
                { "Urgente", 5 },
                { "Crítico", 4 },
                { "Alta", 3 },
                { "Média", 2 },
                { "Baixa", 1 }

            };

            var regrasData = await _context.PontuacoesData.OrderByDescending(r => r.Pontos).ToListAsync();
            var hoje = DateTime.UtcNow.Date;

            var listaPriorizada = new List<IssuePriorizada>();

            //Vê todas as issues do projeto
            foreach (var issue in issuesDoJira)
            {
                var nomeCliente = issue.Fields.Cliente?.Value;
                var cliente = clientesDb.FirstOrDefault(c => c.Nome.Equals(nomeCliente, StringComparison.OrdinalIgnoreCase));

                int pontosPrioridade = 0;
                int pontosCriticidade = 0;
                
                if (cliente != null)
                {
                    pontuacoesPrioridade.TryGetValue(cliente.NivelPrioridade, out int pP);
                    pontuacoesCriticidade.TryGetValue(cliente.NivelCriticidade, out int pC);

                    pontosPrioridade = pP;
                    pontosCriticidade = pC;
                    
                }

                pontosUrgenciaJira.TryGetValue(issue.Fields.Priority.Name, out int pontosUrgencia);

                var idadeEmDias = (int)(hoje - issue.Fields.Created.Date).TotalDays;

                var regraDataFiltro = regrasData.FirstOrDefault(r =>idadeEmDias >= r.DeDiasAtras && idadeEmDias <= r.AteDiasAtras);

                int pontosData = regraDataFiltro?.Pontos ?? 0;

                // Monta o objeto de resposta final
                var issuePriorizada = new IssuePriorizada
                {
                    Chave = issue.Key,
                    Resumo = issue.Fields.Summary,
                    NomeCliente = nomeCliente ?? "Cliente não definido",
                    UrgenciaJira = issue.Fields.Priority.Name,
                    Responsavel = issue.Fields.Responsavel?.DisplayName ?? "Responsável não definido",
                    PontosPrioridade = pontosPrioridade,
                    PontosCriticidade = pontosCriticidade,
                    PontosUrgencia = pontosUrgencia,
                    PontosData = pontosData,
                    PrioridadeCalculada = (pontosPrioridade +  pontosCriticidade) + (pontosUrgencia + pontosData)
                };

                listaPriorizada.Add(issuePriorizada);
            }

            return listaPriorizada.OrderByDescending(i => i.PrioridadeCalculada);
        }

    }
}
