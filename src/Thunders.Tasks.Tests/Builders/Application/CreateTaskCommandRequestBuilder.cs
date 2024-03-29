using FizzWare.NBuilder;
using Thunders.Tasks.Application.CreateTask.Commands;

namespace Thunders.Tasks.Tests.Builders.Application
{
    public class CreateTaskCommandRequestBuilder
    {
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime? EstimateStartDate { get; set; }
        private DateTime? EstimateEndDate { get; set; }

        public CreateTaskCommandRequestBuilder()
        {
            Title = GeneralDataBuilder.TEXT_VALID;
            Description = GeneralDataBuilder.TEXT_VALID;
            EstimateStartDate = GeneralDataBuilder.DATETIME_VALID;
            EstimateEndDate = GeneralDataBuilder.DATETIME_VALID;
        }

        public CreateTaskCommandRequest Build()
        {
            return Builder<CreateTaskCommandRequest>.CreateNew()
                .With(w => w.Title = Title)
                .With(w => w.Description = Description)
                .With(w => w.EstimateStartDate = EstimateStartDate)
                .With(w => w.EstimateEndDate = EstimateEndDate)
                .Build();
        }

        public CreateTaskCommandRequestBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public CreateTaskCommandRequestBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public CreateTaskCommandRequestBuilder WithEstimateStartDate(DateTime? estimateStartDate)
        {
            EstimateStartDate = estimateStartDate;
            return this;
        }

        public CreateTaskCommandRequestBuilder WithEstimateEndDate(DateTime? estimateEndDate)
        {
            EstimateEndDate = estimateEndDate;
            return this;
        }
    }
}
