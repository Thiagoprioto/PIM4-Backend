using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespostasIAController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RespostasIAController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.RespostasIA.ToList());

        [HttpPost]
        public IActionResult Create(RespostaIA resposta)
        {
            _context.RespostasIA.Add(resposta);
            _context.SaveChanges();
            return Ok(resposta);
        }
    }
}
