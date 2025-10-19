using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Models;
using PIM4_backend.Services;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IChamadoService _chamadoService;
        private readonly IFAQService _faqService;

        public AdminController(IUsuarioService usuarioService, IChamadoService chamadoService, IFAQService faqService)
        {
            _usuarioService = usuarioService;
            _chamadoService = chamadoService;
            _faqService = faqService;
        }

        [HttpGet("usuarios")]
        public IActionResult GetUsuarios()
        {
            var users = _usuarioService.GetAll();
            return Ok(users);
        }

        [HttpDelete("usuarios/{id:int}")]
        public IActionResult DeleteUsuario(int id)
        {
            var user = _usuarioService.GetById(id);
            if (user == null) return NotFound();
            return Ok(new { message = $"Usuário {user.Nome} removido (simulação)." });
        }

        [HttpDelete("chamados/{id:int}")]
        public IActionResult DeleteChamado(int id)
        {
            var success = _chamadoService.Delete(id);
            if (!success) return NotFound();
            return Ok(new { message = "Chamado removido (simulação)." });
        }

        [HttpPost("faq")]
        public IActionResult AddFAQ([FromBody] FAQ faq)
        {
            // Simulação de persistência
            return Ok(new { message = "FAQ adicionada (simulação).", faq });
        }

        [HttpDelete("faq/{id:int}")]
        public IActionResult DeleteFAQ(int id)
        {
            return Ok(new { message = $"FAQ {id} removida (simulação)." });
        }
    }
}
