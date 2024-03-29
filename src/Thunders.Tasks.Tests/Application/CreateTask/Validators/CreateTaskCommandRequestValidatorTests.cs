using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.CreateTask.Validators;
using Thunders.Tasks.Tests.Builders;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.CreateTask.Validators
{
    public class CreateTaskCommandRequestValidatorTests
    {
        [Fact]
        public void CreateTaskCommandRequestValidatorTests_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder().Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_Title_Empty_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithTitle(GeneralDataBuilder.TEXT_EMPTY)
                .Build();
            
            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_Description_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithDescription(GeneralDataBuilder.TEXT_EMPTY) 
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateStartDate_Empty_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateStartDate_MinValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MinValue)
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateStartDate_MaxValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MaxValue)
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateStartDate_PastDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateEndDate_MinValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MinValue)
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateEndDate_MaxValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MaxValue)
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateEndDate_PastDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateEndDate_LessThan_EstimateEndDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-2))
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void CreateTaskCommandRequestValidatorTests_EstimateStartDate_GreaterThan_EstimateEndDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var validator = new CreateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }
    }
}
