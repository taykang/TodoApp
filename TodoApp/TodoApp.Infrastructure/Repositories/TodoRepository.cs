using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Core.Constant;
using TodoApp.Core.DataModel;
using TodoApp.Core.FilterModel;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;
    public TodoRepository(AppDbContext context) => _context = context;

    public async Task<int> CountAsync(TodoFilterModel filter)
    {
        var query = _context.TodoItems.AsQueryable();

        if (!string.IsNullOrEmpty(filter._search_text))
        {
            query = query.Where(t =>
                t._title.Contains(filter._search_text) ||
                t._desc.Contains(filter._search_text) ||
                t._tag.Contains(filter._search_text)
            );
        }

        if (!string.IsNullOrEmpty(filter._priority))
        {
            query = query.Where(t => t._priority.Contains(filter._priority));
        }

        if (filter._isCompleted)
        {
            query = query.Where(t => t._isCompleted == filter._isCompleted);
        }

        if (filter._year > 0)
        {
            query = query.Where(t => t._submitted_date.Year == filter._year);
        }

        if (filter._month > 0)
        {
            query = query.Where(t => t._submitted_date.Month == filter._month);
        }

        return await query.CountAsync();
    }

    public IQueryable<TodoItemModel> Query()
    {
        return _context.TodoItems.AsQueryable(); // 👈 exposes queryable
    }
    public async Task<IEnumerable<TodoItemModel>> GetPagedAsync(int page, int pageSize, TodoFilterModel filter)
    {
        var query = _context.TodoItems.AsQueryable();

        // 🔍 Search
        if (!string.IsNullOrEmpty(filter._search_text))
        {
            query = query.Where(t =>
                t._title.Contains(filter._search_text) ||
                t._desc.Contains(filter._search_text) ||
                t._tag.Contains(filter._search_text)
            );
        }

        // 📅 Filter by year/month
        if (filter._year > 0)
            query = query.Where(t => t._submitted_date.Year == filter._year);

        if (!string.IsNullOrEmpty(filter._priority))
        {
            query = query.Where(t => t._priority.Contains(filter._priority));
        }

        if (filter._isCompleted)
        {
            query = query.Where(t => t._isCompleted == filter._isCompleted);
        }

        if (filter._month > 0)
            query = query.Where(t => t._submitted_date.Month == filter._month);

        // 🔃 Sorting
        if (!string.IsNullOrEmpty(filter._sort_field))
        {
            switch (filter._sort_field)
            {
                case "_title":
                    query = filter._sort_direction == "desc"
                        ? query.OrderByDescending(t => t._title)
                        : query.OrderBy(t => t._title);
                    break;
                case "_priority":
                    query = filter._sort_direction == "desc"
                        ? query.OrderByDescending(t => t._priority == TodoPriority.High ? 3 :
                                                     t._priority == TodoPriority.Medium ? 2 :
                                                     t._priority == TodoPriority.Low ? 1 : 0)
                        : query.OrderBy(t => t._priority == TodoPriority.High ? 3 :
                                             t._priority == TodoPriority.Medium ? 2 :
                                             t._priority == TodoPriority.Low ? 1 : 0);
                    break;
                case "_submitted_date":
                    query = filter._sort_direction == "desc"
                        ? query.OrderByDescending(t => t._submitted_date)
                        : query.OrderBy(t => t._submitted_date);
                    break;
                default:
                    query = query.OrderByDescending(t => t._submitted_date); // default sort
                    break;
            }
        }
        else
        {
            query = query.OrderByDescending(t => t._submitted_date); // default sort
        }

        // Pagination
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<TodoItemModel>> GetAllAsync() => await _context.TodoItems.ToListAsync();

    public async Task<TodoItemModel?> GetByIdAsync(int id) => await _context.TodoItems.FindAsync(id);

    public async Task<TodoItemModel> AddAsync(TodoItemModel item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(TodoItemModel item)
    {
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo != null)
        {
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
