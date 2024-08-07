﻿namespace PrismMauiApp
{
    public partial class App
    {
        public static class Pages
        {
            public const string TodoListPage = nameof(PrismMauiApp.Views.TodoListPage);
            public const string TodoDetailPage = nameof(PrismMauiApp.Views.TodoDetailPage);
            public const string NewTodoPage = nameof(PrismMauiApp.Views.NewTodoPage);
            public const string AboutPage = nameof(PrismMauiApp.Views.AboutPage);
            public const string NavigationPage = nameof(Microsoft.Maui.Controls.NavigationPage);
        }

        public static class Dialogs
        {
            public const string NotificationDialog = nameof(PrismMauiApp.Views.NotificationDialog);
        }
    }
}
