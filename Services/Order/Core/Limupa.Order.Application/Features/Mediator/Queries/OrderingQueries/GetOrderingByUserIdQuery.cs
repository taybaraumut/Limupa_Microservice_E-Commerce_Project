using Limupa.Order.Application.Features.CQRS.Results.AddressResults;
using Limupa.Order.Application.Features.Mediator.Results.OrderingResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limupa.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByUserIdQuery:IRequest<List<GetOrderingByUserIdQueryResult>>
    {
        public string UserId { get; set; }
        public GetOrderingByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
