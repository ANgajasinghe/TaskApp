using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Persistence;
using Xunit;

namespace TaskApp.IntegrationTest
{
    public class TaskRepositoryTest : IClassFixture<AppInstance>
    {
        private readonly AppInstance _appInstance;
        private readonly ITaskItemRepositoty _taskItemRepositoty;

        // Arrange
        List<TaskItem> TasksItems = new List<TaskItem>()
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


        public TaskRepositoryTest(AppInstance appInstance)
        {
            _appInstance = appInstance;
            /// _taskItemRepositoty = appInstance.;


        }

        [Fact]
        public async Task InsertTasksAsync_Test()
        {
            try
            {
                //await using var application = new AppInstance();

                //var x = application.Services.GetService<ITaskItemRepositoty>();
                //// act
                // var task = await TaskItemRepositoty.InsertTasksAsync(TasksItems.First());

                //Assert.NotNull(task);
                //Assert.Equal("ag.nayanajith@gmail.com", task.Email);
            }
            catch (Exception)
            {

                throw;
            }

        }



        //[Fact]
        //public async Task SelectAllTasksAsync_Test()
        //{

        //    var tasks = new List<TaskItem>()
        //    {
        //         new TaskItem
        //         {
        //             Email = "ag.nayanajith@gmail.com",
        //             Name = "Saman Kumara",
        //             DueDate = DateTime.Now.AddDays(5),
        //             Priority = 1,
        //             IsCompleated = false
        //         }
        //    };


        //    await _appInstance.TaskItemRepositoty.GetTasksAsync();


        //}


    }
}
