using PIM4_backend.Models;

namespace PIM4_backend.Services
{
    public interface IChamadoService
    {
        IEnumerable<Chamado> GetAll();
        Chamado? GetById(int id);
        Chamado Create(Chamado chamado);
        Chamado? UpdateStatus(int id, string status);
        Chamado? AssignTecnico(int id, int tecnicoId);
        Interacao? AddInteracao(int chamadoId, Interacao interacao);
        bool Delete(int id);
    }
}
