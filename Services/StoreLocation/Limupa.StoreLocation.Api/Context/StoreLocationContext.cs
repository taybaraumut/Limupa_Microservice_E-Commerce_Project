using Limupa.StoreLocation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Limupa.StoreLocation.Api.Context
{
    public class StoreLocationContext:DbContext
    {
        public StoreLocationContext(DbContextOptions<StoreLocationContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<City> Cities { get; set; }

        public static readonly Func<StoreLocationContext, IAsyncEnumerable<City>> GetAllStoreLocationAsync
            = EF.CompileAsyncQuery((StoreLocationContext context) => context.Cities.Take(1000));
    }
}
