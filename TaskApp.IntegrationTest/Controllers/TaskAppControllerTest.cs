using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskApp.Models;
using Xunit;

namespace TaskApp.IntegrationTest.Controllers
{
    public class TaskAppControllerTest 
    {

        [Fact]
        public async Task POST_Create_TaskItem_Reponds_CREATED() 
        {

            await using var application = new TaskAppApplication();

            var testTask = new TaskItem
            {
                Email = "ag.controllertest@gmail.com",
                Name = "Saman Kumara",
                DueDate = DateTime.Now.AddDays(5),
                Priority = 1,
                IsCompleated = false
            };

            var jsonString = JsonSerializer.Serialize(testTask);

            using var jsonContent = new StringContent(jsonString);
            jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            using var client = application.CreateClient();
            using var response = await client.PostAsync("/api/TaskApp", jsonContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GET_TaskItem_Respods_OK() 
        {
            // arrange
            await using var application = new TaskAppApplication();
            using var client = application.CreateClient();
            var response = await client.GetAsync("/api/TaskApp");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

        }
    }
}
