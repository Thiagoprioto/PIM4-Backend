using System;

namespace PIM4_backend.Models.DTOs
{
    public class LogAtividadeDTO
    {
        public long IdLog { get; set; }
        public int IdUsuario { get; set; }
        public string Acao { get; set; }
        public DateTime DataAcao { get; set; }
        public string EnderecoIP { get; set; }
    }
}