using System.ComponentModel.DataAnnotations;

namespace PIM4_backend.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [MaxLength(255)]
        public string? Descricao { get; set; }

        public ICollection<Chamado>? Chamados { get; set; }
    }
}