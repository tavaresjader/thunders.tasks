using FizzWare.NBuilder;
using Thunders.Tasks.Application.UpdateTask.Commands;

namespace Thunders.Tasks.Tests.Builders.Application
{
    public class UpdateTaskCommandRequestBuilder
    {
        private int Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime? EstimateStartDate { get; set; }
        private DateTime? EstimateEndDate { get; set; }

        public UpdateTaskCommandRequestBuilder()
        {
            Id = GeneralDataBuilder.ID_VALID;
            Title = GeneralDataBuilder.TEXT_VALID;
            Description = GeneralDataBuilder.TEXT_VALID;
            EstimateStartDate = GeneralDataBuilder.DATETIME_VALID;
            EstimateEndDate = GeneralDataBuilder.DATETIME_VALID;
        }

        public UpdateTaskCommandRequest Build()
        {
            return Builder<UpdateTaskCommandRequest>.CreateNew()
                .With(w => w.Id = Id)
                .With(w => w.Title = Title)
                .With(w => w.Description = Description)
                .With(w => w.EstimateStartDate = EstimateStartDate)
                .With(w => w.EstimateEndDate = EstimateEndDate)
                .Build();
        }

        public UpdateTaskCommandRequestBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public UpdateTaskCommandRequestBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public UpdateTaskCommandRequestBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public UpdateTaskCommandRequestBuilder WithEstimateStartDate(DateTime? estimateStartDate)
        {
            EstimateStartDate = estimateStartDate;
            return this;
        }

        public UpdateTaskCommandRequestBuilder WithEstimateEndDate(DateTime? estimateEndDate)
        {
            EstimateEndDate = estimateEndDate;
            return this;
        }
    }
}
