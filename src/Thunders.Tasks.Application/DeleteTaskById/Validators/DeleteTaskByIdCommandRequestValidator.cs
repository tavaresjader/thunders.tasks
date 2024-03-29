using FluentValidation;
using Thunders.Tasks.Application.DeleteTaskById.Commands;

namespace Thunders.Tasks.Application.DeleteTaskById.Validators
{
    public class DeleteTaskByIdCommandRequestValidator : AbstractValidator<DeleteTaskByIdCommandRequest>
    {
        public DeleteTaskByIdCommandRequestValidator()
        {
            RuleFor(source => source.Id).NotEmpty();
            RuleFor(source => source.Id).GreaterThan(0);
        }
    }
}
