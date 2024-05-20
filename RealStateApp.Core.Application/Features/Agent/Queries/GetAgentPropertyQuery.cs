using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agent.Queries
{
    public class GetAgentPropertyQuery : IRequest<Response<ICollection<BasePropertyDto>>>
    {
        [SwaggerParameter("The ID of the agent.")]
        public string Id { get; set; }

        public GetAgentPropertyQuery(string id)
        {
            Id = id;
        }
    }
    public class GetAgentPropertyQueryHandler : IRequestHandler<GetAgentPropertyQuery, Response<ICollection<BasePropertyDto>>>
    {
        private readonly IAccountService _accountService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public GetAgentPropertyQueryHandler(IAccountService accountService, IMapper mapper, IPropertyRepository propertyRepository)
        {
            _accountService = accountService;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public async Task<Response<ICollection<BasePropertyDto>>> Handle(GetAgentPropertyQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.FindAllAsync(p => p.AgentId == request.Id);
            if (properties == null || properties.Count == 0) throw new ApiException("No properties found.",(int)HttpStatusCode.NoContent);
            var agentProperties = properties.Select(e => _mapper.Map<BasePropertyDto>(e)).ToList();
            if (agentProperties == null || agentProperties.Count == 0) throw new ApiException("The agent has no properties registered.", (int)HttpStatusCode.NoContent);
            return new Response<ICollection<BasePropertyDto>> { Data = agentProperties, Succeeded = true};
        }
    }
}
