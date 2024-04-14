using System.Windows.Input;
using PrismMauiApp.Model;
using static PrismMauiApp.App;

namespace PrismMauiApp.ViewModels
{
    public class TodoItemViewModel : BindableBase
    {
        private readonly IDateTime dateTime;
        private readonly INavigationService navigationService;
        private readonly Todo todo;

        private ICommand navigateToTodoDetailPageCommand;
        private bool done;

        public TodoItemViewModel(
            IDateTime dateTime,
            INavigationService navigationService,
            Todo todo)
        {
            this.todo = todo;
            this.Name = todo.Name;
            this.DueDate = todo.DueDate;
            this.Done = todo.Done;
            this.dateTime = dateTime;
            this.navigationService = navigationService;
        }

        public string Name { get; }

        public DateTime? DueDate { get; }

        public bool Done
        {
            get => this.done;
            set
            {
                if (this.SetProperty(ref this.done, value))
                {
                    this.RaisePropertyChanged(nameof(this.IsOverdue));
                }
            }
        }

        public bool IsOverdue
        {
            get
            {
                return !this.Done && this.DueDate is DateTime dueDate && dueDate < this.dateTime.Now;
            }
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
