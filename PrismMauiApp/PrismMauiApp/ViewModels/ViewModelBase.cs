namespace PrismMauiApp.ViewModels
{
    public class ViewModelBase : Prism.Mvvm.BindableBase
    {
        private bool isBusy;
        private string title;

        public bool IsBusy
        {
            get => this.isBusy;
            protected set => this.SetProperty(ref this.isBusy, value);
        }

        public string Title
        {
            get => this.title;
            protected set => this.SetProperty(ref this.title, value);
        }
    }
}