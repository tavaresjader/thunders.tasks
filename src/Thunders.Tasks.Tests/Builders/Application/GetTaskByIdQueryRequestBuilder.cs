using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.GetTaskById.Queries;

namespace Thunders.Tasks.Tests.Builders.Application
{
    public class GetTaskByIdQueryRequestBuilder
    {
        private int Id { get; set; }

        public GetTaskByIdQueryRequestBuilder()
        {
            Id = GeneralDataBuilder.ID_VALID;
        }

        public GetTaskByIdQueryRequest Build() 
        {
            return Builder<GetTaskByIdQueryRequest>.CreateNew()
                .With(w => w.Id, Id)
                .Build();
        }

        public GetTaskByIdQueryRequestBuilder WithId(int id)
        {
            Id = id;
            return this;
        }
    }
}
