using Thunders.Tasks.Application.UpdateTask.Validators;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.UpdateTask.Validators
{
    public class UpdateTaskCommandRequestValidatorTests
    {
        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder().Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_Id_Empty_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_EMPTY)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_Id_Invalid_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_Title_Empty_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithTitle(GeneralDataBuilder.TEXT_EMPTY)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_Description_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithDescription(GeneralDataBuilder.TEXT_EMPTY)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateStartDate_Empty_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateStartDate_MinValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MinValue)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateStartDate_MaxValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MaxValue)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateStartDate_PastDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateEndDate_MinValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MinValue)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateEndDate_MaxValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MaxValue)
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateEndDate_PastDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateEndDate_LessThan_EstimateEndDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-2))
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void UpdateTaskCommandRequestValidatorTests_EstimateStartDate_GreaterThan_EstimateEndDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var validator = new UpdateTaskCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }
    }
}
