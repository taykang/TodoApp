using Moq;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Core.DataModel;
using TodoApp.Core.DTOs;

namespace TodoApp.Tests.TestCase;

public class TodoServiceUpdateTests
{
    private readonly Mock<ITodoRepository> _mockRepo;
    private readonly TodoService _service;

    public TodoServiceUpdateTests()
    {
        _mockRepo = new Mock<ITodoRepository>();
        _service = new TodoService(_mockRepo.Object);
    }
    [Fact]
    public async Task UpdateAsync_ValidTodo_ReturnsUpdatedTodo()
    {
        // Arrange
        var updateDto = new TodoUpdateDto
        {
            _id = 1,
            _title = "Updated Title",
            _desc = "Updated Description"
        };

        var existing = new TodoItemModel
        {
            _id = 1,
            _title = "Old Title",
            _desc = "Old Description"
        };

        _mockRepo.Setup(r => r.GetByIdAsync(updateDto._id)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<TodoItemModel>())).Returns(Task.CompletedTask);

        // Act
        var result = await _service.UpdateAsync(updateDto);

        // Assert
        Assert.True(result.success);
        Assert.Equal("Updated Title", result.data._title);
        Assert.Equal("Todo updated successfully", result.message);
    }

    [Fact]
    public async Task UpdateAsync_TodoNotFound_ReturnsNotFound()
    {
        // Arrange
        var updateDto = new TodoUpdateDto { _id = 99, _title = "Doesn't matter" };
        _mockRepo.Setup(r => r.GetByIdAsync(updateDto._id)).ReturnsAsync((TodoItemModel)null);

        // Act
        var result = await _service.UpdateAsync(updateDto);

        // Assert
        Assert.False(result.success);
        Assert.Equal(404, result.code);
        Assert.Equal("Todo not found", result.message);
    }

    [Fact]
    public async Task UpdateAsync_EmptyTitle_ReturnsBadRequest()
    {
        // Arrange
        var updateDto = new TodoUpdateDto { _id = 1, _title = "" };

        var existing = new TodoItemModel
        {
            _id = 1,
            _title = "Old title"
        };

        _mockRepo.Setup(r => r.GetByIdAsync(updateDto._id)).ReturnsAsync(existing);

        // Act
        var result = await _service.UpdateAsync(updateDto);

        // Assert
        Assert.False(result.success);
        Assert.Equal(400, result.code);
        Assert.Equal("Title cannot be empty", result.message);
    }

}
