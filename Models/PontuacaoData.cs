using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STARListener.Models
{
    public class PontuacaoData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DeDiasAtras { get; set; }

        public int AteDiasAtras { get; set; }

        public int Pontos { get; set; }
    }
}
