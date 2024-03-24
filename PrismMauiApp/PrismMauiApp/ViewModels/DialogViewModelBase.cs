namespace PrismMauiApp.ViewModels
{
    public class DialogViewModelBase : BindableBase, IDialogAware
    {
        private string title;
        private DelegateCommand<string> closeDialogCommand;

        public DelegateCommand<string> CloseDialogCommand => this.closeDialogCommand ??= new DelegateCommand<string>(this.CloseDialog);

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public DialogCloseListener RequestClose { get; }

        protected virtual void CloseDialog(string parameter)
        {
            var result = parameter?.ToLower() switch
            {
                "yes" => ButtonResult.OK,
                "no" => ButtonResult.Cancel,
                _ => ButtonResult.None
            };

            this.RequestClose.Invoke(result);

            // Alternative:
            //this.RequestClose.Invoke(new DialogResult(result));
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}