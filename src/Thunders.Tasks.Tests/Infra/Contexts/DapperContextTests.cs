using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System.Data;
using Thunders.Tasks.Core.Data;
using Thunders.Tasks.Infra.Data;

namespace Thunders.Tasks.Tests.Infra.Contexts
{
    public class DapperContextTests
    {
        private IConfiguration _configuration;
        private DapperContext _dapperContext;

        public DapperContextTests()
        {
            _configuration = Substitute.For<IConfiguration>();
            _dapperContext = new DapperContext(_configuration);
        }

        [Fact]
        public void DapperContextTests_CreateConnection_Should_Ok()
        {
            string connectionString = "Server:{LocalHost}";

            _configuration.GetValue<string>(Arg.Any<string>()).Returns(connectionString);

            _dapperContext.CreateConnection();

            var result = _dapperContext.GetConnection();

            Assert.NotNull(result);
        }

        [Fact]
        public void TaskServiceTests_CreateConnection_ConnectionString_Empty_Should_Invalid()
        {
            _configuration.GetValue<string>(Arg.Any<string>()).ReturnsNull();

            _dapperContext.CreateConnection().Throws<ApplicationException>();
            
            Assert.Throws<ApplicationException>(() => _dapperContext.CreateConnection());
        }
    }
}
