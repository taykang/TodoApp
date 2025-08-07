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
    public class TodoServiceDeleteTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodoService _service;

        public TodoServiceDeleteTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _service = new TodoService(_mockRepo.Object);
        }

        [Fact]
        public async Task DeleteAsync_TodoExists_ReturnsSuccess()
        {
            // Arrange
            var todo = new TodoItemModel { _id = 1, _title = "Test" };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result.success);
            Assert.Equal("Todo deleted successfully", result.data);
        }

        [Fact]
        public async Task DeleteAsync_TodoNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((TodoItemModel)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result.success);
            Assert.Equal(404, result.code);
            Assert.Equal("Todo not found", result.message);
        }
    }
}
