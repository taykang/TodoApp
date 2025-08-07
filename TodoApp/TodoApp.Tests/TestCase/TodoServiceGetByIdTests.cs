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
    public class TodoServiceGetByIdTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodoService _service;

        public TodoServiceGetByIdTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _service = new TodoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTodo_WhenFound()
        {
            // Arrange
            var todo = new TodoItemModel { _id = 1, _title = "Test 1" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.True(result.success);
            Assert.Equal(1, result.data._id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound_WhenTodoDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((TodoItemModel)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.False(result.success);
            Assert.Equal(404, result.code);
            Assert.Equal("Todo not found", result.message);
        }
    }
}
