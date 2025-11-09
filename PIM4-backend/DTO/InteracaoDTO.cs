namespace PIM4_backend.DTO
{
    public class InteracaoDTO
    {
        public int IdInteracao { get; set; }
        public int IdChamado { get; set; }
        public int IdAutor { get; set; }
        public string? Mensagem { get; set; }
        public DateTime DataInteracao { get; set; }
    }
}