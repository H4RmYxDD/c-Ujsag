using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NewsDataBase
{
    public class ArticleDbContextFactory : IDesignTimeDbContextFactory<ArticleDbContext>
    {
        public ArticleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ArticleDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=NewsMauiWeb;Trusted_Connection=True;TrustServerCertificate=True");

            return new ArticleDbContext(optionsBuilder.Options);
        }
    }
}
