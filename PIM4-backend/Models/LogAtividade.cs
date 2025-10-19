using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class LogAtividade
    {
        [Key]
        public int IdLog { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Acao { get; set; } = null!; // Ex: "Chamado criado", "Login", "Status alterado"

        public DateTime DataAcao { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? EnderecoIP { get; set; }
    }
}

