namespace STARListener.Models
{
    public class IssuePriorizada
    {
        public int PrioridadeCalculada { get; set; }

        public string Chave { get; set; }
        public string Resumo { get; set; }
        public string ? NomeCliente { get; set; }
        public string UrgenciaJira { get; set; }
        public string ? Responsavel { get; set; }

        public int PontosPrioridade { get; set; }
        public int PontosCriticidade { get; set; }
        public int PontosUrgencia { get; set; }
        public int PontosData { get; set; }

    }
}
