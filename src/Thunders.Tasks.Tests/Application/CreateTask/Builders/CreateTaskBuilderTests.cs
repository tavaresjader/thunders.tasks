using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.CreateTask.Validators;
using Thunders.Tasks.Tests.Builders;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.CreateTask.Builders
{
    public class CreateTaskBuilderTests
    {
        [Fact]
        public void CreateTaskBuilderTests_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder().Build();
            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void CreateTaskBuilderTests_Title_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithTitle(null)
                .Build();            

            Assert.Null(request.Title);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void CreateTaskBuilderTests_Description_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithDescription(null)
                .Build();

            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Null(request.Description);            
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void CreateTaskBuilderTests_EstimateStartDate_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(null)
                .Build();

            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Null(request.EstimateStartDate);
            Assert.Equal(request.EstimateEndDate, GeneralDataBuilder.DATETIME_VALID);
        }

        [Fact]
        public void CreateTaskBuilderTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(null)
                .Build();

            Assert.Equal(request.Title, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.Description, GeneralDataBuilder.TEXT_VALID);
            Assert.Equal(request.EstimateStartDate, GeneralDataBuilder.DATETIME_VALID);
            Assert.Null(request.EstimateEndDate);
        }
    }
}
