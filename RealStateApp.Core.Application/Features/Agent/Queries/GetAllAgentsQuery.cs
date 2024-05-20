using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.Agent.Queries
{
    public record GetAllAgentsQuery : IRequest<Response<ICollection<DtoAccounts>>>;

    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, Response<ICollection<DtoAccounts>>>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public GetAllAgentsQueryHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Response<ICollection<DtoAccounts>>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
        {
            var list = await _accountService.GetAllUsers();
            var agents = list.FindAll(e => e.Role.Contains(RolesEnum.Agent.ToString())).ToList();
            if (agents == null || agents.Count == 0) throw new ApiException("No agents found", (int)HttpStatusCode.NoContent);
            var agentsConverted = agents.Select(e => _mapper.Map<DtoAccounts>(e)).ToList();
            return new Response<ICollection<DtoAccounts>> { Succeeded = true, Data =  agentsConverted };
        }
    }
}
