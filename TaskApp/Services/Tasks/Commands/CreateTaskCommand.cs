using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskApp.Test
{
    public class CreateTaskCommand : IRequest<int>
    {
        public string? Id { get; set; }
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