using System;

namespace PIM4_backend.DTO // (Seu namespace é DTO singular)
{
    public class ChamadoDTO
    {
        public int IdChamado { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public int Prioridade { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int? IdCategoria { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public int? IdTecnicoResponsavel { get; set; }
        public int? IdStatus { get; set; }
    }
}