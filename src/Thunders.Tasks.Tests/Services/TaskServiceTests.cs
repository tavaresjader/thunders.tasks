using ErrorOr;
using NSubstitute;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Services;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Services
{
    public class TaskServiceTests
    {
        private ITaskRepository _taskRepository;
        private readonly TaskService _service;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public TaskServiceTests()
        {
            _taskRepository = Substitute.For<ITaskRepository>();           
            _cancellationTokenSource = new CancellationTokenSource();
            _service = new TaskService(_taskRepository);
        }

        [Fact]
        public async Task TaskServiceTests_Create_Should_Ok()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.CreateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(GeneralDataBuilder.ID_VALID);

            var result = await _service.CreateAsync(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskServiceTests_Create_Should_Invalid()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.CreateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(GeneralDataBuilder.ID_INVALID);

            var result = await _service.CreateAsync(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task TaskServiceTests_Update_Should_Ok()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(true);

            var result = await _service.UpdateAsync(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskServiceTests_Update_Should_Invalid()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(false);

            var result = await _service.UpdateAsync(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task TaskServiceTests_Delete_Should_Ok()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.DeleteAsync(Arg.Any<int>(), _cancellationTokenSource.Token).Returns(true);

            var result = await _service.DeleteAsync(request.Id, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskServiceTests_Delete_Should_Invalid()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.DeleteAsync(Arg.Any<int>(), _cancellationTokenSource.Token).Returns(false);

            var result = await _service.DeleteAsync(request.Id, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task TaskServiceTests_GetById_Should_Ok()
        {            
            _taskRepository.GetByIdAsync(Arg.Any<int>(), _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build());

            var result = await _service.GetByIdAsync(GeneralDataBuilder.ID_VALID, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskServiceTests_GetById_Should_Invalid()
        {
            var request = new TaskDtoBuilder().Build();

            _taskRepository.GetByIdAsync(Arg.Any<int>(), _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().BuildNull());

            var result = await _service.GetByIdAsync(request.Id, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.NotFound);
        }

        [Fact]
        public async Task TaskServiceTests_GetAll_Should_Ok()
        {
            _taskRepository.GetAllAsync(_cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build(1).ToList());

            var result = await _service.GetAllAsync(_cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskServiceTests_GetAll_Should_Invalid()
        {
            _taskRepository.GetAllAsync(_cancellationTokenSource.Token).Returns(Error.Failure("", "Failture to Get All Tasks"));

            var result = await _service.GetAllAsync(_cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }
    }
}
