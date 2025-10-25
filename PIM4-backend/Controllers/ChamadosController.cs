using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChamadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var chamados = _context.Chamados
                .Include(c => c.UsuarioSolicitante)
                .Include(c => c.TecnicoResponsavel)
                .ToList();

            return Ok(chamados);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var chamado = _context.Chamados
                .Include(c => c.UsuarioSolicitante)
                .Include(c => c.TecnicoResponsavel)
                .FirstOrDefault(c => c.IdChamado == id);

            return chamado == null ? NotFound() : Ok(chamado);
        }

        [HttpPost]
        public IActionResult Create(Chamado chamado)
        {
            _context.Chamados.Add(chamado);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = chamado.IdChamado }, chamado);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Chamado chamado)
        {
            var existing = _context.Chamados.Find(id);
            if (existing == null) return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(chamado);
            _context.SaveChanges();
            return Ok(chamado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var chamado = _context.Chamados.Find(id);
            if (chamado == null) return NotFound();

            _context.Chamados.Remove(chamado);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
