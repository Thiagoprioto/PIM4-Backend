using PIM4_backend.Models;
using PIM4_backend.Services;
using Microsoft.AspNetCore.Mvc;
using PIM4_backend.DTO;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> GetAll()
        {
            var users = _usuarioService.GetAll().Select(u => new UsuarioDTO { Id = u.IdUsuario, Nome = u.Nome, Email = u.Email, Perfil = u.Perfil });
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UsuarioDTO> GetById(int id)
        {
            var u = _usuarioService.GetById(id);
            if (u == null) return NotFound();
            return Ok(new UsuarioDTO { Id = u.IdUsuario, Nome = u.Nome, Email = u.Email, Perfil = u.Perfil });
        }

        [HttpPost]
        public ActionResult Create([FromBody] Usuario usuario)
        {
            var created = _usuarioService.Create(usuario);
            return CreatedAtAction(nameof(GetById), new { id = created.IdUsuario }, new UsuarioDTO { Id = created.IdUsuario, Nome = created.Nome, Email = created.Email, Perfil = created.Perfil });
        }

        [HttpGet("por-perfil/{perfil}")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetByPerfil(string perfil)
        {
            var users = _usuarioService.GetAll()
                .Where(u => u.Perfil.Equals(perfil, StringComparison.OrdinalIgnoreCase))
                .Select(u => new UsuarioDTO { Id = u.IdUsuario, Nome = u.Nome, Email = u.Email, Perfil = u.Perfil });

            if (!users.Any()) return NotFound(new { message = "Nenhum usuário encontrado com esse perfil." });

            return Ok(users);
        }
    }
}
