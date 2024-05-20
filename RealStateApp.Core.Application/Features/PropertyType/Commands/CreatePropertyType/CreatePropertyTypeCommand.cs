using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<Response<BasePropertyTypeDto>>
    {
        [SwaggerParameter("The name of the property type.")]
        public string Name { get; set; }

        [SwaggerParameter("The description of the property type.")]
        public string Description { get; set; }

        public CreatePropertyTypeCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, Response<BasePropertyTypeDto>>
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IMapper _mapper;

        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BasePropertyTypeDto>> Handle(CreatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.AddAsync(_mapper.Map<Domain.Entities.PropertyType>(command));
            return new Response<BasePropertyTypeDto>(_mapper.Map<BasePropertyTypeDto>(entity));
        }
    }
}
