using System.Windows.Input;
using PrismMauiApp.Model;
using PrismMauiApp.Services;

namespace PrismMauiApp.ViewModels
{
    public class TodoDetailViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService navigationService;
        private readonly ITodoRepository todoRepository;

        private Command saveCommand;
        private bool isNewItem;
        private string id;
        private string text;
        private DateTime dueDate;
        private string description;

        public TodoDetailViewModel(
            INavigationService navigationService,
            ITodoRepository todoRepository)
        {
            this.navigationService = navigationService;
            this.todoRepository = todoRepository;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["model"] is not Todo existingItem)
            {
                this.isNewItem = true;
                this.DueDate = DateTime.Now.Date.AddDays(1).AddHours(12);
            }
            else
            {
                this.Id = existingItem.Id;
                this.Name = existingItem.Name;
                this.DueDate = existingItem.DueDate;
                this.Description = existingItem.Description;

                this.isNewItem = false;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // Nothing to do here...
        }

        public string Id
        {
            get => this.id;
            private set => this.SetProperty(ref this.id, value, nameof(this.Id));
        }

        public string Name
        {
            get => this.text;
            set
            {
                if (this.SetProperty(ref this.text, value, nameof(this.Name)))
                {
                    this.RaisePropertyChanged(nameof(this.Summary));
                }
            }
        }

        public DateTime DueDate
        {
            get => this.dueDate;
            set
            {
                if (this.SetProperty(ref this.dueDate, value, nameof(this.DueDate)))
                {
                    this.RaisePropertyChanged(nameof(this.Summary));
                }
            }
        }

        public string Description
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value, nameof(this.Description));
        }

        // DEMO: Cascading updates: If Text or DueDate property changes, Summary needs to be updated too.
        public string Summary => $"Summary: {this.Name} @ {this.DueDate}";

        public ICommand SaveCommand => this.saveCommand ??= new Command(async () =>
        {
            var todo = new Todo
            {
                Id = this.Id,
                Name = this.Name,
                DueDate = this.DueDate,
                Description = this.Description,
            };

            if (this.isNewItem)
            {
                await this.todoRepository.AddAsync(todo);
            }
            else
            {
                await this.todoRepository.UpdateAsync(todo);
            }

            var navigationParameters = new NavigationParameters
            {
                { "isNewItem", this.isNewItem },
            };
            await this.navigationService.GoBackAsync(navigationParameters);
        });
    }
}