using PIM4_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faq;

        public FAQController(IFAQService faq)
        {
            _faq = faq;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_faq.GetAll());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string q)
        {
            return Ok(_faq.Search(q));
        }
    }
}
