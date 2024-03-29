using FluentValidation;
using Thunders.Tasks.Application.GetTaskById.Queries;

namespace Thunders.Tasks.Application.GetTaskById.Validators
{
    public class GetTaskByIdQueryRequestValidator : AbstractValidator<GetTaskByIdQueryRequest>
    {
        public GetTaskByIdQueryRequestValidator()
        {
            RuleFor(source => source).NotEmpty();
            RuleFor(source => source.Id).GreaterThan(0);
        }
    }
}
