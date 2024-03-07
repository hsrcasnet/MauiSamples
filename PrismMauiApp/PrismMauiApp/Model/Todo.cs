namespace PrismMauiApp.Model
{
    public class Todo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool Done { get; set; }
    }
}