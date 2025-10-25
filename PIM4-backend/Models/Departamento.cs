using PIM4_backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIM4_backend.Models
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(255)]
        public string Descricao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}