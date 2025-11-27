using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataBase
{
    public class ArticleDbContext : IdentityDbContext<IdentityUser>
    {
        public ArticleDbContext(DbContextOptions<ArticleDbContext> options)
            : base(options) { }
        public DbSet<Article> Articles { get; set; }
    }
}
