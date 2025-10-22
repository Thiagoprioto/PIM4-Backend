using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class RespostaIA
    {
        [Key]
        public int IdRespostaIA { get; set; }

        [ForeignKey(nameof(Chamado))]
        public int IdChamado { get; set; }
        public Chamado? Chamado { get; set; }

        [Required]
        public string Mensagem { get; set; } = null!;

        [MaxLength(100)]
        public string? Modelo { get; set; } // Ex: GPT-4, IA Suporte, etc.

        public DateTime DataGeracao { get; set; } = DateTime.UtcNow;

        // Pode ser usado para guardar ID da execução no N8N
        [MaxLength(200)]
        public string? IdExecucaoN8N { get; set; }
    }
}
