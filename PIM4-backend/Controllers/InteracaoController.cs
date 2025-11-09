using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM4_backend.Data;
using PIM4_backend.DTO;
using PIM4_backend.Models;
using System.Security.Claims;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Exige login
    public class InteracaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InteracaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================================
        // 1. ENDPOINT PARA LISTAR OS COMENTÁRIOS DE UM CHAMADO
        // GET: /api/Interacoes/1 (onde 1 é o IdChamado)
        // ==========================================================
        [HttpGet("{idChamado}")]
        // Permite que o Colaborador E o Técnico leiam os comentários
        [Authorize(Roles = "Tecnico, Colaborador")]
        public async Task<IActionResult> GetInteracoesPorChamado(int idChamado)
        {
            // Busca as interações e também o Nome do Autor (fazendo um JOIN)
            var interacoes = await _context.Interacoes
                .Where(i => i.IdChamado == idChamado)
                .OrderBy(i => i.DataInteracao) // Da mais antiga para a mais nova
                .Select(i => new InteracaoDTO // Usa o InteracaoDTO que você já tem
                {
                    IdInteracao = i.IdInteracao,
                    IdChamado = i.IdChamado,
                    IdAutor = i.IdAutor,
                    // NomeAutor = i.Autor.Nome, (Descomente se você configurou o DTO para ter o NomeAutor)
                    Mensagem = i.Mensagem,
                    DataInteracao = i.DataInteracao
                })
                .ToListAsync();

            return Ok(interacoes);
        }

        // ==========================================================
        // 2. ENDPOINT PARA O TÉCNICO ADICIONAR UM COMENTÁRIO
        // POST: /api/Interacoes
        // ==========================================================
        [HttpPost]
        [Authorize(Roles = "Tecnico")] // Só o Técnico pode escrever
        public async Task<IActionResult> AdicionarInteracao([FromBody] ComentarioDTO dto)
        {
            // Pega o ID do técnico que está logado (do Token)
            var tecnicoId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (string.IsNullOrWhiteSpace(dto.Mensagem))
            {
                return BadRequest("A mensagem não pode estar vazia.");
            }

            var novaInteracao = new Interacao
            {
                IdChamado = dto.IdChamado,
                Mensagem = dto.Mensagem,
                IdAutor = tecnicoId, // O ID do técnico logado
                DataInteracao = DateTime.Now
            };

            _context.Interacoes.Add(novaInteracao);
            await _context.SaveChangesAsync();

            // Retorna o DTO da interação que acabou de ser criada
            var interacaoCriadaDto = new InteracaoDTO
            {
                IdInteracao = novaInteracao.IdInteracao,
                IdChamado = novaInteracao.IdChamado,
                IdAutor = novaInteracao.IdAutor,
                Mensagem = novaInteracao.Mensagem,
                DataInteracao = novaInteracao.DataInteracao
            };

            return Ok(interacaoCriadaDto);
        }
    }
}
