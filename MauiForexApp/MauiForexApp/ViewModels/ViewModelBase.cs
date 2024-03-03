namespace ForexApp.ViewModels
{
    /// <summary>
    /// This is just a very simple implementation of a viewmodel base class.
    /// </summary>
    public abstract class ViewModelBase : BindableObject
    {
        private string title;

        public string Title
        {
            get => this.title;
            protected set
            {
                this.title = value;
                this.OnPropertyChanged(nameof(this.Title));
            }
        }
    }
}