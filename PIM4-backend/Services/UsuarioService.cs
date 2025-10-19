// Arquivo: PIM4-backend/Services/UsuarioService.cs

using System.Security.Cryptography;
using System.Text;
using PIM4_backend.Models;
using PIM4_backend.Services;

namespace PIM4_backend.Services
{
    // Serviço em memória para simular persistência.
    public class UsuarioService : IUsuarioService
    {
        private readonly List<Usuario> _usuarios = new();
        private int _nextId = 1;

        public UsuarioService()
        {
            // seed (usuários iniciais)
            Create(new Usuario { Nome = "Admin", Email = "admin@empresa.com", SenhaHash = HashPassword("admin123"), Perfil = "Admin" });
            Create(new Usuario { Nome = "Tecnico", Email = "tecnico@empresa.com", SenhaHash = HashPassword("tec123"), Perfil = "Tecnico" });
            Create(new Usuario { Nome = "Usuario Teste", Email = "usuario@empresa.com", SenhaHash = HashPassword("user123"), Perfil = "Usuario" });
        }

        public Usuario Create(Usuario user)
        {
            
            user.IdUsuario = _nextId++; // Trocado de 'Id' para 'IdUsuario'

            if (string.IsNullOrWhiteSpace(user.SenhaHash))
            {
                user.SenhaHash = HashPassword("123456");
            }
            _usuarios.Add(user);
            return user;
        }

        public IEnumerable<Usuario> GetAll() => _usuarios;

        public Usuario? GetById(int id) =>
            
            _usuarios.FirstOrDefault(u => u.IdUsuario == id); // Trocado de 'u.Id' para 'u.IdUsuario'

        public Usuario? Authenticate(string email, string senha)
        {
            var hash = HashPassword(senha);
            return _usuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.SenhaHash == hash);
        }

        private static string HashPassword(string senha)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hashed = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashed);
        }
    }
}