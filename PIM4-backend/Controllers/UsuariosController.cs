using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Usuarios.ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Usuario usuario)
        {
            var existing = _context.Usuarios.Find(id);
            if (existing == null) return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("conexao")]
        public IActionResult TestarConexao()
        {
            try
            {
                if (_context.Database.CanConnect())
                {
                    return Ok("Conexão com o banco de dados OK!");
                }
                else
                {
                    return StatusCode(500, "Falha ao conectar ao banco de dados.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar: {ex.Message}");
            }
        }
    }
}
