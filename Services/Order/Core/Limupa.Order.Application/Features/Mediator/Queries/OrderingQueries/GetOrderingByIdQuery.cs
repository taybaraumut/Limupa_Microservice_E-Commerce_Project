﻿using Limupa.Order.Application.Features.Mediator.Results.OrderingResults;
using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByIdQuery:IRequest<GetOrderingByIdQueryResult>
    {
        public int Id { get; set; }
        public GetOrderingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
