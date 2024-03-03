using ForexApp.Model;

namespace ForexApp.Services
{
    public class ForexServiceMock : IForexService
    {
        private static readonly Random Rng = new Random();

        public async Task<IEnumerable<QuoteDto>> GetLatestQuotes(string baseCurrency, string[] targetCurrencies)
        {
            var quoteDtos = new List<QuoteDto>();

            foreach (var targetCurrency in targetCurrencies)
            {
                var randomPrice = (decimal)Rng.NextDouble() * Rng.Next(1, 100);
                var dto = new QuoteDto(baseCurrency, targetCurrency, randomPrice);
                quoteDtos.Add(dto);
            }

            // Wait 
            await Task.Delay(3000);

            return quoteDtos;
        }
    }
}