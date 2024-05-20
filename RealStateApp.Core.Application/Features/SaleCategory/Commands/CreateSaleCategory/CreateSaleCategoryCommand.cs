using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Features.Property.Commands.CreateProperty;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RealStateApp.Core.Application.Features.SaleCategory.Commands.CreateSaleCategory
{
    public class CreateSaleCategoryCommand : IRequest<Response<int>>
    {

        [SwaggerParameter("The name of the sale category.")]
        public string Name { get; set; }

        [SwaggerParameter("The description of the sale category.")]
        public string Description { get; set; }

        public CreateSaleCategoryCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public class CreateSaleCategoryCommandHandler : IRequestHandler<CreateSaleCategoryCommand, Response<int>>
    {
        private readonly ISaleCategoryRepository _repository;
        private readonly IMapper _mapper;
        public CreateSaleCategoryCommandHandler(ISaleCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateSaleCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(_mapper.Map<Domain.Entities.SaleCategory>(command));
            return new Response<int>(result.Id);
        }
    }
}
