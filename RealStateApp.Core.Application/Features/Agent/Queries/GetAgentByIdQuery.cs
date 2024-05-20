using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Exceptions;
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
    public class GetAgentByIdQuery : IRequest<Response<DtoAccounts>>
    {
        [SwaggerParameter("The ID of the agent.")]
        public string Id { get; set; }

        public GetAgentByIdQuery(string id)
        {
            Id = id;
        }
    }
    public class GetAgentByIdQueryhandler : IRequestHandler<GetAgentByIdQuery, Response<DtoAccounts>>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public GetAgentByIdQueryhandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Response<DtoAccounts>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await _accountService.GetByIdAsync(request.Id);
            if (agent == null) throw new ApiException("Agent not found", (int)HttpStatusCode.NoContent);
            return new Response<DtoAccounts> { Data = _mapper.Map<DtoAccounts>(agent), Succeeded = true};
        }
    }
}
