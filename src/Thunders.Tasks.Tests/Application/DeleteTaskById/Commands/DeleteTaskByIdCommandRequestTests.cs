using Thunders.Tasks.Tests.Builders;

namespace Thunders.Tasks.Tests.Application.DeleteTaskById.Builders
{
    public class DeleteTaskByIdCommandRequestTests
    {
        [Fact]
        public void DeleteTaskByIdCommandRequestTests_Should_Ok()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder().Build();
            Assert.Equal(request.Id, GeneralDataBuilder.ID_VALID);
        }

        [Fact]
        public void DeleteTaskByIdCommandRequestTests_Id_Empty_Should_Ok()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_EMPTY)
                .Build();

            Assert.Equal(request.Id, GeneralDataBuilder.ID_EMPTY);
        }

        [Fact]
        public void DeleteTaskByIdCommandRequestTests_Id_Invalid_Should_Ok()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();

            Assert.Equal(request.Id, GeneralDataBuilder.ID_INVALID);
        }
        
    }
}
