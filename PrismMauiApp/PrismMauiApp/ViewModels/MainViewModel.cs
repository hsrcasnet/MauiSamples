using Microsoft.Extensions.Logging;

namespace PrismMauiApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILogger<MainViewModel> logger;

        public MainViewModel(ILogger<MainViewModel> logger)
        {
            this.logger = logger;
        }
    }
}
