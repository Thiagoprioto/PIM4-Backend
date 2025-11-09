namespace PIM4_backend.DTO
{
    // O que o App Mobile ENVIA para o /login
    public class LoginRequestDTO
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }

}
