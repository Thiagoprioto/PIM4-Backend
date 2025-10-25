using PIM4_backend.Models; // (Verifique se o namespace de Chamado está aqui, se for diferente)
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        [StringLength(50)]
        public string Perfil { get; set; }

        public int IdDepartamento { get; set; }

        [ForeignKey("IdDepartamento")]
        public virtual Departamento Departamento { get; set; }

        public virtual ICollection<LogAtividade> LogsAtividades { get; set; }

        public virtual ICollection<Interacao> Interacoes { get; set; }

        [InverseProperty("UsuarioSolicitante")]
        public virtual ICollection<Chamado> ChamadosAbertos { get; set; }

        [InverseProperty("TecnicoResponsavel")]
        public virtual ICollection<Chamado> ChamadosAtribuidos { get; set; }
    }
}