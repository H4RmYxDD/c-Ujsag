using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDataBase
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public List<ArticleModel> Articles { get; set; } = new List<ArticleModel>();
    }
}
