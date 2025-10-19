using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class Interacao
    {
        [Key]
        public int IdInteracao { get; set; }

        // Chamado relacionado
        [ForeignKey(nameof(Chamado))]
        public int IdChamado { get; set; }
        public Chamado? Chamado { get; set; }

        // Autor (usuário ou técnico)
        [ForeignKey(nameof(Autor))]
        public int IdAutor { get; set; }
        public Usuario? Autor { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Mensagem { get; set; } = null!;

        public DateTime DataInteracao { get; set; } = DateTime.UtcNow;
    }
}
