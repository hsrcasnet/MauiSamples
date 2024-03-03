using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;

namespace ForexApp.Model
{
    [DebuggerDisplay("Data.Count = {Data.Count}")]
public class LatestQuotesDto
{
    public LatestQuotesDto()
    {
        this.Data = new Dictionary<string, decimal>();
    }

    [JsonProperty("data")]
    public IDictionary<string, decimal> Data { get; set; }
}

    //public class Currency
    //{
    //    public string Symbol { get; set; }
    //    public string Name { get; set; }
    //    public string SymbolNative { get; set; }
    //    public int DecimalDigits { get; set; }
    //    public int Rounding { get; set; }
    //    public string Code { get; set; }
    //    public string NamePlural { get; set; }
    //}

    //public class CurrencyData
    //{
    //    public Dictionary<string, Currency> Currencies { get; set; }
    //}

    //public class RootObject
    //{
    //    public CurrencyData Data { get; set; }
    //}
}