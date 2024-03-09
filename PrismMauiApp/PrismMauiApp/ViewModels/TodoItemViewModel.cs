using System.Windows.Input;
using PrismMauiApp.Model;
using static PrismMauiApp.App;

namespace PrismMauiApp.ViewModels
{
    public class TodoItemViewModel : BindableBase
    {
        private readonly INavigationService navigationService;
        private readonly Todo todo;

        private ICommand navigateToTodoDetailPageCommand;
        private bool done;

        public TodoItemViewModel(
            INavigationService navigationService,
            Todo todo)
        {
            this.todo = todo;
            this.Name = todo.Name;
            this.DueDate = todo.DueDate;
            this.Done = todo.Done;
            this.navigationService = navigationService;
        }

        public string Name { get; }

        public DateTime? DueDate { get; }

        public bool Done
        {
            get => this.done;
            set => this.SetProperty(ref this.done, value);
        }

        public ICommand NavigateToTodoDetailPageCommand => this.navigateToTodoDetailPageCommand ??= new Command(async () =>
        {
            var navigationParameters = new NavigationParameters
            {
                { "model", this.todo }
            };
            await this.navigationService.NavigateAsync(Pages.TodoDetailPage, navigationParameters);
        });

    }
}
