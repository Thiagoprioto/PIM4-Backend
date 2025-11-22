using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PIM4_backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// 1. ADICIONANDO A POLÍTICA DE CORS (Permissão para o App Mobile)
// ==========================================================
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// ==========================================================
// 2. ADICIONANDO O HTTPCLIENT (O "Telefone" para ligar para o n8n)
// ==========================================================
builder.Services.AddHttpClient();

// ==========================================================
// 3. CONFIGURAÇÃO DO JWT (JSON WEB TOKEN)
// ==========================================================
// Puxa a "Chave Secreta" do appsettings.json
var jwtKey = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false; // (Mude para true em produção)
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Adiciona o contexto do EF Core (Seu código original)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona os controladores (Seu código original)
builder.Services.AddControllers();

// Swagger (Seu código original)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // (Simplificado)

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // (Corretamente comentado)

app.UseCors(myAllowSpecificOrigins);

// ==========================================================
// 4. ATIVAR A AUTENTICAÇÃO E AUTORIZAÇÃO
// (Tem que vir ANTES de MapControllers)
// ==========================================================
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();