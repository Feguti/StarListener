using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STARListener.Models
{
    public class PontuacaoPrioridade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Nivel { get; set; } // Nível 0, 1, 2...

        public int Pontos { get; set; }// Pontos 10, 9, 8...
    }
}