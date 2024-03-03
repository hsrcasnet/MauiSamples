using ForexApp.Model;

namespace ForexApp.Services
{
    public class ForexServiceMock : IForexService
    {
        private static readonly Random Rng = new Random();

        public async Task<IEnumerable<QuoteDto>> GetQuotes(string baseCurrency, string[] currencies)
        {
            var quoteDtos = new List<QuoteDto>();
            foreach (var symbol in currencies)
            {
                var dto = new QuoteDto
                {
                    Symbol = symbol,
                    Price = (decimal)Rng.NextDouble() * Rng.Next(1, 100),
                };
                quoteDtos.Add(dto);
            }

            await Task.Delay(3000);

            return quoteDtos;
        }
    }
}