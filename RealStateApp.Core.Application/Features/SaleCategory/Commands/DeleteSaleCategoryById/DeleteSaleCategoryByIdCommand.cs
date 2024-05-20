using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.SaleCategory.Commands.DeleteSaleCategoryById
{
    public class DeleteSaleCategoryByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter("The ID of the sale category to delete.")]
        public int Id { get; set; }

        public DeleteSaleCategoryByIdCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteSaleCategoryByIdCommandHandler : IRequestHandler<DeleteSaleCategoryByIdCommand, Response<int>>
    {
        private readonly ISaleCategoryRepository _repository;
        private readonly IMapper _mapper;
        public DeleteSaleCategoryByIdCommandHandler(ISaleCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteSaleCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var saleCategory = await _repository.GetEntityByIdAsync(request.Id);

            if (saleCategory == null) throw new ApiException("Sale category not found.", (int)HttpStatusCode.BadRequest);

            await _repository.DeleteAsync(saleCategory);

            return new Response<int>(saleCategory.Id);
        }
    }
}
