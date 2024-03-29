﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using PrismMauiApp.Services;
using static PrismMauiApp.App;
using NavigationMode = Prism.Navigation.NavigationMode;

namespace PrismMauiApp.ViewModels
{
    public class TodoListViewModel : ViewModelBase, INavigatedAware
    {
        private readonly ILogger logger;
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly ITodoRepository todoRepository;

        private Command loadTodosCommand;
        private Command addItemCommand;
        private ObservableCollection<TodoItemViewModel> items;

        public TodoListViewModel(
            ILogger<TodoListViewModel> logger,
            INavigationService navigationService,
            IPageDialogService dialogService,
            ITodoRepository todoRepository)
        {
            this.logger = logger;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.todoRepository = todoRepository;

            this.Title = "TODO Items";
            this.Items = new ObservableCollection<TodoItemViewModel>();
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

        public ObservableCollection<TodoItemViewModel> Items
        {
            get => this.items;
            private set => this.SetProperty(ref this.items, value, nameof(this.Items));
        }

        public ICommand LoadTodosCommand
        {
            get => this.loadTodosCommand ??= new Command(async () =>
            {
                await this.LoadTodosAsync();
            });
        }

        public ICommand AddItemCommand
        {
            get => this.addItemCommand ??= new Command(async () =>
            {
                await this.navigationService.NavigateAsync(Pages.NewTodoPage);
            });
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
                // Load payload from backend
                var todos = (await this.todoRepository.GetAsync(forceRefresh: true)).ToList();

                // Refresh the list by clearing all and adding one after the other
                this.Items.Clear();
                foreach (var todo in todos)
                {
                    var todoItemViewModel = new TodoItemViewModel(this.navigationService, todo);
                    this.Items.Add(todoItemViewModel);
                }

                // Refresh the whole list in one call
                //this.Items = new ObservableCollection<TodoItemViewModel>(todos.Select(t => new TodoItemViewModel(this.navigationService, t)));
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
    }
}