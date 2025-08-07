
using TodoApp.Core.DataModel;
using TodoApp.Core.FilterModel;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<int> CountAsync(TodoFilterModel filter);
        Task<IEnumerable<TodoItemModel>> GetPagedAsync(int page, int pageSize, TodoFilterModel filter);
        IQueryable<TodoItemModel> Query();
        Task<List<TodoItemModel>> GetAllAsync();
        Task<TodoItemModel?> GetByIdAsync(int id);
        Task<TodoItemModel> AddAsync(TodoItemModel item);
        Task UpdateAsync(TodoItemModel item);
        Task DeleteAsync(int id);
    }
}
