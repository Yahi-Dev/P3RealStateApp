using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.SaleCategory.Queries.GetAllSaleCategories
{
    public class GetAllSaleCategoriesQuery : IRequest<Response<ICollection<BaseSaleCategoryDto>>>
    {
    }
    public class GetAllSaleCategoriesQueryHandler : IRequestHandler<GetAllSaleCategoriesQuery, Response<ICollection<BaseSaleCategoryDto>>>
    {
        private readonly ISaleCategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetAllSaleCategoriesQueryHandler(ISaleCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ICollection<BaseSaleCategoryDto>>> Handle(GetAllSaleCategoriesQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync();
            if (list == null || list.Count == 0) throw new ApiException("No data found.", (int)HttpStatusCode.NoContent);
            var convertedList = list.Select(e => _mapper.Map<BaseSaleCategoryDto>(e)).ToList();
            return new Response<ICollection<BaseSaleCategoryDto>>(convertedList);   
        }
    }
}
