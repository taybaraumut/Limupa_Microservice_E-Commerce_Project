using Limupa.Order.Application.Features.Mediator.Queries.OrderingQueries;
using Limupa.Order.Application.Features.Mediator.Results.OrderingResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;
using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingQueryHandler : IRequestHandler<GetOrderingQuery, List<GetOrderingQueryResult>>
    {
        private readonly IRepository<Ordering> repository;

        public GetOrderingQueryHandler(IRepository<Ordering> repository)
        {
            this.repository = repository;
        }

        public async Task<List<GetOrderingQueryResult>> Handle(GetOrderingQuery request, CancellationToken cancellationToken)
        {
            var values = await repository.GetAllAsync();
            return values.Select(x => new GetOrderingQueryResult
            {
                OrderingID=x.OrderingID,
                OrderDate = x.OrderDate,
                TotalPrice = x.TotalPrice,
                UserID = x.UserID

            }).ToList();
        }
    }
}
