using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDataBase
{
    public class Seeder
    {
        private readonly ArticleDbContext _db;

        public Seeder(ArticleDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync()
        {
            if (!_db.Articles.Any())
            {
                var todos = new[]
                {
                    new Article
                    {
                        Title = "elso ujsag",
                        Content = "elso ujsag tartalma"
                    },
                    new Article
                    {
                        Title = "masodik ujsag",
                        Content = "masodik ujsag tartalma"
                    }
                };

                _db.Articles.AddRange(todos);
                await _db.SaveChangesAsync();
            }
        }
    }
}
