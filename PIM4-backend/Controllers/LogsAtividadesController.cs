using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsAtividadesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogsAtividadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.LogsAtividades.ToList());

        [HttpPost]
        public IActionResult Create(LogAtividade log)
        {
            _context.LogsAtividades.Add(log);
            _context.SaveChanges();
            return Ok(log);
        }
    }
}
