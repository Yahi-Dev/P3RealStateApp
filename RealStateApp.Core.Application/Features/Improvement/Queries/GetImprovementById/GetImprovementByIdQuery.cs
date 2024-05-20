using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.Improvement.Queries.GetImprovementById
{
    public class GetImprovementByIdQuery : IRequest<Response<BaseImprovementDto>>
    {
        [SwaggerParameter("The ID of the improvement.")]
        public int Id { get; set; }

        public GetImprovementByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetImprovementByIdQueryHandler : IRequestHandler<GetImprovementByIdQuery, Response<BaseImprovementDto>>
    {
        private readonly IImprovementRepository _repository;
        private readonly IMapper _mapper;

        public GetImprovementByIdQueryHandler(IImprovementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BaseImprovementDto>> Handle(GetImprovementByIdQuery query, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync();
            var improvement = list.Find(i => i.Id == query.Id);
            if (improvement == null) throw new ApiException("Not found.", (int)HttpStatusCode.NoContent);
            return new Response<BaseImprovementDto>(_mapper.Map<BaseImprovementDto>(improvement));
        }
    }
}
