using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDataBase
{
    [PrimaryKey(nameof(AuthorId), nameof(ArticleId))]
    public class ArticleAuthor
    {
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }
        public Author Author { get; set; }
        public ArticleModel Article { get; set; }
    }
}
