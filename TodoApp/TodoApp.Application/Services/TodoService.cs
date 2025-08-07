
using TodoApp.Application.Interfaces;
using TodoApp.Core.DataModel;
using TodoApp.Core.DTOs;
using TodoApp.Core.FilterModel;
using TodoApp.Core.Mappers;
using TodoApp.Core.Wrapper;
using System.Linq;

namespace TodoApp.Application.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repository;
        public TodoService(ITodoRepository repository) => _repository = repository;

        // ✅ Get all
        public async Task<Response<IEnumerable<TodoDto>>> GetAllAsync()
        {
            var todos = await _repository.GetAllAsync();
            var dtoList = todos.Select(TodoMapper.ToDto);
            return Response<IEnumerable<TodoDto>>.SuccessResponse(dtoList);
        }

        // ✅ Get paginated list
        public async Task<PagingResponse<IEnumerable<TodoDto>>> GetPagedAsync(int page, int pageSize, TodoFilterModel filter)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var todos = await _repository.GetPagedAsync(page, pageSize, filter); // should return List<Todo>
            var totalRecords = await _repository.CountAsync(filter); // should count with filter applied

            var dtoList = todos.Select(TodoMapper.ToDto);

            return new PagingResponse<IEnumerable<TodoDto>>(dtoList, page, pageSize, totalRecords);
        }


        // ✅ Get by ID
        public async Task<Response<TodoDto>> GetByIdAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null)
                return Response<TodoDto>.ErrorResponse("Todo not found", 404);

            return Response<TodoDto>.SuccessResponse(TodoMapper.ToDto(todo));
        }

        // ✅ Add new
        public async Task<Response<TodoDto>> AddAsync(TodoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto._title))
                return Response<TodoDto>.ErrorResponse("Title cannot be empty", 400);

            var entity = TodoMapper.FromCreateDto(dto);
            var created = await _repository.AddAsync(entity);

            return Response<TodoDto>.SuccessResponse(TodoMapper.ToDto(created), "Todo created successfully", 201);
        }

        // ✅ Update existing
        public async Task<Response<TodoDto>> UpdateAsync(TodoUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto._id);
            if (existing == null)
                return Response<TodoDto>.ErrorResponse("Todo not found", 404);

            if (string.IsNullOrWhiteSpace(dto._title))
                return Response<TodoDto>.ErrorResponse("Title cannot be empty", 400);

            TodoMapper.UpdateEntity(existing, dto);
            await _repository.UpdateAsync(existing);

            return Response<TodoDto>.SuccessResponse(TodoMapper.ToDto(existing), "Todo updated successfully");
        }

        // ✅ Delete
        public async Task<Response<string>> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return Response<string>.ErrorResponse("Todo not found", 404);

            await _repository.DeleteAsync(id);
            return Response<string>.SuccessResponse("Todo deleted successfully");
        }
    }
}

