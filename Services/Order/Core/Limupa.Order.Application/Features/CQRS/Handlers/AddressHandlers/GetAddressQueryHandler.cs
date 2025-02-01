using Limupa.Order.Application.Features.CQRS.Results.AddressResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressQueryHandler
    {
        private readonly IRepository<Address> repository;

        public GetAddressQueryHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        public async Task<List<GetAddressQueryResult>> Handle()
        {
            var values = await repository.GetAllAsync();
            return values.Select(x=>new GetAddressQueryResult
            {
                AddressID = x.AddressID,
                City = x.City,
                Detail = x.DetailOne,
                District = x.District,
                UserID = x.UserID

            }).ToList();
        }
    }
}
