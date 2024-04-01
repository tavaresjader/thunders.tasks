using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.GetAllTasks.Queries;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Builders.Application
{
    public class GetAllTasksQueryResponseBuilder
    {
        private int Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime? EstimateStartDate { get; set; }
        private DateTime? EstimateEndDate { get; set; }
        private DateTime CreatedAt { get; set; }
        private DateTime? UpdatedAt { get; set; }

        public GetAllTasksQueryResponseBuilder()
        {
            Id = GeneralDataBuilder.ID_VALID;
            Title = GeneralDataBuilder.TEXT_VALID;
            Description = GeneralDataBuilder.TEXT_VALID;
            EstimateStartDate = GeneralDataBuilder.DATETIME_VALID;
            EstimateEndDate = GeneralDataBuilder.DATETIME_VALID;
            CreatedAt = GeneralDataBuilder.DATETIME_VALID;
            UpdatedAt = GeneralDataBuilder.DATETIME_VALID;
        }

        public IList<GetAllTasksQueryResponse> Build(int size)
        {
            return Builder<GetAllTasksQueryResponse>.CreateListOfSize(size)
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

        public GetAllTasksQueryResponseBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public GetAllTasksQueryResponseBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public GetAllTasksQueryResponseBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public GetAllTasksQueryResponseBuilder WithEstimateStartDate(DateTime? estimateStartDate)
        {
            EstimateStartDate = estimateStartDate;
            return this;
        }

        public GetAllTasksQueryResponseBuilder WithEstimateEndDate(DateTime? estimateEndDate)
        {
            EstimateEndDate = estimateEndDate;
            return this;
        }
    }
}
