using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Services;
using TodoApp.Core.FilterModel;
using TodoApp.Core.Wrapper;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _service;
        public TodoController(TodoService service) => _service = service;

        // ✅ Get paginated list
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] TodoFilterModel? filter,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            filter ??= new TodoFilterModel(); // fallback to empty filter
            var result = await _service.GetPagedAsync(page, pageSize, filter);
            return StatusCode(result.code, result);
        }

        // ✅ Get all (non-paged, for backward compatibility)
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return StatusCode(result.code, result); // ✅ Uses Response<IEnumerable<TodoDto>>
        }

        // ✅ Get detail by ID (using wrapper)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.code, result);
        }

        // ✅ Create new Todo
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TodoCreateDto item)
        {
            var result = await _service.AddAsync(item);
            return StatusCode(result.code, result);
        }

        // ✅ Update Todo
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto item)
        {
            if (id != item._id)
                return BadRequest(Response<string>.ErrorResponse("ID in URL does not match the request body"));

            var result = await _service.UpdateAsync(item);
            return StatusCode(result.code, result);
        }

        // ✅ Delete Todo
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.code, result);
        }
    }
}
