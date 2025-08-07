using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Core.DataModel;

namespace TodoApp.Tests.TestCase
{
    public class TodoServiceGetAllTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodoService _service;

        public TodoServiceGetAllTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _service = new TodoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTodos()
        {
            // Arrange
            var todos = new List<TodoItemModel>
        {
            new TodoItemModel { _id = 1, _title = "Test 1" },
            new TodoItemModel { _id = 2, _title = "Test 2" }
        };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(todos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.True(result.success);
            Assert.Equal(2, result.data.Count());
        }
    }
}
