using System.ComponentModel.DataAnnotations;

namespace PIM4_backend.Models
{
    public class FAQ
    {
        [Key]
        public int IdFaq { get; set; }

        [Required]
        [MaxLength(300)]
        public string Pergunta { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Resposta { get; set; } = null!;
    }
}
