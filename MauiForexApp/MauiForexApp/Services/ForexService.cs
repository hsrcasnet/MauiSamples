using System.Web;
using ForexApp.Model;
using Newtonsoft.Json;

namespace ForexApp.Services
{
    public class ForexService : IForexService
    {
        private readonly IForexServiceConfiguration forexServiceConfiguration;
        private readonly HttpClient httpClient;

        public ForexService(IForexServiceConfiguration forexServiceConfiguration)
        {
            this.forexServiceConfiguration = forexServiceConfiguration;
            this.httpClient = new HttpClient();
        }

        public async Task<IEnumerable<QuoteDto>> GetLatestQuotes(string baseCurrency, string[] targetCurrencies)
        {
            // Send API request
            var apiKey = this.forexServiceConfiguration.ApiKey;
            var currencies = HttpUtility.UrlEncode(string.Join(",", targetCurrencies));

            var uri = $"https://api.freecurrencyapi.com/v1/latest?apikey={apiKey}" +
                $"&base_currency={baseCurrency}" +
                $"&currencies={currencies}";

            var httpResponseMessage = await this.httpClient.GetAsync(uri);

            httpResponseMessage.EnsureSuccessStatusCode();

            // Read request payload and transform JSON to DTO objects
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var latestQuotesDto = JsonConvert.DeserializeObject<LatestQuotesDto>(jsonResponse);

            var quoteDtos = latestQuotesDto.Data
                .Select(c => new QuoteDto(baseCurrency, c.Key, c.Value))
                .ToArray();

            return quoteDtos;
        }
    }
}