using PIM4_backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIM4_backend.Models
{
    public class StatusChamado
    {
        [Key]
        public int IdStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(255)]
        public string Descricao { get; set; }
        public virtual ICollection<Chamado> Chamados { get; set; }
    }
}

