namespace DataBindingDemo.Services
{
    public interface INavigationService
    {
        Task PushAsync(string pageName);
    }
}