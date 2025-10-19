using PIM4_backend.Models;

namespace PIM4_backend.Services
{
    public interface IFAQService
    {
        IEnumerable<FAQ> GetAll();
        IEnumerable<FAQ> Search(string query);
    }
}

