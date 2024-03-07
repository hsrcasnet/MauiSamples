using PrismMauiApp.Model;

namespace PrismMauiApp.Services
{
    public class TodoRepositorySqlite : ITodoRepository
    {
        // TODO: Implement repository with SQLite
        // https://github.com/dotnet/maui-samples/blob/main/8.0/Data/TodoSQLite/TodoSQLite/Data/TodoItemDatabase.cs

        public Task<bool> AddAsync(Todo item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Todo>> GetAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Todo item)
        {
            throw new NotImplementedException();
        }
    }
}
