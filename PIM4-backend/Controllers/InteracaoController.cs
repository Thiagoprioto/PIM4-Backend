using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteracoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InteracoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Interacoes.ToList());

        [HttpPost]
        public IActionResult Create(Interacao interacao)
        {
            _context.Interacoes.Add(interacao);
            _context.SaveChanges();
            return Ok(interacao);
        }
    }
}
