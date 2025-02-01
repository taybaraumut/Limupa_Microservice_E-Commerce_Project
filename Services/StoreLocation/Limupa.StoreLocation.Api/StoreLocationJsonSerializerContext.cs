using Limupa.StoreLocation.Api.Dtos;
using Limupa.StoreLocation.Api.Entities;
using Limupa.StoreLocation.Api.Extensions;
using Limupa.StoreLocation.Api.Middlewares;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Limupa.StoreLocation.Api
{
    [JsonSerializable(typeof(IAsyncEnumerable<City>))]
    public partial class StoreLocationJsonSerializerContext:JsonSerializerContext
    {
    }
}
