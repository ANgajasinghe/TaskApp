using System;
using System.Collections.Generic;
using System.Linq;
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

        }

        [Fact]
        public async Task Insert_Task_Test()
        {
           
                // act
                var task = await _appInstance.TaskItemRepositoty.InsertTaskAsync(TasksItems.First());

                Assert.NotNull(task);
                Assert.Equal("ag.nayanajith@gmail.com", task.Email);
        }



        [Fact]
        public async Task Get_TaskItems_Test()
        {
            // Arrange
            var email = TasksItems.First().Email;

            // Act
            var data = await _appInstance.TaskItemRepositoty.GetTaskItemsAsync();

            // Assert
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
            Assert.True(data.First().Email == email);
        }


        [Fact]
        public async Task Get_Task_Item() 
        {
            string id = "614daddea649706158b81d88";
            TaskItem taskItem = await _appInstance.TaskItemRepositoty.GetTaskItemAsync(id);

            Assert.NotNull(taskItem);
        }

        [Fact]
        public async Task Update_Task_Item() 
        {
            // Arrange
            string id = "614daddea649706158b81d88";
            TaskItem taskItemIn = await _appInstance.TaskItemRepositoty.GetTaskItemAsync(id);
            taskItemIn.Name = "Akalanka Nayanajith";

            // Act
            TaskItem result = await _appInstance.TaskItemRepositoty.UpdateTaskItemAsync(id, taskItemIn);

            //Assert
            Assert.NotNull(result);
            
        }

        public async Task Delete_Task_Item() 
        {
            // Arrange
            string id = "614daddea649706158b81d88";

            // Act
            await _appInstance.TaskItemRepositoty.DeleteAsync(id);
            TaskItem result = await _appInstance.TaskItemRepositoty.GetTaskItemAsync(id);

            //Assert
            Assert.Null(result);

        }

       


    }
}
