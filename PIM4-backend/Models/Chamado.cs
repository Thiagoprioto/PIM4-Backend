using PIM4_backend.Models; // (Verifique se o namespace de Usuario está aqui, se for diferente)
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
        [StringLength(150)]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string Prioridade { get; set; }

        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }

        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public virtual Categoria Categoria { get; set; }

        public int IdStatus { get; set; }
        [ForeignKey("IdStatus")]
        public virtual StatusChamado StatusChamado { get; set; }

        public int IdUsuarioSolicitante { get; set; }

        [ForeignKey("IdUsuarioSolicitante")]
        public virtual Usuario UsuarioSolicitante { get; set; }
        public int? IdTecnicoResponsavel { get; set; }
        [ForeignKey("IdTecnicoResponsavel")]
        public virtual Usuario TecnicoResponsavel { get; set; }
        public virtual ICollection<Interacao> Interacoes { get; set; }
    }
}