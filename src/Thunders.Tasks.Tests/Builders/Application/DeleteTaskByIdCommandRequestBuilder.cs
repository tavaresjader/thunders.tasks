using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.DeleteTaskById.Commands;

namespace Thunders.Tasks.Tests.Builders
{
    public class DeleteTaskByIdCommandRequestBuilder
    {
        private int Id { get; set; }

        public DeleteTaskByIdCommandRequestBuilder() 
        {
            Id = GeneralDataBuilder.ID_VALID;
        }

        public DeleteTaskByIdCommandRequest Build() 
        {
            return Builder<DeleteTaskByIdCommandRequest>
                .CreateNew()
                .With(w => w.Id = Id)
                .Build();
        }

        public DeleteTaskByIdCommandRequestBuilder WithId(int id) 
        {
            Id = id;
            return this;
        }
    }
}
