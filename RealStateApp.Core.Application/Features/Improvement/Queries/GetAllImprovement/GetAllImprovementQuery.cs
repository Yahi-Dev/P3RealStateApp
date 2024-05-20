using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Improvement.Queries.GetAllImprovement
{
    public class GetAllImprovementQuery : IRequest<Response<IList<BaseImprovementDto>>>
    {
    }
    public class GetAllImprovementQueryHandler : IRequestHandler<GetAllImprovementQuery, Response<IList<BaseImprovementDto>>>
    {
        private readonly IImprovementRepository _repository;
        private readonly IMapper _mapper;

        public GetAllImprovementQueryHandler(IImprovementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IList<BaseImprovementDto>>> Handle(GetAllImprovementQuery query, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync();
            if (list == null || list.Count == 0) throw new ApiException("No data found.", (int)HttpStatusCode.NoContent);
            var convertedList = list.Select(i => _mapper.Map<BaseImprovementDto>(i)).ToList();
            return new Response<IList<BaseImprovementDto>>(convertedList);
        }
    }
}
