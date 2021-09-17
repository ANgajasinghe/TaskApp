using FluentAssertions;
using TaskApp.Exceptions;
using TaskApp.Test;
using Xunit;

namespace TaskApp.IntegrationTest.Services.Tasks.Commands
{
    public class CreateTeskCommandTest : IClassFixture<AppInstance>
    {
        private readonly AppInstance _appInstance;

        public CreateTeskCommandTest(AppInstance appInstance)
        {
            _appInstance = appInstance;
        }

        [Fact]
        public async void ShouldRequireMinimumFields()
        {
            var command = new CreateTaskCommand();
            var rexception = await FluentActions.Invoking(() => _appInstance.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            // var res = _appInstance.SendAsync(command).GetAwaiter().GetResult();

            // Assert.Equal(1, res);
        }
    }
}
