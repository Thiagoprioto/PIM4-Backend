namespace PIM4_backend.DTO
{
    // O que esta API DEVOLVE para o App Mobile
    public class LoginResponseDTO
    {
        public string? Token { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Perfil { get; set; }
    }
}
