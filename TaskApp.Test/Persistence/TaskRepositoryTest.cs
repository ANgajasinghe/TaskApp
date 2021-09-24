using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Persistence;
using Xunit;

namespace TaskApp.Test.Persistence
{
    public class TaskRepositoryTest
    {
        // sample test with MOQ.
        // This type of testing would be better to check some kind of logics or small units of your code.
        // To get real results from below test case you can implement this as an integration testing.

        Mock<ITaskItemRepositoty> _taskRepositoryMock = new();

        [Fact]
        public async Task GetTasksAsync_Test()
        {

            var tasks = new List<TaskItem>()
            {
                new TaskItem
                 {
                     Email = "ag.nayanajith@gmail.com",
                     Name = "Saman Kumara",
                     DueDate = DateTime.Now.AddDays(5),
                     Priority = 1,
                     IsCompleated = false
                 }
            };

            _taskRepositoryMock.Setup(x => x.GetTaskItemsAsync()).ReturnsAsync(tasks);
            var res = await _taskRepositoryMock.Object.GetTaskItemsAsync();

            Assert.NotNull(res);
            Assert.Single(res);

            _taskRepositoryMock.Verify(x => x.GetTaskItemsAsync());


        }


    }
}
