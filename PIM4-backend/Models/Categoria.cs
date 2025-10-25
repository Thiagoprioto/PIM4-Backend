using PIM4_backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIM4_backend.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(255)]
        public string Descricao { get; set; }
        public virtual ICollection<Chamado> Chamados { get; set; }
    }
}