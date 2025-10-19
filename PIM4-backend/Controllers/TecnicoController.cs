using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Models;
using PIM4_backend.Services;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TecnicoController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;

        public TecnicoController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        [HttpGet("chamados/{tecnicoId:int}")]
        public IActionResult GetChamadosDoTecnico(int tecnicoId)
        {
            var chamados = _chamadoService.GetAll()

                .Where(c => c.IdTecnicoResponsavel == tecnicoId);

            if (!chamados.Any())
                return NotFound(new { message = "Nenhum chamado atribuído a este técnico." });

            return Ok(chamados);
        }

        [HttpPut("chamados/{id:int}/status")]
        public IActionResult AtualizarStatus(int id, [FromQuery] string status)
        {
            var chamado = _chamadoService.UpdateStatus(id, status);
            if (chamado == null) return NotFound();
            return Ok(chamado);
        }

        [HttpPost("chamados/{id:int}/interacao")]
        public IActionResult AdicionarInteracao(int id, [FromBody] Interacao interacao)
        {
            var added = _chamadoService.AddInteracao(id, interacao);
            if (added == null) return NotFound();
            return Ok(added);
        }
    }
}