using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Core.DataModel;
using TodoApp.Core.FilterModel;

namespace TodoApp.Tests.TestCase
{
    public class TodoServiceGetPagedTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodoService _service;

        public TodoServiceGetPagedTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _service = new TodoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsPagedTodos()
        {
            // Arrange
            int page = 1;
            int pageSize = 2;
            var filter = new TodoFilterModel();

            var todos = new List<TodoItemModel>
        {
            new TodoItemModel { _id = 1, _title = "Page 1" },
            new TodoItemModel { _id = 2, _title = "Page 2" }
        };

            _mockRepo.Setup(r => r.GetPagedAsync(page, pageSize, filter)).ReturnsAsync(todos);
            _mockRepo.Setup(r => r.CountAsync(filter)).ReturnsAsync(5); // total items with filter

            // Act
            var result = await _service.GetPagedAsync(page, pageSize, filter);

            // Assert
            Assert.Equal(2, result.data.Count());
            Assert.Equal(1, result.current_page);
            Assert.Equal(5, result.total_records);
            Assert.Equal(2, result.page_size);
        }
    }
}
