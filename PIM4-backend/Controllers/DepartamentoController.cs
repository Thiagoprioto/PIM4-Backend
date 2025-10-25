using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Departamentos.ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dep = _context.Departamentos.Find(id);
            return dep == null ? NotFound() : Ok(dep);
        }

        [HttpPost]
        public IActionResult Create(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = departamento.IdDepartamento }, departamento);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Departamento departamento)
        {
            var existing = _context.Departamentos.Find(id);
            if (existing == null) return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(departamento);
            _context.SaveChanges();
            return Ok(departamento);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dep = _context.Departamentos.Find(id);
            if (dep == null) return NotFound();

            _context.Departamentos.Remove(dep);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
