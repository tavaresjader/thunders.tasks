using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.CreateTask.Validators;
using Thunders.Tasks.Application.DeleteTaskById.Validators;
using Thunders.Tasks.Tests.Builders;

namespace Thunders.Tasks.Tests.Application.DeleteTaskById.Validators
{
    public class DeleteTaskByIdCommandRequestValidatorTests
    {
        [Fact]
        public void DeleteTaskByIdCommandRequestValidatorTests_Should_Ok()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder().Build();

            var validator = new DeleteTaskByIdCommandRequestValidator().Validate(request);

            Assert.True(validator.IsValid);
        }

        [Fact]
        public void DeleteTaskByIdCommandRequestValidatorTests_Should_Id_Empty_Invalid()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_EMPTY)
                .Build();

            var validator = new DeleteTaskByIdCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }

        [Fact]
        public void DeleteTaskByIdCommandRequestValidatorTests_Should_Id_Invalid()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();

            var validator = new DeleteTaskByIdCommandRequestValidator().Validate(request);

            Assert.False(validator.IsValid);
        }
    }
}
