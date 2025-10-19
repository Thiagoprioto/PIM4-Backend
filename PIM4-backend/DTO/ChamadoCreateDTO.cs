namespace PIM4_backend.DTO
{
    public class ChamadoCreateDTO
    {
        public string Titulo { get; set; } = null!;
        public string? Descricao { get; set; }
        public int UsuarioId { get; set; }
        public string Prioridade { get; set; } = "Normal";
    }
}
