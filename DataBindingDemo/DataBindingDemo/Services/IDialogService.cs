namespace DataBindingDemo.ViewModels
{
    public interface IDialogService
    {
        Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);
    }
}