using Thunders.Tasks.Core.Dtos;

namespace Thunders.Tasks.Tests.Builders.Dtos
{
    public class TaskDtoBuilder
    {
        private int Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime? EstimateStartDate { get; set; }
        private DateTime? EstimateEndDate { get; set; }
        private DateTime CreatedAt { get; set; }
        private DateTime? UpdatedAt { get; set; }


        public TaskDtoBuilder()
        {
            Id = GeneralDataBuilder.ID_VALID;
            Title = GeneralDataBuilder.TEXT_VALID;
            Description = GeneralDataBuilder.TEXT_VALID;
            EstimateStartDate = GeneralDataBuilder.DATETIME_VALID;
            EstimateEndDate = GeneralDataBuilder.DATETIME_VALID.AddDays(1);
            CreatedAt = GeneralDataBuilder.DATETIME_VALID;
            UpdatedAt = GeneralDataBuilder.DATETIME_VALID;
        }

        public TaskDto Build()
        {
            return Builder<TaskDto>.CreateNew()
                .With(w => w.Id, Id)
                .With(w => w.Title, Title)
                .With(w => w.Description, Description)
                .With(w => w.EstimateStartDate, EstimateStartDate)
                .With(w => w.EstimateEndDate, EstimateEndDate)
                .With(w => w.CreatedAt, CreatedAt)
                .With(w => w.UpdatedAt, UpdatedAt)
                .Build();
        }

        public IList<TaskDto> Build(int listSize)
        {
            if (listSize is 0)
                return new List<TaskDto>();

            return Builder<TaskDto>.CreateListOfSize(listSize)                
                .All()
                .With(w => w.Id, Id)
                .With(w => w.Title, Title)
                .With(w => w.Description, Description)
                .With(w => w.EstimateStartDate, EstimateStartDate)
                .With(w => w.EstimateEndDate, EstimateEndDate)
                .With(w => w.CreatedAt, CreatedAt)
                .With(w => w.UpdatedAt, UpdatedAt)
                .Build();
        }

        public TaskDto BuildNull()
        {
            return null;
        }

        public TaskDtoBuilder WithId(int id)
        {
            Id = id;
            return this;
        }


        public TaskDtoBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public TaskDtoBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public TaskDtoBuilder WithEstimateStartDate(DateTime? estimateStartDate)
        {
            EstimateStartDate = estimateStartDate;
            return this;
        }

        public TaskDtoBuilder WithEstimateEndDate(DateTime? estimateEndDate)
        {
            EstimateEndDate = estimateEndDate;
            return this;
        }
    }
}
