using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PIM4_backend.Data;
using PIM4_backend.DTO;
using PIM4_backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PIM4_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            // 1. Encontra o usuário no banco de dados pelo Email
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

            // 2. Verifica se o usuário existe e se a senha está correta
            // ATENÇÃO: Estamos comparando texto puro (SenhaHash == Senha)
            // porque foi o que combinamos para o teste.
            if (usuario == null || usuario.SenhaHash != request.Senha)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }

            // 3. Gera o Token JWT
            var token = GenerateJwtToken(usuario);

            // 4. Retorna o Token e as informações do usuário para o app
            return Ok(new LoginResponseDTO
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil // A parte mais importante!
            });
        }

        [HttpPost("register")]
        [Authorize(Roles = "Tecnico")]
        public async Task<IActionResult> Register([FromBody] CadastroUsuarioCompletoDTO dto)
        {
            // 1. Valida se o e-mail já existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Este e-mail já está em uso.");
            }

            // 2. Cria o Utilizador
            var novoUsuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.Senha,
                Perfil = dto.Perfil,
                IdDepartamento = dto.IdDepartamento
            };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Colaborador cadastrado com sucesso!" });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            // Pega a chave secreta do appsettings.json
            var jwtKey = _configuration["Jwt:Key"];
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Adiciona as "claims" (informações) que queremos no token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Perfil) // Adiciona o PERFIL no token
                }),
                Expires = DateTime.UtcNow.AddHours(8), // Duração do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}