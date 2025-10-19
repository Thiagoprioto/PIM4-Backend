using PIM4_backend.Models;

namespace PIM4_backend.Services
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetAll();
        Usuario? GetById(int id);
        Usuario Create(Usuario user);
        Usuario? Authenticate(string email, string senha);
    }
}

