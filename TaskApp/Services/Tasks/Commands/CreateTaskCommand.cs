using MediatR;

namespace TaskApp.Test
{
    public class CreateTaskCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public bool IsCompleated { get; set; }
    }


    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {


        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if (request.Email != null)
            {
                return 1;
            }

            return 0;
        }
    }
}