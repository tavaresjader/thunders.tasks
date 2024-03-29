using FluentValidation;
using Thunders.Tasks.Application.CreateTask.Commands;

namespace Thunders.Tasks.Application.CreateTask.Validators
{
    public class CreateTaskCommandRequestValidator : AbstractValidator<CreateTaskCommandRequest>
    {
        public CreateTaskCommandRequestValidator()
        {
            RuleFor(source=> source.Title)
                .NotEmpty();

            RuleFor(source => source.EstimateStartDate)
                .NotEmpty();

            When(source => source.EstimateStartDate != null, () =>
            {
                RuleFor(source => source.EstimateStartDate)
                .GreaterThanOrEqualTo(DateTime.Now.Date);

                RuleFor(source => source.EstimateStartDate)
                .GreaterThan(DateTime.MinValue);

                RuleFor(source => source.EstimateStartDate)
                .LessThan(DateTime.MaxValue);
            });

            When(source => source.EstimateEndDate != null, () =>
            {
                RuleFor(source => source.EstimateEndDate)
                .GreaterThanOrEqualTo(DateTime.Now.Date);

                RuleFor(source => source.EstimateEndDate)
                .GreaterThan(DateTime.MinValue);

                RuleFor(source => source.EstimateEndDate)
                .LessThan(DateTime.MaxValue);
            });

            //When(source => source.EstimateStartDate != null && source.EstimateEndDate != null, () =>
            //{

            //});

          
        }
    }
}
