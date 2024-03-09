using System.Windows.Input;
using Microsoft.Extensions.Logging;
using PrismMauiApp.Model;
using PrismMauiApp.Services;

namespace PrismMauiApp.ViewModels
{
    public class TodoDetailViewModel : ViewModelBase, INavigatedAware
    {
        private readonly ILogger<TodoDetailViewModel> logger;
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly ITodoRepository todoRepository;
        private readonly ILauncher launcher;

        private bool isNewItem;
        private string id;
        private string name;
        private DateTime dueDate;
        private string description;
        private string link;
        private Command launchUrlCommand;
        private Command saveCommand;

        public TodoDetailViewModel(
            ILogger<TodoDetailViewModel> logger,
            INavigationService navigationService,
            IPageDialogService dialogService,
            ITodoRepository todoRepository,
            ILauncher launcher)
        {
            this.logger = logger;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.todoRepository = todoRepository;
            this.launcher = launcher;
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
                this.Link = existingItem.Link;
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
            get => this.name;
            set => this.SetProperty(ref this.name, value, nameof(this.Name));
        }

        public DateTime DueDate
        {
            get => this.dueDate;
            set => this.SetProperty(ref this.dueDate, value, nameof(this.DueDate));
        }

        public string Description
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value, nameof(this.Description));
        }

        public ICommand LaunchUrlCommand => this.launchUrlCommand ??= new Command(() => _ = this.LaunchUrlAsync());

        public string Link
        {
            get => this.link;
            set => this.SetProperty(ref this.link, value, nameof(this.Link));
        }

        private async Task LaunchUrlAsync()
        {
            await this.launcher.OpenAsync(this.Link);
        }

        public ICommand SaveCommand => this.saveCommand ??= new Command(() => _ = this.SaveTodoAsync());

        private async Task SaveTodoAsync()
        {
            this.IsBusy = true;

            try
            {
                var todo = new Todo
                {
                    Id = this.Id,
                    Name = this.Name,
                    DueDate = this.DueDate,
                    Link = this.Link,
                    Description = this.Description,
                };

                if (this.isNewItem)
                {
                    await this.todoRepository.AddAsync(todo);
                    _ = this.dialogService.DisplayAlertAsync("Success", "Successfully added your TODO", "OK");
                }
                else
                {
                    await this.todoRepository.UpdateAsync(todo);
                    _ = this.dialogService.DisplayAlertAsync("Success", "Successfully updated your TODO", "OK");
                }

                var navigationParameters = new NavigationParameters
                {
                    { "isNewItem", this.isNewItem },
                };
                await this.navigationService.GoBackAsync(navigationParameters);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "SaveTodoAsync failed with exception");
                await this.dialogService.DisplayAlertAsync("Error", "Something went wrong. Please try again later.", "OK");
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}