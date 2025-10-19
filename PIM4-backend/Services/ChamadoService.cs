// Arquivo: PIM4-backend/Services/ChamadoService.cs

using PIM4_backend.Models;
using PIM4_backend.Services;

namespace PIM4_backend.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly List<Chamado> _chamados = new();
        private int _nextId = 1;

        public ChamadoService()
        {
            // seed com nenhum chamado — você pode adicionar se quiser
        }

        public Chamado Create(Chamado chamado)
        {
            // CORREÇÃO: Propriedade 'IdChamado'
            chamado.IdChamado = _nextId++;
            chamado.DataAbertura = DateTime.UtcNow;
            _chamados.Add(chamado);
            return chamado;
        }

        public IEnumerable<Chamado> GetAll() => _chamados;

        public Chamado? GetById(int id) =>
            // CORREÇÃO: Propriedade 'IdChamado'
            _chamados.FirstOrDefault(c => c.IdChamado == id);

        public Chamado? UpdateStatus(int id, string status)
        {
            var c = GetById(id);
            if (c == null) return null;

            
            // (Assumindo 1=Aberto, 2=EmAndamento, 3=Fechado)
            if (status.Equals("Fechado", StringComparison.OrdinalIgnoreCase))
            {
                c.IdStatus = 3;
                c.DataFechamento = DateTime.UtcNow;
            }
            else if (status.Equals("Aberto", StringComparison.OrdinalIgnoreCase))
            {
                c.IdStatus = 1;
                c.DataFechamento = null;
            }
            else if (status.Equals("EmAndamento", StringComparison.OrdinalIgnoreCase))
            {
                c.IdStatus = 2;
                c.DataFechamento = null;
            }

            return c;
        }

        public Chamado? AssignTecnico(int id, int tecnicoId)
        {
            var c = GetById(id);
            if (c == null) return null;

            
            c.IdTecnicoResponsavel = tecnicoId;

            
            c.IdStatus = 2; // Assumindo 2 = EmAndamento

            return c;
        }

        public Interacao? AddInteracao(int chamadoId, Interacao interacao)
        {
            var c = GetById(chamadoId);
            if (c == null) return null;

            
            if (c.Interacoes == null)
            {
                c.Interacoes = new List<Interacao>();
            }

            

            
            interacao.IdInteracao = c.Interacoes.Any() ? c.Interacoes.Max(i => i.IdInteracao) + 1 : 1;

            
            interacao.DataInteracao = DateTime.UtcNow;

            c.Interacoes.Add(interacao);
            return interacao;
        }

        public bool Delete(int id)
        {
            
            var chamado = _chamados.FirstOrDefault(c => c.IdChamado == id);

            if (chamado == null) return false;
            _chamados.Remove(chamado);
            return true;
        }
    }
}