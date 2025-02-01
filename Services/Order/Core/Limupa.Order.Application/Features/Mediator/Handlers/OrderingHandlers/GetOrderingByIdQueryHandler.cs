using Limupa.Order.Application.Features.Mediator.Queries.OrderingQueries;
using Limupa.Order.Application.Features.Mediator.Results.OrderingResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;
using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingByIdQueryHandler : IRequestHandler<GetOrderingByIdQuery, GetOrderingByIdQueryResult>
    {
        private readonly IRepository<Ordering> repository;

        public GetOrderingByIdQueryHandler(IRepository<Ordering> repository)
        {
            this.repository = repository;
        }

        public async Task<GetOrderingByIdQueryResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await repository.GetByIdAsync(request.Id);
            return new GetOrderingByIdQueryResult
            {
                OrderingID = value.OrderingID,
                OrderDate = value.OrderDate,
                TotalPrice = value.TotalPrice,
                UserID = value.UserID
            };
        }
    }
}
