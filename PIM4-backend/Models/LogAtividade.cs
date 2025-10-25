using PIM4_backend.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4_backend.Models
{
    public class LogAtividade
    {
        [Key]
        public long IdLog { get; set; }

        [Required]
        [StringLength(100)]
        public string Acao { get; set; }

        public DateTime DataAcao { get; set; }

        [StringLength(50)]
        public string EnderecoIP { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}