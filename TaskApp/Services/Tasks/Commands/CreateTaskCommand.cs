using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskApp.Application.Mappings;
using TaskApp.Models;
using TaskApp.Persistence;

namespace TaskApp.Test
{
    public class CreateTaskCommand : IRequest<int>, IMapTo<TaskItem>
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public int Priority { get; set; }
        public bool IsCompleated { get; set; }
    }


    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly ITaskItemRepositoty _taskItemRepositoty;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(ITaskItemRepositoty taskItemRepositoty,IMapper mapper)
        {
            _taskItemRepositoty = taskItemRepositoty;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _taskItemRepositoty.InsertTaskAsync(_mapper.Map<TaskItem>(request));
                return 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
           
        }
    }

    public class CreateTaskCommandValidatior : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidatior()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("This is not a valid email address.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(v => v.IsCompleated)
                .Must(v => v == false).WithMessage("IsComppleated should be false");

        }
    }
}