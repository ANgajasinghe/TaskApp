using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TaskApp.Test;

public class TasksTest
{

    // I mock a mediator and it will be the global object since I am using XUnit 
    // This type of mocking cannot get expected result we do not know our code is executed successfully or data gave been committed in to database really.
    // That why we need intergeneration testing.
    // But using these type tests we can check our logics

    Mock<IMediator> _mediatorMock = new();

    [Fact]
    public async Task CreateATask()
    {
        // Arrange
        CreateTaskCommand command = new()
        {
            Id = "sasa",
            Email = "ag.anayanajith@gmail.com",
            Name = "Some task",
            DueDate = DateTime.Now.AddDays(1),
            Priority = 1,
            IsCompleated = false
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTaskCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _mediatorMock.Object.Send(command);


        Assert.Equal(1, result);

        _mediatorMock.Verify(x => x.Send(It.IsAny<CreateTaskCommand>(), It.IsAny<CancellationToken>()));

    }
}

