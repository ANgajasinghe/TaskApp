using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TaskApp.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public string Email { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public int Priority { get; set; }
        public bool IsCompleated { get; set; }
    }
}