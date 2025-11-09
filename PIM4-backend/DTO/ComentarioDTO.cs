namespace PIM4_backend.DTO
{
    public class ComentarioDTO
    {
        // O ID do chamado que o técnico está comentando
        public int IdChamado { get; set; }

        // A mensagem que o técnico escreveu
        public string? Mensagem { get; set; }
    }
}
