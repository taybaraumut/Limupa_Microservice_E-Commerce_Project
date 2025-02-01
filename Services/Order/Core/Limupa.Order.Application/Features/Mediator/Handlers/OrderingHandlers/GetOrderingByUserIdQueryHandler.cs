using Limupa.Order.Application.Features.Mediator.Queries.OrderingQueries;
using Limupa.Order.Application.Features.Mediator.Results.OrderingResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limupa.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingByUserIdQueryHandler : IRequestHandler<GetOrderingByUserIdQuery, List<GetOrderingByUserIdQueryResult>>
    {
        private readonly IOrderingRepository orderingRepository;

        public GetOrderingByUserIdQueryHandler(IOrderingRepository orderingRepository)
        {
            this.orderingRepository = orderingRepository;
        }

        public async Task<List<GetOrderingByUserIdQueryResult>> Handle(GetOrderingByUserIdQuery request, CancellationToken cancellationToken)
        {
            var values = orderingRepository.GetOrderingsByUserId(request.UserId);
            return values.Select(x => new GetOrderingByUserIdQueryResult
            {
                OrderDate = x.OrderDate,
                OrderingID = x.OrderingID,
                TotalPrice = x.TotalPrice,
                UserID = x.UserID

            }).ToList();
        }
    }
}
