using PIM4_backend.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class RespostaIA
    {
        [Key]
        public int IdRespostaIA { get; set; }

        public string Mensagem { get; set; }
        public string Modelo { get; set; }
        public DateTime DataGeracao { get; set; }
        public string IdExecucaoN8N { get; set; } 
        public int IdChamado { get; set; }
        [ForeignKey("IdChamado")]
        public virtual Chamado Chamado { get; set; }
    }
}