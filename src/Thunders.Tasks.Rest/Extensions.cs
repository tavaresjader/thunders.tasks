using Thunders.Tasks.Core.Data;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Core.Services;
using Thunders.Tasks.Infra.Data;
using Thunders.Tasks.Infra.Repositories;
using Thunders.Tasks.Services;

namespace Thunders.Tasks.Rest
{
    public static class Extensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration) 
        {        
            services.AddSingleton<IDapperContext, DapperContext>();                  
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
