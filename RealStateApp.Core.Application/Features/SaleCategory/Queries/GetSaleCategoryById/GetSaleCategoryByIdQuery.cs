using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.SaleCategory.Queries.GetSaleCategoryById
{
    public class GetSaleCategoryByIdQuery : IRequest<Response<BaseSaleCategoryDto>>
    {
        [SwaggerParameter("The ID of the sale category.")]
        public int Id { get; set; }

        public GetSaleCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetSaleCategoryByIdQueryHandler : IRequestHandler<GetSaleCategoryByIdQuery, Response<BaseSaleCategoryDto>>
    {
        private readonly ISaleCategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetSaleCategoryByIdQueryHandler(ISaleCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BaseSaleCategoryDto>> Handle(GetSaleCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetEntityByIdAsync(request.Id);
            if (entity == null) throw new ApiException("Not found.", (int)HttpStatusCode.NoContent);
            return  new Response<BaseSaleCategoryDto>(_mapper.Map<BaseSaleCategoryDto>(entity));
        }
    }
}
