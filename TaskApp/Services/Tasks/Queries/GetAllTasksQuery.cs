using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Persistence;

namespace TaskApp.Services.Tasks.Queries
{
    public class GetAllTasksQuery : IRequest<List<TaskItem>>
    {
        
    }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskItem>>
    {
        private readonly ITaskItemRepositoty _taskItemRepositoty;

        public GetAllTasksQueryHandler(ITaskItemRepositoty taskItemRepositoty)
        {
            _taskItemRepositoty = taskItemRepositoty;
        }

        public async Task<List<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskItemRepositoty.GetTaskItemsAsync();
        }
    }
}
