using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Models;

namespace TaskApp.Persistence
{
    public interface ITaskItemRepositoty
    {
        Task<List<TaskItem>> GetTaskItemsAsync();
        Task<TaskItem> GetTaskItemAsync(string id);
        Task<TaskItem> InsertTaskAsync(TaskItem task);
        Task<TaskItem> UpdateTaskItemAsync(string id, TaskItem taskItemIn);
        Task DeleteAsync(string id);
    }


    // This is a repository that has responsibility to handle CRUD operations no any flow logic here

    public class TaskItemRepositoty : ITaskItemRepositoty
    {
        private readonly IMongoCollection<TaskItem> _taskItems;

        public TaskItemRepositoty(IMongoDatabaseSettings mongoDatabaseSettings)
        {
            var client = new MongoClient(mongoDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(mongoDatabaseSettings.DatabaseName);

            _taskItems = database.GetCollection<TaskItem>(mongoDatabaseSettings.CollectionName);

        }

   
        public async Task<List<TaskItem>> GetTaskItemsAsync()
            => await _taskItems.Find(x=> true).ToListAsync();

        public async Task<TaskItem> GetTaskItemAsync(string id)
            => await _taskItems.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<TaskItem> InsertTaskAsync(TaskItem task)
        {
            await _taskItems.InsertOneAsync(task);
            return task;
        }

        public async Task<TaskItem> UpdateTaskItemAsync(string id, TaskItem taskItemIn)
        {
            await _taskItems.ReplaceOneAsync(x=>x.Id == id, taskItemIn);
            return taskItemIn;
        }

        public async Task DeleteAsync(string id)
            => await _taskItems.DeleteOneAsync(id);

    }
}