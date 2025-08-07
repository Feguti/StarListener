using System.ComponentModel.DataAnnotations;

namespace STARListener.Models
{
    public class Cliente
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public string Nome { get; set; }

        public int NivelPrioridade { get; set; }
        public int NivelCriticidade { get; set; }
    }
}