
using ForexApp.Model;

namespace ForexApp.Services
{
    public interface IForexService
    {
        Task<IEnumerable<QuoteDto>> GetQuotes(string baseCurrency, string[] currencies);
    }
}