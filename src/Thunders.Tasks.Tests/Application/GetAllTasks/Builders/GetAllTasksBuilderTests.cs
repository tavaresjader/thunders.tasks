using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.GetAllTasks.Builders
{
    public class GetAllTasksBuilderTests
    {
        [Fact]
        public void GetAllTasksBuilderTests_Should_Ok()
        {
            int listSize = 10;

            var request = new GetAllTasksQueryResponseBuilder().Build(listSize);

            Assert.Equal(listSize, request.Count);

            Assert.Equal(request.First().Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.First().Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.First().EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.First().EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);

            Assert.Equal(request.Last().Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Last().Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Last().EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.Last().EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void GetAllTasksBuilderTests_Title_Empty_Should_Ok()
        {
            var request = new GetAllTasksQueryResponseBuilder()
                .WithTitle(null)
                .Build(1);

            Assert.Null(request.First().Title);
            Assert.Equal(request.First().Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.First().EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.First().EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void GetAllTasksBuilderTests_Description_Empty_Should_Ok()
        {
            var request = new GetAllTasksQueryResponseBuilder()
                .WithDescription(null)
                .Build(1);

            Assert.Equal(request.First().Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Null(request.First().Description);
            Assert.Equal(request.First().EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.First().EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void GetAllTasksBuilderTests_EstimateStartDate_Empty_Should_Ok()
        {
            var request = new GetAllTasksQueryResponseBuilder()
                .WithEstimateStartDate(null)
                .Build(1);

            Assert.Equal(request.First().Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.First().Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Null(request.First().EstimateStartDate);
            Assert.Equal(request.First().EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void GetAllTasksBuilderTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new GetAllTasksQueryResponseBuilder()
                .WithEstimateEndDate(null)
                .Build(1);

            Assert.Equal(request.First().Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.First().Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.First().EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Null(request.First().EstimateEndDate);
        }
    }
}
