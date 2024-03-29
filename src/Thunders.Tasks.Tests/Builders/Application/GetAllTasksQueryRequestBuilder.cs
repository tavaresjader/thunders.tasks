using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.GetAllTasks.Queries;

namespace Thunders.Tasks.Tests.Builders.Application
{
    public class GetAllTasksQueryRequestBuilder
    {
        public GetAllTasksQueryRequest Build() 
        {
            return Builder< GetAllTasksQueryRequest>.CreateNew().Build();
        }
    }
}
