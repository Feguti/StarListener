using System.ComponentModel.DataAnnotations;

namespace STARListener.Models
{
    public class Cliente
    {
        [Key] // Isso indica que a propriedade Id é a chave primária da tabela
        public int Id { get; set; }

        [Required] // Isso indica que o nome não pode ser nulo ou vazio
        public string Nome { get; set; }

        public int NivelPrioridade { get; set; }
        public int NivelCriticidade { get; set; }
    }
}