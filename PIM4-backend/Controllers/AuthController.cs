

using PIM4_backend.DTO;
using PIM4_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _usuarioService.Authenticate(dto.Email, dto.Senha);
            if (user == null) return Unauthorized(new { message = "Credenciais inválidas" });

            // Geramos um token fictício (GUID).
            var token = Guid.NewGuid().ToString();

            return Ok(new
            {
                token,

                
                usuario = new { user.IdUsuario, user.Nome, user.Email, user.Perfil } 
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] PIM4_backend.Models.Usuario usuario)
        {
            var created = _usuarioService.Create(usuario);

            
            return CreatedAtAction(nameof(Register),
                new { id = created.IdUsuario }, 
                new { created.IdUsuario, created.Nome, created.Email, created.Perfil }); 
        }
    }
}