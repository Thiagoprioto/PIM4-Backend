using PIM4_backend.Models;

namespace PIM4_backend.DTO
{
    public class ChamadoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descricao { get; set; }
        public string Status { get; set; } = null!;
        public DateTime DataAbertura { get; set; }
        public int UsuarioId { get; set; }
        public int? TecnicoId { get; set; }
        public string Prioridade { get; set; } = null!;
        public List<Interacao> Interacoes { get; set; } = new();
    }
}

