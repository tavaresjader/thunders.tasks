using Thunders.Tasks.Application.GetTaskById.Validators;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.GetTaskById.Validators
{
    public class GetTaskByIdQueryRequestValidatorTests
    {
        [Fact]
        public void GetTaskByIdQueryRequestValidatorTests_Should_Ok()
        {
            var request = new GetTaskByIdQueryRequestBuilder().Build();

            var validator = new GetTaskByIdQueryRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void GetTaskByIdQueryRequestValidatorTests_Should_Id_Empty_Invalid()
        {
            var request = new GetTaskByIdQueryRequestBuilder()
                .WithId(GeneralDataBuilder.ID_EMPTY)
                .Build();

            var validator = new GetTaskByIdQueryRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void GetTaskByIdQueryRequestValidatorTests_Should_Id_Invalid()
        {
            var request = new GetTaskByIdQueryRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();

            var validator = new GetTaskByIdQueryRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }
    }
}
