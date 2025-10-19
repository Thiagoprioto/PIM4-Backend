using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class Chamado
    {
        [Key]
        public int IdChamado { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = null!;

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public string Prioridade { get; set; } = "Normal"; // Baixa, Normal, Alta

        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
        public DateTime? DataFechamento { get; set; }

        // Categoria
        [ForeignKey(nameof(Categoria))]
        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }

        // Usuário solicitante
        [ForeignKey(nameof(UsuarioSolicitante))]
        public int IdUsuarioSolicitante { get; set; }
        public Usuario? UsuarioSolicitante { get; set; }

        // Técnico responsável
        [ForeignKey(nameof(TecnicoResponsavel))]
        public int? IdTecnicoResponsavel { get; set; }
        public Usuario? TecnicoResponsavel { get; set; }

        // Status
        [ForeignKey(nameof(StatusChamado))]
        public int IdStatus { get; set; }
        public StatusChamado? StatusChamado { get; set; }

        // Interações (se existir)
        public ICollection<Interacao>? Interacoes { get; set; }

        // IA (um chamado pode ter várias respostas IA)
        public ICollection<RespostaIA>? RespostasIA { get; set; }
    }
}
