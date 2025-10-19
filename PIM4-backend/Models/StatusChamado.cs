using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIM4_backend.Models
{
    public class StatusChamado
    {
        [Key]
        public int IdStatus { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!; // Ex: "Aberto", "Em andamento", "Fechado"

        [MaxLength(255)]
        public string? Descricao { get; set; }

        // Relacionamento com Chamados
        public ICollection<Chamado>? Chamados { get; set; }
    }
}

