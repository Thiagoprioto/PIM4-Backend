using PIM4_backend.DTO;
using PIM4_backend.Models;
using PIM4_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;
        private readonly IUsuarioService _usuarioService;

        public ChamadosController(IChamadoService chamadoService, IUsuarioService usuarioService)
        {
            _chamadoService = chamadoService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChamadoDTO>> GetAll()
        {
            var list = _chamadoService.GetAll().Select(c => ToDTO(c));
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ChamadoDTO> GetById(int id)
        {
            var c = _chamadoService.GetById(id);
            if (c == null) return NotFound();
            return Ok(ToDTO(c));
        }

        [HttpPost]
        public ActionResult<ChamadoDTO> Create(ChamadoCreateDTO dto)
        {
            // valida usuário (usando o IdUsuario do model)
            var usuario = _usuarioService.GetById(dto.UsuarioId);
            if (usuario == null) return BadRequest(new { message = "UsuarioId inválido" });

            var chamado = new Chamado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,

                
                IdUsuarioSolicitante = dto.UsuarioId,

                Prioridade = dto.Prioridade,

                IdStatus = 1, // Assumindo 1 = "Aberto"

                DataAbertura = DateTime.UtcNow
            };

            var created = _chamadoService.Create(chamado);

            return CreatedAtAction(nameof(GetById), new { id = created.IdChamado }, ToDTO(created));
        }

        [HttpPut("{id:int}/status")]
        public ActionResult<ChamadoDTO> UpdateStatus(int id, [FromQuery] string status)
        {
            var updated = _chamadoService.UpdateStatus(id, status);
            if (updated == null) return NotFound();
            return Ok(ToDTO(updated));
        }

        [HttpPut("{id:int}/assign")]
        public ActionResult<ChamadoDTO> AssignTecnico(int id, [FromQuery] int tecnicoId)
        {
            var tecnico = _usuarioService.GetById(tecnicoId);
            if (tecnico == null) return BadRequest(new { message = "TecnicoId inválido" });
            var updated = _chamadoService.AssignTecnico(id, tecnicoId);
            if (updated == null) return NotFound();
            return Ok(ToDTO(updated));
        }

        [HttpPost("{id:int}/interacoes")]
        public ActionResult AddInteracao(int id, [FromBody] Interacao interacao)
        {
            var added = _chamadoService.AddInteracao(id, interacao);
            if (added == null) return NotFound();
            return Ok(added);
        }

        private static ChamadoDTO ToDTO(Chamado c)
        {
            return new ChamadoDTO
            {
                Id = c.IdChamado,
                Titulo = c.Titulo,
                Descricao = c.Descricao,
                Status = ConvertStatusToString(c.IdStatus),
                DataAbertura = c.DataAbertura,
                UsuarioId = c.IdUsuarioSolicitante,
                TecnicoId = c.IdTecnicoResponsavel,
                Prioridade = c.Prioridade,

                Interacoes = c.Interacoes?.ToList()
            };
        }

        private static string ConvertStatusToString(int idStatus)
        {
            switch (idStatus)
            {
                case 1: return "Aberto";
                case 2: return "EmAndamento";
                case 3: return "Fechado";
                default: return "Desconhecido";
            }
        }


        [HttpGet("usuario/{usuarioId:int}")]
        public ActionResult<IEnumerable<ChamadoDTO>> GetByUsuario(int usuarioId)
        {
            var list = _chamadoService.GetAll()

                .Where(c => c.IdUsuarioSolicitante == usuarioId)
                .Select(c => ToDTO(c));

            if (!list.Any()) return NotFound(new { message = "Nenhum chamado encontrado para este usuário." });
            return Ok(list);
        }

        [HttpGet("tecnico/{tecnicoId:int}")]
        public ActionResult<IEnumerable<ChamadoDTO>> GetByTecnico(int tecnicoId)
        {
            var list = _chamadoService.GetAll()

                .Where(c => c.IdTecnicoResponsavel == tecnicoId)
                .Select(c => ToDTO(c));

            if (!list.Any()) return NotFound(new { message = "Nenhum chamado atribuído a este técnico." });
            return Ok(list);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteChamado(int id)
        {
            var chamado = _chamadoService.GetById(id);
            if (chamado == null) return NotFound();

            _chamadoService.Delete(id);
            return NoContent();
        }
    }
}