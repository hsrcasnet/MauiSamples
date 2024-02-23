namespace DataBindingDemo.ViewModels;

public class DialogService : IDialogService
{
    private readonly Page page;

    public DialogService(Page page)
    {
        this.page = page;
    }

    public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
    {
        return this.page.DisplayAlert(title, message, accept, cancel);
    }
}