using PIM4_backend.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class Interacao
    {
        [Key]
        public int IdInteracao { get; set; }

        public string Mensagem { get; set; }
        public DateTime DataInteracao { get; set; }

        public int IdAutor { get; set; }
        [ForeignKey("IdAutor")]
        public virtual Usuario Autor { get; set; }

        public int IdChamado { get; set; }
        [ForeignKey("IdChamado")]
        public virtual Chamado Chamado { get; set; }
    }
}