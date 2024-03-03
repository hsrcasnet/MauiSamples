using System.Collections.ObjectModel;
using System.Windows.Input;
using ForexApp.Model;
using ForexApp.Services;

namespace ForexApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IForexService forexService;
        private string newQuoteSymbol;
        private bool isBusy;
        private bool isRefreshing;

        public MainViewModel(IForexService forexService)
        {
            this.forexService = forexService;

            this.Title = "Welcome to ForexApp";
            this.Quotes = new ObservableCollection<QuoteViewModel>();
        }

        public ICommand RefreshButtonCommand => new Command(
            async () =>
                {
                    this.IsBusy = true;
                    await this.LoadData();
                    this.IsBusy = false;
                });

        public ICommand RefreshListCommand => new Command(
            async () =>
                {
                    this.IsRefreshing = true;
                    await this.LoadData();
                    this.IsRefreshing = false;
                });

        private async Task LoadData()
        {
            string[] pairs;

            if (!this.Quotes.Any())
            {
                pairs = new[] { "EUR_CHF", "CHF_EUR", };
            }
            else
            {
                pairs = this.Quotes.Select(q => q.Symbol).ToArray();
            }

            await this.LoadAndUpdateQuotes(pairs);
        }

        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                this.isBusy = value;
                this.OnPropertyChanged(nameof(this.IsBusy));
            }
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set
            {
                this.isRefreshing = value;
                this.OnPropertyChanged(nameof(this.IsRefreshing));
            }
        }

        private async Task LoadAndUpdateQuotes(string[] pairs)
        {
            try
            {
                ICollection<QuoteDto> quotes = (await this.forexService.GetQuotes("CHF", pairs)).ToList();

                this.UpdateQuotes(quotes);
            }
            catch (Exception ex)
            {
                // TODO: Log exception here
                // TODO: Inform user about the error using a dialog
            }
        }

        private void UpdateQuotes(ICollection<QuoteDto> quotes)
        {
            foreach (var quoteDto in quotes)
            {
                this.AddOrUpdateQuote(quoteDto);
            }

            var returnedSymbols = quotes.Select(dto => dto.Symbol);
            var unusedViewModels = this.Quotes.Where(vm => !returnedSymbols.Contains(vm.Symbol)).ToList();
            foreach (var quoteViewModel in unusedViewModels)
            {
                this.Quotes.Remove(quoteViewModel);
            }
        }

        private void AddOrUpdateQuote(QuoteDto quoteDto)
        {
            var vm = this.Quotes.SingleOrDefault(q => q.Symbol == quoteDto.Symbol);
            if (vm == null)
            {
                this.Quotes.Add(new QuoteViewModel(quoteDto));
            }
            else
            {
                vm.Update(quoteDto);
            }
        }

        public ICommand AddSymbolCommand => new Command(
            execute: async () => await this.AddSymbolAsync(),
            canExecute: () => this.IsNewQuoteSymbolEnabled);

        private async Task AddSymbolAsync()
        {
            try
            {
                var symbol = this.NewQuoteSymbol;
                var pairs = new[] { symbol };
                var quoteDtos = (await this.forexService.GetQuotes("CHF", pairs)).ToList();

                if (quoteDtos.Count == 0)
                {
                    // TODO: Inform user that the result was empty
                }
                else
                {
                    foreach (var quoteDto in quoteDtos)
                    {
                        this.AddOrUpdateQuote(quoteDto);
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Log exception here
                // TODO: Inform user about the error
            }

            this.NewQuoteSymbol = null;
        }

        public string NewQuoteSymbol
        {
            get => this.newQuoteSymbol;
            set
            {
                this.newQuoteSymbol = value;
                this.OnPropertyChanged(nameof(this.NewQuoteSymbol));
                this.OnPropertyChanged(nameof(this.IsNewQuoteSymbolEnabled));
            }
        }

        public bool IsNewQuoteSymbolEnabled
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.NewQuoteSymbol);
            }
        }

        public ObservableCollection<QuoteViewModel> Quotes { get; private set; }
    }
}