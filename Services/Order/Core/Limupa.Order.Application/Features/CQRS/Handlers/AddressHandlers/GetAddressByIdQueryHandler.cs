using Limupa.Order.Application.Features.CQRS.Queries.AddressQueries;
using Limupa.Order.Application.Features.CQRS.Results.AddressResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressByIdQueryHandler
    {
        private readonly IRepository<Address> repository;

        public GetAddressByIdQueryHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery getAddressByIdQuery)
        {
            var values = await repository.GetByIdAsync(getAddressByIdQuery.Id);
            return new GetAddressByIdQueryResult
            {
                AddressID = values.AddressID,
                City = values.City,
                Detail = values.DetailOne,
                District = values.District,
                UserID = values.UserID
            };
        }
    }
}
