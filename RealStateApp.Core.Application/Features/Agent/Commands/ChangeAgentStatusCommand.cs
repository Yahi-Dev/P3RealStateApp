using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Agent.Commands
{
    public class ChangeAgentStatusCommand : IRequest<Response<int>>
    {
        [SwaggerParameter("The ID of the agent.")]
        public string Id { get; init; }

        [SwaggerParameter("The new status of the agent.")]
        public bool IsActive { get; init; }

        public ChangeAgentStatusCommand(string id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }
    }

    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, Response<int>>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public ChangeAgentStatusCommandHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(ChangeAgentStatusCommand request, CancellationToken cancellationToken)
        {
            var agent = await _accountService.GetByIdAsync(request.Id);
            agent.IsActive = request.IsActive;
            await _accountService.UpdateUserAsync(_mapper.Map<RegisterRequest>(agent));
            return new Response<int> { Data = 1, Succeeded = true };
        }
    }
}
