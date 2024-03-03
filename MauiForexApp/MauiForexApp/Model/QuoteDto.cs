using System.Diagnostics;

namespace ForexApp.Model
{
    [DebuggerDisplay("BaseCurrency = {BaseCurrency}, TargetCurrency = {TargetCurrency}, Price = {Price}")]
    public class QuoteDto
    {
        public QuoteDto(string baseCurrency, string targetCurrency, decimal price)
        {
            this.BaseCurrency = baseCurrency;
            this.TargetCurrency = targetCurrency;
            this.Price = price;
        }

        public string BaseCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public decimal Price { get; set; }
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