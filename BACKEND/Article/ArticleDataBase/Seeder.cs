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
                var articles = new[]
                {
                    new ArticleModel
                    {
                        Title = "elso ujsag",
                        Content = "elso ujsag tartalma"
                    },
                    new ArticleModel
                    {
                        Title = "masodik ujsag",
                        Content = "masodik ujsag tartalma"
                    }
                };

                _db.Articles.AddRange(articles);
                await _db.SaveChangesAsync();
            }
            if (!_db.Authors.Any())
            {
                var authors = new[]
                {
                    new Author
                    {
                        Name = "pisti",
                        IsAdmin = false,
                    },
                    new Author
                    {
                        Name = "admin",
                        IsAdmin = true,
                    }
                };

                _db.Authors.AddRange(authors);
                await _db.SaveChangesAsync();
            }


        }

    }
}
