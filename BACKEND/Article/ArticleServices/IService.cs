using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using ArticleDataBase;

namespace ArticleServices
{
    public interface IService
    {
        Task CreateArticleAsync(Article entity);
        Task CreateAuthorAsync (Author author);
        Task<ArticleDto> GetArticleAsync(int id);
        Task<List<ArticleDto>> ListAllArticleAsync();
        Task DeleteArticleAsync(int id);
        Task UpdateArticleAsync(Article model);
    }
}
