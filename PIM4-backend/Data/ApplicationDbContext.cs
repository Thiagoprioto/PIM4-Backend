using Microsoft.EntityFrameworkCore;
using PIM4_backend.Models;

namespace PIM4_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<StatusChamado> StatusChamados { get; set; }
        public DbSet<LogAtividade> LogsAtividades { get; set; }
        public DbSet<Interacao> Interacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Relacionamento: Chamado <-> Usuário Solicitante
            modelBuilder.Entity<Chamado>()
                .HasOne(c => c.UsuarioSolicitante)      // Um Chamado tem UM Solicitante
                .WithMany(u => u.ChamadosAbertos)       // Um Usuário tem MUITOS ChamadosAbertos
                .HasForeignKey(c => c.IdUsuarioSolicitante) // A chave é IdUsuarioSolicitante
                .OnDelete(DeleteBehavior.Restrict);     // Impede deletar um usuário se ele tiver chamados

            // 2. Relacionamento: Chamado <-> Técnico Responsável
            modelBuilder.Entity<Chamado>()
                .HasOne(c => c.TecnicoResponsavel)      // Um Chamado tem UM Técnico
                .WithMany(u => u.ChamadosAtribuidos)    // Um Usuário tem MUITOS ChamadosAtribuidos
                .HasForeignKey(c => c.IdTecnicoResponsavel) // A chave é IdTecnicoResponsavel
                .OnDelete(DeleteBehavior.SetNull);      // Se o técnico for deletado, o campo fica nulo
        }
    }
}