using DataBindingDemo.Views;

namespace DataBindingDemo.Services
{
    /// <summary>
    /// Very simple and tightly-coupled implementation of a navigation service.
    /// Production implementations would provide more features and comfort,
    /// but for educational reasons we should be fine for now.
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly Page currentPage;

        public NavigationService(Page page)
        {
            this.currentPage = page;
        }

        public Task PushAsync(string pageName)
        {
            Page page = null;
            switch (pageName)
            {
                case nameof(SliderPage):
                    page = new SliderPage();
                    break;

                case nameof(LoginPage):
                    page = new LoginPage();
                    break;

                case nameof(CarsListPage):
                    page = new CarsListPage();
                    break;
                    
                case nameof(SuperCarsListPage):
                    page = new SuperCarsListPage();
                    break;

                default:
                    throw new NotSupportedException($"Page '{pageName}' is not known");
            }

            return this.currentPage.Navigation.PushAsync(page);
        }
    }
}