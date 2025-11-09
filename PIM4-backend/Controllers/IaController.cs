using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string N8N_CHAT_WEBHOOK_URL = "http://localhost:5678/webhook/3a3e5181-cec5-42b6-b312-8853a544fda3";
        public IaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> PerguntarAoChat([FromBody] ChatRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync(N8N_CHAT_WEBHOOK_URL, request);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(500, new { resposta = "O assistente de IA está indisponível no momento." });
                }

                var respostaIa = await response.Content.ReadFromJsonAsync<IaResponse>();

                return Ok(respostaIa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resposta = $"Erro ao conectar com a IA: {ex.Message}" });
            }
        }
    }
    public class ChatRequest
    {
        public string Pergunta { get; set; }
        public string SessionId { get; set; }
    }

    public class IaResponse
    {
        public string Resposta { get; set; }
    }
}