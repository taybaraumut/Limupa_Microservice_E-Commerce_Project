using Limupa.Comment.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Comment.Api.Context
{
    public class CommentContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1437;initial Catalog=LimupaCommentDb;Trusted_Connection=false;TrustServerCertificate=true;User=sa;Password=Admin3462");
        }
        public DbSet<UserComment> UserComments { get; set; }
    }
}
