using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDataBase
{
    public class ArticleDbContext : IdentityDbContext<IdentityUser>
    {
        public ArticleDbContext(DbContextOptions<ArticleDbContext> options)
            : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
