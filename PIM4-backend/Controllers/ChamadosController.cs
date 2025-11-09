using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM4_backend.Data;
using PIM4_backend.DTO;
using PIM4_backend.Models;
using System.Security.Claims;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChamadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChamadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("meus")]
        [Authorize(Roles = "Colaborador")]
        public async Task<IActionResult> GetMeusChamados()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var chamados = await _context.Chamados
                .Where(c => c.IdUsuarioSolicitante == userId)
                .OrderByDescending(c => c.DataAbertura)
                .Select(c => new ChamadoDTO
                {
                    IdChamado = c.IdChamado,
                    Titulo = c.Titulo,
                    Descricao = c.Descricao,
                    Prioridade = c.Prioridade,
                    DataAbertura = c.DataAbertura,
                    DataFechamento = c.DataFechamento,
                    IdCategoria = c.IdCategoria,
                    IdUsuarioSolicitante = c.IdUsuarioSolicitante,
                    IdTecnicoResponsavel = c.IdTecnicoResponsavel,
                    IdStatus = c.IdStatus
                })
                .ToListAsync();

            return Ok(chamados);
        }

        [HttpPost]
        [Authorize(Roles = "Colaborador")]
        public async Task<IActionResult> AbrirNovoChamado([FromBody] NovoChamadoDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var novoChamado = new Chamado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                IdCategoria = dto.IdCategoria,
                IdUsuarioSolicitante = userId,
                DataAbertura = DateTime.Now,
                IdStatus = 1,
                Prioridade = 1
            };

            _context.Chamados.Add(novoChamado);
            await _context.SaveChangesAsync();

            return Ok(novoChamado);
        }

        [HttpGet("todos")]
        [Authorize(Roles = "Tecnico")]
        public async Task<IActionResult> GetTodosChamados()
        {
            var chamados = await _context.Chamados
                .OrderByDescending(c => c.DataAbertura)
                .Select(c => new ChamadoDTO
                {
                    IdChamado = c.IdChamado,
                    Titulo = c.Titulo,
                    Descricao = c.Descricao,
                    Prioridade = c.Prioridade,
                    DataAbertura = c.DataAbertura,
                    DataFechamento = c.DataFechamento,
                    IdCategoria = c.IdCategoria,
                    IdUsuarioSolicitante = c.IdUsuarioSolicitante,
                    IdTecnicoResponsavel = c.IdTecnicoResponsavel,
                    IdStatus = c.IdStatus
                })
                .ToListAsync();

            return Ok(chamados);
        }

        [HttpPut("{id}/assumir")]
        [Authorize(Roles = "Tecnico")]
        public async Task<IActionResult> AssumirChamado(int id)
        {
            var tecnicoId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var chamado = await _context.Chamados.FindAsync(id);

            if (chamado == null) return NotFound();

            chamado.IdTecnicoResponsavel = tecnicoId;
            chamado.IdStatus = 2;
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }

        [HttpPut("{id}/finalizar")]
        [Authorize(Roles = "Tecnico")]
        public async Task<IActionResult> FinalizarChamado(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null) return NotFound();

            chamado.IdStatus = 3;
            chamado.DataFechamento = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }
    }
}