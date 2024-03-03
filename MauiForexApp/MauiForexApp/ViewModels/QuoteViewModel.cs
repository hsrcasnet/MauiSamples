using ForexApp.Model;

namespace ForexApp.ViewModels
{
    public class QuoteViewModel : ViewModelBase
    {
        private decimal price;
        private string baseCurrency;
        private string targetCurrency;

        public QuoteViewModel(QuoteDto quoteDto)
        {
            this.Update(quoteDto);
        }

        public string BaseCurrency
        {
            get => this.baseCurrency;
            set
            {
                this.baseCurrency = value;
                this.OnPropertyChanged(nameof(this.BaseCurrency));
            }
        }
        

        public string TargetCurrency
        {
            get => this.targetCurrency;
            set
            {
                this.targetCurrency = value;
                this.OnPropertyChanged(nameof(this.TargetCurrency));
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
            this.BaseCurrency = quoteDto.BaseCurrency;
            this.TargetCurrency = quoteDto.TargetCurrency;
            this.Price = quoteDto.Price;
        }
    }
}