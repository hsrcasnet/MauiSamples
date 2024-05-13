using System.Windows.Input;
using Microsoft.Extensions.Logging;
using PrismMauiApp.Services;
using static PrismMauiApp.App;
using NavigationMode = Prism.Navigation.NavigationMode;

namespace PrismMauiApp.ViewModels
{
    public class TodoListViewModel : ViewModelBase, INavigatedAware, IApplicationLifecycleAware
    {
        private readonly ILogger logger;
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly ITodoRepository todoRepository;
        private readonly IDateTime dateTime;

        private IAsyncCommand loadTodosCommand;
        private IAsyncCommand addItemCommand;
        private TodoItemViewModel[] items;

        public TodoListViewModel(
            ILogger<TodoListViewModel> logger,
            INavigationService navigationService,
            IPageDialogService dialogService,
            ITodoRepository todoRepository,
            IDateTime dateTime)
        {
            this.logger = logger;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.todoRepository = todoRepository;
            this.dateTime = dateTime;
            this.Title = "TODO Items";
            this.Items = [];
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();
            if (navigationMode == NavigationMode.New)
            {
                await this.LoadTodosAsync();
            }
            else if (navigationMode == NavigationMode.Back)
            {
                var isNewItem = parameters["isNewItem"] as bool?;
                if (isNewItem.HasValue && isNewItem.Value)
                {
                    await this.LoadTodosAsync();
                }
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // Nothing to do here...
        }

        public TodoItemViewModel[] Items
        {
            get => this.items;
            private set => this.SetProperty(ref this.items, value, nameof(this.Items));
        }

        public IAsyncCommand AddItemCommand
        {
            get => this.addItemCommand ??= new AsyncDelegateCommand(this.NavigateToNewTodoPageAsync);
        }

        private async Task NavigateToNewTodoPageAsync()
        {
            await this.navigationService.NavigateAsync(Pages.NewTodoPage);
        }

        public IAsyncCommand LoadTodosCommand
        {
            get => this.loadTodosCommand ??= new AsyncDelegateCommand(this.LoadTodosAsync);
        }

        private async Task LoadTodosAsync()
        {
            if (this.IsBusy)
            {
                return;
            }

            this.IsBusy = true;

            try
            {
                // Load payload from database
                var todos = (await this.todoRepository.GetAsync(forceRefresh: true)).ToArray();

                // Map Todo objects to TodoItemViewModels
                this.Items = todos
                    .Select(t => new TodoItemViewModel(this.dateTime, this.navigationService, t))
                    .OrderBy(t => t.DueDate)
                    .ToArray();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "LoadTodosAsync failed with exception");
                await this.dialogService.DisplayAlertAsync("Error", "Something went wrong. Please try again later.", "OK");
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public void OnSleep()
        {
            // Nothing to do here
        }

        public void OnResume()
        {
            // Current date/time is used to display "IsOverdue".
            // Therefore, we need to refresh the todo list if the app is resumed.
            _ = this.LoadTodosAsync();
        }
    }
}