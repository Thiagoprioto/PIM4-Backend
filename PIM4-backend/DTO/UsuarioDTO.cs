public class UsuarioDTO
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
    public string Perfil { get; set; }
    public int? IdDepartamento { get; set; }
}
