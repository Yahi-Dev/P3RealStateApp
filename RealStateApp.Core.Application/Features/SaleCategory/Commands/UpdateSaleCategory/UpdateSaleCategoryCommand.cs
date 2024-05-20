using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.SaleCategory.Commands.UpdateSaleCategory
{
    public class UpdateSaleCategoryCommand : IRequest<Response<UpdateSaleCategoryResponse>>
    {
        [SwaggerParameter("The ID of the sale category.")]
        public int Id { get; set; }

        [SwaggerParameter("The updated name of the sale category if updated.")]
        public string Name { get; set; }

        [SwaggerParameter("The updated description of the sale category if updated.")]
        public string Description { get; set; }

        public UpdateSaleCategoryCommand(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    public class UpdateSaleCategoryCommandHandler : IRequestHandler<UpdateSaleCategoryCommand, Response<UpdateSaleCategoryResponse>>
    {
        private readonly ISaleCategoryRepository _repository;
        private readonly IMapper _mapper;
        public UpdateSaleCategoryCommandHandler(ISaleCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateSaleCategoryResponse>> Handle(UpdateSaleCategoryCommand command, CancellationToken cancellationToken)
        {
            var saleCategory = await _repository.GetEntityByIdAsync(command.Id);

            if (saleCategory == null) throw new Exception("Entity not found.");

            await _repository.UpdateAsync(_mapper.Map<Domain.Entities.SaleCategory>(command), command.Id);

            saleCategory = await _repository.GetEntityByIdAsync(command.Id);

            return new Response<UpdateSaleCategoryResponse>(_mapper.Map<UpdateSaleCategoryResponse>(saleCategory));
        }
    }
}
