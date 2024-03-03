
using ForexApp.Model;

namespace ForexApp.Services
{
    public interface IForexService
    {
        Task<IEnumerable<QuoteDto>> GetLatestQuotes(string baseCurrency, string[] currencies);
    }
}