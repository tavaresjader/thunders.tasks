using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.UpdateTask.Builders
{
    public class UpdateTaskBuilderTests
    {
        [Fact]
        public void UpdateTaskBuilderTests_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder().Build();
            Assert.Equal(request.Id, GeneralDataBuilder.ID_VALID);
            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void UpdateTaskBuilderTests_Title_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithTitle(null)
                .Build();

            Assert.Null(request.Title);
            Assert.Equal(request.Id, GeneralDataBuilder.ID_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void UpdateTaskBuilderTests_Description_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithDescription(null)
                .Build();

            Assert.Equal(request.Id, GeneralDataBuilder.ID_VALID);
            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Null(request.Description);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void UpdateTaskBuilderTests_EstimateStartDate_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(null)
                .Build();

            Assert.Equal(request.Id, GeneralDataBuilder.ID_VALID);
            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Null(request.EstimateStartDate);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void UpdateTaskBuilderTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(null)
                .Build();

            Assert.Equal(request.Id, GeneralDataBuilder.ID_VALID);
            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Null(request.EstimateEndDate);
        }

        [Fact]
        public void UpdateTaskBuilderTests_Id_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithId(0)
                .Build();

            Assert.Equal(0, request.Id);
            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

    }
}
