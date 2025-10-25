using Microsoft.AspNetCore.Mvc;
using PIM4_backend.Data;
using PIM4_backend.Models;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusChamadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusChamadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.StatusChamados.ToList());
    }
}
