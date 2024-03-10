using MauiTestingDemo.ViewModels;

namespace MauiTestingDemo
{
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage(TodoListViewModel todoListViewModel)
        {
            this.InitializeComponent();
            this.BindingContext = todoListViewModel;
        }
    }

}
