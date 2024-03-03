using ForexApp.Model;

namespace ForexApp.ViewModels
{
    public class QuoteViewModel : ViewModelBase
    {
        private decimal price;
        private string symbol;

        public QuoteViewModel(QuoteDto quoteDto)
        {
            this.Update(quoteDto);
        }

        public string Symbol
        {
            get => this.symbol;
            set
            {
                this.symbol = value;
                this.OnPropertyChanged(nameof(this.Symbol));
            }
        }

        public decimal Price
        {
            get => this.price;
            set
            {
                this.price = value;
                this.OnPropertyChanged(nameof(this.Price));
            }
        }

        public void Update(QuoteDto quoteDto)
        {
            this.Symbol = quoteDto.Symbol;
            this.Price = quoteDto.Price;
        }
    }
}