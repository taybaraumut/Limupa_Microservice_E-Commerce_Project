using Limupa.Order.Application.Features.Mediator.Results.OrderingResults;
using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingQuery:IRequest<List<GetOrderingQueryResult>>
    {
    }
}
