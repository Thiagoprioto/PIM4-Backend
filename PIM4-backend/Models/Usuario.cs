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
        [MaxLength(150)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = null!;

        [Required]
        public string SenhaHash { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Perfil { get; set; } = "Usuario"; // Admin, Tecnico, Usuario

        // (opcional) Departamento
        [ForeignKey(nameof(Departamento))]
        public int? IdDepartamento { get; set; }
        public Departamento? Departamento { get; set; }

        // Relacionamentos
        public ICollection<Chamado>? ChamadosAbertos { get; set; }         // como solicitante
        public ICollection<Chamado>? ChamadosAtribuidos { get; set; }     // como técnico
        public ICollection<Interacao>? Interacoes { get; set; }
        public ICollection<LogAtividade>? LogsAtividades { get; set; }
    }
}
