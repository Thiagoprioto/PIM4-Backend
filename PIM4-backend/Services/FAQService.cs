// Arquivo: PIM4-backend/Services/FAQService.cs

using PIM4_backend.Models;
using PIM4_backend.Services;

namespace PIM4_backend.Services
{
    public class FAQService : IFAQService
    {
        private readonly List<FAQ> _faqs = new();

        public FAQService()
        {
            // Dados iniciais de FAQ (podem ser expandidos)

            
            _faqs.Add(new FAQ { IdFaq = 1, Pergunta = "Como resetar senha?", Resposta = "Solicite ao técnico ou use a opção 'esqueci senha' no portal." });

            
            _faqs.Add(new FAQ { IdFaq = 2, Pergunta = "O computador não liga", Resposta = "Verifique cabos e energia. Se persistir, abra um chamado na categoria Hardware." });
        }

        public IEnumerable<FAQ> GetAll() => _faqs;

        public IEnumerable<FAQ> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return _faqs;
            query = query.ToLowerInvariant();
            return _faqs.Where(f => f.Pergunta.ToLower().Contains(query) || f.Resposta.ToLower().Contains(query));
        }
    }
}