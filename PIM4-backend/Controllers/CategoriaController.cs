using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Categorias.ToList());

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), categoria);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = _context.Categorias.Find(id);
            if (cat == null) return NotFound();

            _context.Categorias.Remove(cat);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
