using Moq;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Core.DataModel;
using TodoApp.Core.DTOs;

namespace TodoApp.Tests.TestCase;

public class TodoServiceCreateTests
{
    private readonly Mock<ITodoRepository> _mockRepo;
    private readonly TodoService _service;

    public TodoServiceCreateTests()
    {
        _mockRepo = new Mock<ITodoRepository>();
        _service = new TodoService(_mockRepo.Object);
    }
    [Fact]
    public async Task AddAsync_ValidTodo_ReturnsCreatedTodo()
    {
        // Arrange
        var createDto = new TodoCreateDto { _title = "New Task", _desc = "Do this", _tag = "work", _priority = "High" };
        var createdModel = new TodoItemModel
        {
            _id = 1,
            _title = createDto._title,
            _desc = createDto._desc,
            _tag = createDto._tag,
            _priority = createDto._priority
        };

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<TodoItemModel>())).ReturnsAsync(createdModel);

        // Act
        var result = await _service.AddAsync(createDto);

        // Assert
        Assert.True(result.success);
        Assert.Equal(201, result.code);
        Assert.Equal("New Task", result.data._title);
    }

    [Fact]
    public async Task AddAsync_EmptyTitle_ReturnsErrorResponse()
    {
        // Arrange
        var createDto = new TodoCreateDto { _title = "" };

        // Act
        var result = await _service.AddAsync(createDto);

        // Assert
        Assert.False(result.success);
        Assert.Equal(400, result.code);
        Assert.Equal("Title cannot be empty", result.message);
    }
}
