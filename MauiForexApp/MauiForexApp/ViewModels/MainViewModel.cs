using System.Collections.ObjectModel;
using System.Windows.Input;
using ForexApp.Model;
using ForexApp.Services;

namespace ForexApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IForexService forexService;
        private string baseCurrency;
        private string targetCurrencies;
        private bool isBusy;
        private bool isRefreshing;

        public MainViewModel(IForexService forexService)
        {
            this.forexService = forexService;

            this.Title = "Welcome to ForexApp";
            this.Quotes = new ObservableCollection<QuoteViewModel>();

            this.RefreshButtonCommand.Execute(null);
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
            if (!this.Quotes.Any())
            {
                await this.AddQuoteAsync("CHF", "EUR");
                await this.AddQuoteAsync("EUR", "CHF");
            }
            else
            {
                foreach (var vm in this.Quotes)
                {
                    await this.LoadAndUpdateQuotesAsync(vm.BaseCurrency, vm.TargetCurrency);
                }
            }
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

        private async Task LoadAndUpdateQuotesAsync(string baseCurrency, params string[] targetCurrencies)
        {
            try
            {
                var quoteDtos = (await this.forexService.GetLatestQuotes(baseCurrency, targetCurrencies)).ToArray();

                if (quoteDtos.Length == 0)
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
                // TODO: Inform user about the error using a dialog
            }
        }

        private void AddOrUpdateQuote(QuoteDto quoteDto)
        {
            var existingQuoteViewModel = this.Quotes.SingleOrDefault(q =>
                q.BaseCurrency == quoteDto.BaseCurrency &&
                q.TargetCurrency == quoteDto.TargetCurrency);

            if (existingQuoteViewModel == null)
            {
                this.Quotes.Add(new QuoteViewModel(quoteDto));
            }
            else
            {
                existingQuoteViewModel.Update(quoteDto);
            }
        }

        public ICommand AddQuoteCommand => new Command(
            execute: async () => await this.AddQuoteAsync(),
            canExecute: () => this.CanAddQuote);

        private async Task AddQuoteAsync()
        {
            await this.AddQuoteAsync(this.BaseCurrency, this.TargetCurrencies);

            this.BaseCurrency = null;
            this.TargetCurrencies = null;
        }

        private async Task AddQuoteAsync(string baseCurrency, string targetCurrencies)
        {
            try
            {
                this.IsBusy = true;

                // TODO: Validate input parameters BaseCurrency and TargetCurrencies

                var targetCurrenciesArray = targetCurrencies?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToArray();

                await this.LoadAndUpdateQuotesAsync(baseCurrency, targetCurrenciesArray);
            }
            catch (Exception ex)
            {
                // TODO: Log exception here
                // TODO: Inform user about the error
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public string BaseCurrency
        {
            get => this.baseCurrency;
            set
            {
                this.baseCurrency = value;
                this.OnPropertyChanged(nameof(this.BaseCurrency));
                this.OnPropertyChanged(nameof(this.CanAddQuote));
            }
        }

        public string TargetCurrencies
        {
            get => this.targetCurrencies;
            set
            {
                this.targetCurrencies = value;
                this.OnPropertyChanged(nameof(this.TargetCurrencies));
                this.OnPropertyChanged(nameof(this.CanAddQuote));
            }
        }

        public bool CanAddQuote
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.BaseCurrency) && !string.IsNullOrWhiteSpace(this.TargetCurrencies);
            }
        }

        public ObservableCollection<QuoteViewModel> Quotes { get; private set; }
    }
}