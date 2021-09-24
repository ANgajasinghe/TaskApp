using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Models;

namespace TaskApp.Persistence
{
    public interface ITaskItemRepositoty
    {
        Task<List<TaskItem>> GetTasksAsync();
        Task<TaskItem> InsertTasksAsync(TaskItem task);
    }



    public class TaskItemRepositoty : ITaskItemRepositoty
    {
        private readonly IMongoCollection<TaskItem> _taskItems;

        public TaskItemRepositoty(IMongoDatabaseSettings mongoDatabaseSettings)
        {
            var client = new MongoClient(mongoDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(mongoDatabaseSettings.DatabaseName);

            _taskItems = database.GetCollection<TaskItem>(mongoDatabaseSettings.CollectionName);

        }

        public Task<List<TaskItem>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TaskItem> InsertTasksAsync(TaskItem task)
        {
            await _taskItems.InsertOneAsync(task);
            return task;
        }


    }
}