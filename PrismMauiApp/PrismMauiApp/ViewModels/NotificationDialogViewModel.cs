namespace PrismMauiApp.ViewModels
{
    public class NotificationDialogViewModel : DialogViewModelBase
    {
        private string message;

        public NotificationDialogViewModel()
        {
            this.Title = "Notification Dialog";
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            this.Message = parameters.GetValue<string>("message");
        }

        public string Message
        {
            get => this.message;
            set => this.SetProperty(ref this.message, value);
        }
    }
}
